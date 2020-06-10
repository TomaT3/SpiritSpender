using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization;
using NSubstitute;
using SpiritSpenderServer.Automatic;
using SpiritSpenderServer.Config;
using SpiritSpenderServer.Config.HardwareConfiguration;
using SpiritSpenderServer.Events.Mqtt;
using SpiritSpenderServer.Events.Mqtt.Settings;
using SpiritSpenderServer.Events.SignalR;
using SpiritSpenderServer.HardwareControl;
using SpiritSpenderServer.Persistence;
using SpiritSpenderServer.Persistence.DriveSettings;
using SpiritSpenderServer.Persistence.Positions;
using SpiritSpenderServer.Persistence.Serialization;
using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
using UnitsNet.Serialization.JsonNet;

namespace SpiritSpenderServer
{
    public class Startup
    {
        private readonly string _myAllowSpecificOrigins = "_myAllowSpecificOrigins";
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new ServerConfig();
            Configuration.Bind(config);

            services.AddControllers().AddNewtonsoftJson(action => action.SerializerSettings.Converters.Add(new UnitsNetJsonConverter()));

            BsonSerializer.RegisterSerializationProvider(new UnitNetSerializationProvider());
            services.AddSingleton(config.MongoDB);
            services.AddSingleton<ISpiritSpenderDBContext, SpiritSpenderDBContext>();
            services.AddSingleton<IDriveSettingRepository, DriveSettingRepository>();
            services.AddSingleton<ISpiritDispenserSettingRepository, SpiritDispenserSettingRepository>();
            services.AddSingleton<IShotGlassPositionSettingRepository, ShotGlassPositionSettingRepository>();
            services.AddSingleton<IAutomaticMode, AutomaticMode>();
            services.AddSingleton<IHardwareConfiguration, HardwareConfiguration>();

            ConfigureMqtt(services);
            services.AddSignalR();
            
            services.AddCors(options =>
            {
                options.AddPolicy(_myAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            if (_env.IsDevelopment())
            {
                services.AddSingleton<IGpioControllerFacade>(_ => Substitute.For<IGpioControllerFacade>());
            }
            else
            {
                services.AddSingleton<IGpioControllerFacade, GpioControllerFacade>();
            }
            
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SpiritSpender API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(_myAllowSpecificOrigins);

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Spirit Spender API V1");
            });

            app.UseRouting();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/signalr");
            });

            var hardwareConfiguration = app.ApplicationServices.GetService<IHardwareConfiguration>();
            hardwareConfiguration.LoadHardwareConfiguration().Wait();

            var mqttConnection = app.ApplicationServices.GetService<IMqttConnection>();
            mqttConnection.ConnectAsync().Wait();

            mqttConnection.Subscribe<string>("my/topic", s =>
            {
                Console.WriteLine(s);
            }).Wait();
            mqttConnection.Publish("my/topic", "it is working");
        }

        private void ConfigureMqtt(IServiceCollection services)
        {
            var mqttSettings = new MqttSettings();
            Configuration.Bind("Mqtt", mqttSettings);

            services.AddSingleton(mqttSettings);
            services.AddSingleton<IMqttConnection, MqttConnection>();
        }
    }
}
