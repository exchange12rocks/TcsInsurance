using System.Collections.Specialized;
using System.Configuration;
namespace TinkoffService.Helpers
{
    public class ConfigurationManagerHelper
    {
        public static ConfigurationManagerHelper Default
        {
            get
            {
                return new ConfigurationManagerHelper(ConfigurationManager.AppSettings);
            }
        }
        private NameValueCollection appSettings;
        public ConfigurationManagerHelper(NameValueCollection appSettings)
        {
            this.appSettings = appSettings;
        }
        public string VirtuBaseUrl
        {
            get
            {
                return this.appSettings["virtuBaseUrl"];
            }
        }
        public string VirtuUserName
        {
            get
            {
                return this.appSettings["virtuUserName"];
            }
        }
        public string VirtuPassword
        {
            get
            {
                return this.appSettings["virtuPassword"];
            }
        }
        public string WSUserName
        {
            get
            {
                return this.appSettings["wsUserName"];
            }
        }
        public string WSPassword
        {
            get
            {
                return this.appSettings["wsPassword"];
            }
        }
    }
}
