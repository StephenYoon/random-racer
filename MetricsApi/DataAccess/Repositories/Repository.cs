using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MetricsApi.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        protected readonly IDbConnection DbConnection;

        public Repository(IDbConnectionHelper dbConnectionHelper)
        {
            DbConnection = dbConnectionHelper.CreateRandomRacerDbConnection();
        }
    }
}
