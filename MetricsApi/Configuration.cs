using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace MetricsApi
{
    public class Configuration
    {
        public static IConfigurationBuilder Build(IConfigurationBuilder config = null)
        {
            var configurationBuilder = (config ?? new ConfigurationBuilder())
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();

            return configurationBuilder;
        }
    }
}
