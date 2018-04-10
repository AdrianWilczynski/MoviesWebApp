using Microsoft.Extensions.Options;
using MoviesWebApp.Configuration;
using System.Data.SqlClient;

namespace MoviesWebApp.DataAccess
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string defaultConnectionString;

        public ConnectionFactory(IOptions<DatabaseOptions> databaseOptions)
        {
            defaultConnectionString = databaseOptions.Value.DefaultConnectionString;
        }

        public SqlConnection GetConnection() => new SqlConnection(defaultConnectionString);
    }
}
