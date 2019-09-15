using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace MetricsApi.Utilities
{
    public class AppSettings : IAppSettings
    {
        public static string EnvironmentCode => System.Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "dev";

        //public static string AuthenticationGoogleClientIdValue => System.Environment.GetEnvironmentVariable("AuthenticationGoogleClientId") ?? "AuthenticationGoogleClientId";

        //public static string AuthenticationGoogleClientSecretValue => System.Environment.GetEnvironmentVariable("AuthenticationGoogleClientSecret") ?? "AuthenticationGoogleClientSecret";

        public string Environment => EnvironmentCode;

        public string AuthenticationGoogleClientId { get; set; }

        public string AuthenticationGoogleClientSecret { get; set; }

        public string Version => Assembly.GetAssembly(typeof(AppSettings))?.GetName()?.Version?.ToString(4) ?? "UNKNOWN";

        public string RandomRacerDbConnection { get; set; }
    }
}
