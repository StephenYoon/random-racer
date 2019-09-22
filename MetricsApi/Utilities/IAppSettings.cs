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

        string RandomRacerDbConnection { get; set; }

        string JwtSecret { get; set; }
    }
}
