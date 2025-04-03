using Infrastructure.Contract;
using System.Data;
using System.Data.SqlClient;
using Infrastructure.Constants;
using Microsoft.Extensions.Options;

namespace Infrastructure.DbConnection
{
    public class DbConnectionFactory : IDbConnectionFatory

    {
        private IDbConnection _dbConnection;
        private readonly ConnectionString _connectionString;

        public DbConnectionFactory(IOptions<ConnectionString> connectionstring)
        {
            _connectionString = connectionstring.Value;
        }

        public IDbConnection Connection
        {
            get
            {
                if (_dbConnection == null || _dbConnection.State != ConnectionState.Open)
                {
                    _dbConnection = new SqlConnection(_connectionString.DefaultConnection);
                }
                return _dbConnection;
            }
        }

        public void Dispose()
        {
            if (_dbConnection != null)
            {
                _dbConnection.Dispose();
            }
        }
    }
}
