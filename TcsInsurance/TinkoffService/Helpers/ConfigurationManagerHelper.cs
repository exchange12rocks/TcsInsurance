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
        public string virtuBaseUrl
        {
            get
            {
                return this.appSettings["virtuBaseUrl"];
            }
        }
        public string virtuUserName
        {
            get
            {
                return this.appSettings["virtuUserName"];
            }
        }
        public string virtuPassword
        {
            get
            {
                return this.appSettings["virtuPassword"];
            }
        }
    }
}
