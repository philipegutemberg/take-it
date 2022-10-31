using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Database.SQLServer.Connection
{
    internal class DbConnection
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public DbConnection(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> ExecuteAsyncWithTransaction(string sql, object parameters)
        {
            using IDbConnection connection = _dbConnectionFactory.Build();
            connection.Open();

            using var dbTransaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

            try
            {
                int rowsAffected = await connection.ExecuteAsync(sql.ToString(), param: parameters, transaction: dbTransaction);
                dbTransaction.Commit();
                return rowsAffected;
            }
            catch (Exception)
            {
                dbTransaction.Rollback();
                throw;
            }
        }

        public async Task<IEnumerable<dynamic>> QueryAsync(string sql, object? parameters)
        {
            using IDbConnection connection = _dbConnectionFactory.Build();
            connection.Open();
            return await connection.QueryAsync(sql, param: parameters);
        }

        public async Task<IEnumerable<dynamic>> QueryAsync(string sql)
        {
            return await QueryAsync(sql, null);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters)
        {
            using IDbConnection connection = _dbConnectionFactory.Build();
            connection.Open();
            return await connection.QueryAsync<T>(sql, param: parameters);
        }

        public async Task<T> QuerySingle<T>(string sql, object parameters)
        {
            using IDbConnection connection = _dbConnectionFactory.Build();
            connection.Open();
            return await connection.QuerySingleOrDefaultAsync<T>(sql, param: parameters);
        }

        public async Task<dynamic> QuerySingle(string sql, object parameters)
        {
            using IDbConnection connection = _dbConnectionFactory.Build();
            connection.Open();
            return await connection.QuerySingleOrDefaultAsync(sql, param: parameters);
        }
    }
}