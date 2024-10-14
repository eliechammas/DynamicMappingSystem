using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Common
{
    /// <summary>
    /// helper class that allows using appsettings.config data within the application from static classes 
    /// </summary>
    public static class AppSettingsHelper
    {
        private static IConfiguration _config;

        public static void AppSettingsConfigure(IConfiguration configuration)
        {
            _config = configuration;
        }

        public static IConfigurationSection Setting(string key)
        {
            return _config.GetSection(key);
        }
    }
}
