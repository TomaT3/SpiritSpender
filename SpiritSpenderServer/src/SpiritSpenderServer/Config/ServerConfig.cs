namespace SpiritSpenderServer.Config
{
    public class ServerConfig
    {
        public MongoDBConfig MongoDB { get; set; } = new MongoDBConfig();
        public IoBrokerConfig IoBroker { get; set; } = new IoBrokerConfig();
    }
}
