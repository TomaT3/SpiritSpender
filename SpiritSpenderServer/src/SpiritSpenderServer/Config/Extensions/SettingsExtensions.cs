namespace SpiritSpenderServer.Config.Extensions;

public static class SettingsExtensions
{
    public static void MapSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDB>(configuration.GetSection(nameof(MongoDB)));
        services.Configure<IoBroker>(configuration.GetSection(nameof(IoBroker)));
        services.Configure<CommonServerSettings>(configuration.GetSection(nameof(CommonServerSettings)));
        services.Configure<SerialCommunicationConfig>(configuration.GetSection(SerialCommunicationConfig.SECTION_NAME));
    }
}
