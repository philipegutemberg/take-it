using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Database.SQLServer.Connection
{
    internal class DbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection Build()
        {
            return new SqlConnection(_configuration.GetConnectionString("TakeIt"));
        }
    }
}