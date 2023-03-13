namespace SpiritSpenderServer;

using ioBroker.net;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization;
using NC_Communication;
using NC_Communication.AxisConfigurations;
using SpiritSpenderServer.API.SignalR.Hubs;
using SpiritSpenderServer.Automatic;
using SpiritSpenderServer.Config;
using SpiritSpenderServer.Config.Extensions;
using SpiritSpenderServer.Config.HardwareConfiguration;
using SpiritSpenderServer.HardwareControl;
using SpiritSpenderServer.HardwareControl.EmergencyStop;
using SpiritSpenderServer.HardwareControl.SpiritSpenderControl;
using SpiritSpenderServer.HardwareControl.StatusLamp;
using SpiritSpenderServer.HostedServices;
using SpiritSpenderServer.Interface.HardwareControl;
using SpiritSpenderServer.Persistence;
using SpiritSpenderServer.Persistence.DriveSettings;
using SpiritSpenderServer.Persistence.Positions;
using SpiritSpenderServer.Persistence.Serialization;
using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
using SpiritSpenderServer.Persistence.StatusLampSettings;
using SpiritSpenderServer.Simulation;
using UnitsNet.Serialization.JsonNet;

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
        services.MapSettings(Configuration);

        services.AddSignalR().AddNewtonsoftJsonProtocol(action => action.PayloadSerializerSettings.Converters.Add(new UnitsNetJsonConverter()));
        services.AddControllers().AddNewtonsoftJson(action => action.SerializerSettings.Converters.Add(new UnitsNetJsonConverter()));

        BsonSerializer.RegisterSerializationProvider(new UnitNetSerializationProvider());
        services.AddSingleton<ISpiritSpenderDBContext, SpiritSpenderDBContext>();
        services.AddSingleton<IDriveSettingRepository, DriveSettingRepository>();
        services.AddSingleton<ISpiritDispenserSettingRepository, SpiritDispenserSettingRepository>();
        services.AddSingleton<IShotGlassPositionSettingRepository, ShotGlassPositionSettingRepository>();
        services.AddSingleton<IStatusLampSettingRepository, StatusLampSettingRepository>();
        services.AddSingleton<StatusObserver>();
        services.AddSingleton<IAutomaticMode, AutomaticMode>();

        services.AddSingleton<ISpiritDispenserControl, SpiritDispenserControl>();
        services.AddSingleton<IStatusLamp, StatusLamp>();
        services.AddSingleton<IEmergencyStop, EmergencyStop>();
        services.AddSingleton<IShotGlassPositionSettingsConfiguration, ShotGlassPositionSettingsConfiguration>();

        services.AddSingleton<INcCommunication, NcCommunication>();
        services.AddSingleton<ISerialCommunication, SerialCommunication>();
        services.AddSingleton<IXAxisConfiguration, XAxisConfiguration>();
        services.AddSingleton<IYAxisConfiguration, YAxisConfiguration>();

        services.AddSingleton<AxisHubInformer>();
        services.AddSingleton<NcCommunicationHubInformer>();


        services.AddSingleton<IIoBrokerDotNet, IoBrokerDotNet>(sp =>
        {
            var ioBrokerSettings = sp.GetRequiredService<IOptions<IoBroker>>().Value;
            return new IoBrokerDotNet(ioBrokerSettings.ConnectionUrl);
        });

        RegisterHostedServices(services);

        services.AddCors(options =>
        {
            options.AddPolicy(_myAllowSpecificOrigins,
            builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .WithOrigins("http://localhost:4200", "http://spiritspender:4200")
                       .AllowCredentials();
            });
        });

        var serverConfig = Configuration.GetSection(nameof(CommonServerSettings)).Get<CommonServerSettings>();
        if (serverConfig != null && serverConfig.UseSimulation)
        {
            SimulationStartup.StartSimulation(services);
        }
        else
        {
            services.AddSingleton<IGpioControllerFacade, GpioControllerFacade>();
            services.AddSingleton<IGpioPinFactory, GpioPinFactory>();
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
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<AxisHub>("/signal-r/axis");
            endpoints.MapHub<NcCommunicationHub>("/signal-r/nc-communication");
        });
    }

    private static void RegisterHostedServices(IServiceCollection services)
    {
        services.AddHostedService<GpioComponentsStartup>();
        services.AddHostedService<StausObserverStartup>();
        services.AddHostedService<SignalRInformers>();
        services.AddHostedService<IoBrokerCommunicationService>();
    }
}
