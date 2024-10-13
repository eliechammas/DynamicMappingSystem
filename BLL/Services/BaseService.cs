using BLL.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class BaseService: IBaseService
    {
        private IConfiguration _configuration;
        public BaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfigurationSection GetConfigurationSection(string key)
        {
            return this._configuration.GetSection(key);
        }
    }
}
