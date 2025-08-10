using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace xUnitDM.Common
{
    internal static class AppSettingsConfiguration
    {
        private static IConfiguration _configuration;

        public static IConfiguration GetConfiguration()
        {
            _configuration = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                            .Build();

            return _configuration;
        }

    }
}
