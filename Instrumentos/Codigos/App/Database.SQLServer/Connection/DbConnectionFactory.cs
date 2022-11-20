using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Database.SQLServer.Connection
{
    internal class DbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Build()
        {
            return new NpgsqlConnection(_configuration.GetConnectionString("TakeIt"));
        }
    }
}