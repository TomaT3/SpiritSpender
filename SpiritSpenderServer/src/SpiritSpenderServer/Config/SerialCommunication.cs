namespace SpiritSpenderServer.Config;

public class SerialCommunicationConfig
{
    public static string SECTION_NAME = "SerialCommunication";
    public string? Port { get; set; }
    public int BaudRate { get; set; }
    public int ReadTimeout { get; set; }
    public int WriteTimeout { get; set; }
}
