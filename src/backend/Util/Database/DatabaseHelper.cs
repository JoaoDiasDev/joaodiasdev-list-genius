using MySqlConnector;

namespace JoaoDiasDev.ListGenius.Util.Database
{
    public static class DatabaseHelper
    {
        public static bool CreateDatabase(string connection)
        {
            try
            {
                var connectionStringBuilder = new MySqlConnectionStringBuilder(connection);
                var databaseName = connectionStringBuilder.Database;
                connectionStringBuilder.Database = "mysql";
                using (var createDatabaseConnection = new MySqlConnection(connectionStringBuilder.ToString()))
                {
                    createDatabaseConnection.Open();

                    var databaseExistsQuery = $"SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '{databaseName}'";
                    var databaseExists = new MySqlCommand(connection: createDatabaseConnection, commandText: databaseExistsQuery).ExecuteNonQuery();

                    if (databaseExists != 0)
                    {
                        var createDatabaseQuery = $"CREATE DATABASE IF NOT EXISTS `{databaseName}`;";
                        new MySqlCommand(connection: createDatabaseConnection, commandText: createDatabaseQuery).ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
