using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace xUnitDM.Configuration
{
    public class ConfigurationFixture
    {
        public IConfiguration Configuration { get; }

        public ConfigurationFixture() 
        { 
            var inMemorySettings = new Dictionary<string, string> 
            {
                
            };

            Configuration = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                            .Build();
        }

    }
}
