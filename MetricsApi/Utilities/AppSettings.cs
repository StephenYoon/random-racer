using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace MetricsApi.Utilities
{
    public class AppSettings : IAppSettings
    {
        public static string EnvironmentCode => System.Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "dev";

        public string Environment => EnvironmentCode;

        public string Version => Assembly.GetAssembly(typeof(AppSettings))?.GetName()?.Version?.ToString(4) ?? "UNKNOWN";

        public string RandomRacerDbConnection { get; set; }
    }
}
