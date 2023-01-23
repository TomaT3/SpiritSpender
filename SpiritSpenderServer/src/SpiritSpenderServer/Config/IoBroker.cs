namespace SpiritSpenderServer.Config
{
    public class IoBroker
    {
        public string? ConnectionUrl { get; set; }
        public int ConnectionTimeout { get; set; }
        public bool Enabled { get; set; }
    }
}
