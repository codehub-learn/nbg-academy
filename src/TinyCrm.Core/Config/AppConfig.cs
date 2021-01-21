namespace TinyCrm.Core.Config
{
    public class AppConfig
    {
        public string CrmConnectionString { get; set; }
        public string MinLoggingLevel { get; set; }
        public ClientConfig ClientConfig { get; set; }
    }
}
