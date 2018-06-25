using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace cleaning_robotApp_CallingApi.Class
{
    class Config
    {

        /// <summary>
        /// THis method search in the appsettings.json for a configuration
        /// </summary>
        /// <param name="key">key to search</param>
        /// <returns>Value on the key config</returns>
        public string getConfig(string key)
        {
            string config;
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            config = configuration[key];

            return config;

        }


    }
}
