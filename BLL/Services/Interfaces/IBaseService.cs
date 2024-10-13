using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services.Interfaces
{
    public interface IBaseService
    {
        IConfigurationSection GetConfigurationSection(string Key);
    }
}
