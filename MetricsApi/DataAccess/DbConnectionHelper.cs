using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using MetricsApi.Utilities;

namespace MetricsApi.DataAccess
{
    public class DbConnectionHelper : IDbConnectionHelper
    {
        private readonly IAppSettings _appSettings;

        public DbConnectionHelper(IAppSettings settings)
        {
            _appSettings = settings;
        }

        public SqlConnection CreateRandomRacerDbConnection()
        {
            var connectionBuilder = new SqlConnectionStringBuilder(_appSettings.RandomRacerDbConnection);
            return new SqlConnection(connectionBuilder.ToString());
        }
    }
}
