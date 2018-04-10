using System.Data.SqlClient;

namespace MoviesWebApp.DataAccess
{
    public interface IConnectionFactory
    {
        SqlConnection GetConnection();
    }
}
