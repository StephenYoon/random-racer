using System.Data.SqlClient;

namespace MetricsApi.DataAccess
{
    public interface IDbConnectionHelper
    {
        SqlConnection CreateRandomRacerDbConnection();
    }
}
