using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cleaning_Robot_Api.Service
{
    /// <summary>
    /// this service is use to get the all the configuration 
    /// this interface it can be use in all the project to get the key of the appsetting.json
    /// </summary>
    public interface IConfigMessage
    {
        string getConfigurationMessage(string msg);
    }
    /// <summary>
    /// this is class that instance the object to get the configs
    /// </summary>
    public class ConfigMsg : IConfigMessage
    {
        private IConfiguration _configuration;

        public ConfigMsg(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string getConfigurationMessage(string msg)
        {
            return _configuration[msg];
        }


    }
}
