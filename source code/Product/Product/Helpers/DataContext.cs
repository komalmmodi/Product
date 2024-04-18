using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Product.Helpers
{
    public class DataContext
    {
        private DbSettings _dbSettings;

        public DataContext(IOptions<DbSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = $"Server={_dbSettings.Server}; Database={_dbSettings.Database}; User Id={_dbSettings.UserId}; Password={_dbSettings.Password};TrustServerCertificate=true";
            return new SqlConnection(connectionString);
        }

        public async Task Init()
        {
            await _initDatabase();
            //await _initTables();
        }

        private async Task _initDatabase()
        {
            // create database if it doesn't exist
            var connectionString = $"Server={_dbSettings.Server}; Database={_dbSettings.Database}; User Id={_dbSettings.UserId}; Password={_dbSettings.Password};TrustServerCertificate=true";
            using var connection = new SqlConnection(connectionString);
            var sql = $"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{_dbSettings.Database}') CREATE DATABASE [{_dbSettings.Database}];";
            await connection.ExecuteAsync(sql);
        }

        private async Task _initTables()
        {
            // create tables if they don't exist
            using var connection = CreateConnection();
            await _initProducts();

            async Task _initProducts()
            {
                var sql = """
                IF OBJECT_ID('tabProductDetails', 't') IS NULL
                CREATE TABLE tabProductDetails (
                    iProductId INT NOT NULL PRIMARY KEY IDENTITY,
                    varProductName VARCHAR(100),
                    iProductPrice INT
                );
            """;
                await connection.ExecuteAsync(sql);
            }
        }
    }
}
