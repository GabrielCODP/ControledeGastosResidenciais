using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace MyExpenseControl.Infrastructure.Migrations
{
    public static class DatabaseMigration
    {
        public static void Migrate(string connectionString, IServiceProvider serviceProvider)
        {
            EnsureDatabaseCraeted_Mysql(connectionString);

            MigrationDatabase(serviceProvider);
        }

        private static void EnsureDatabaseCraeted_Mysql(string connectionString)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);

            var databaseName = connectionStringBuilder.Database;

            //Se o banco não existir, você não consegue conectar nele
            connectionStringBuilder.Remove("Database");

            //Using garante que a conexão será fechadoi automaticamente
            using var dbConnection = new MySqlConnection(connectionStringBuilder.ConnectionString);

            var paramets = new DynamicParameters();
            paramets.Add("name", databaseName);

            var records = dbConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @name", paramets);

            if (records.Any() == false)
            {
                dbConnection.Execute($"CREATE DATABASE {databaseName}");
            }
        }

        private static void MigrationDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.ListMigrations();
            runner.MigrateUp();
        }
    }
}
