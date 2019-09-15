using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsApi.Utilities
{
    public interface IAppSettings
    {
        string Environment { get; }

        string Version { get; }

        string AuthenticationGoogleClientId { get; set; }

        string AuthenticationGoogleClientSecret { get; set; }

        string RandomRacerDbConnection { get; set; }
    }
}
