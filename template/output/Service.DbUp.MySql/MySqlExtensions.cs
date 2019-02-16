using System;
using System.Data;
using DbUp;
using DbUp.Engine.Output;
using MySql.Data.MySqlClient;

public static class MySqlExtensions
{
    /// <summary>
    ///     Ensures that the database specified in the connection string exists.
    /// </summary>
    /// <param name="supported">Fluent helper type.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <returns />
    public static void MySqlDatabase(this SupportedDatabasesForEnsureDatabase supported, string connectionString)
    {
        MySqlDatabase(supported, connectionString, new ConsoleUpgradeLog());
    }

    /// <summary>
    ///     Ensures that the database specified in the connection string exists.
    /// </summary>
    /// <param name="supported">Fluent helper type.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="commandTimeout">
    ///     Use this to set the command time out for creating a database in case you're encountering a
    ///     time out in this operation.
    /// </param>
    /// <returns />
    public static void MySqlDatabase(this SupportedDatabasesForEnsureDatabase supported, string connectionString, int commandTimeout)
    {
        MySqlDatabase(supported, connectionString, new ConsoleUpgradeLog(), commandTimeout);
    }

    /// <summary>
    ///     Ensures that the database specified in the connection string exists.
    /// </summary>
    /// <param name="supported">Fluent helper type.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="logger">The <see cref="T:DbUp.Engine.Output.IUpgradeLog" /> used to record actions.</param>
    /// <param name="timeout">
    ///     Use this to set the command time out for creating a database in case you're encountering a time
    ///     out in this operation.
    /// </param>
    /// <returns />
    public static void MySqlDatabase(this SupportedDatabasesForEnsureDatabase supported, string connectionString, IUpgradeLog logger, int timeout = -1)
    {
        if (supported == null)
            throw new ArgumentNullException(nameof(supported));
        if (string.IsNullOrEmpty(connectionString) || connectionString.Trim() == string.Empty)
            throw new ArgumentNullException(nameof(connectionString));
        if (logger == null)
            throw new ArgumentNullException(nameof(logger));

        var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);
        var initialCatalog = connectionStringBuilder.Database;
        if (string.IsNullOrEmpty(initialCatalog) || initialCatalog.Trim() == string.Empty)
            throw new InvalidOperationException("The connection string does not specify a database name.");
        connectionStringBuilder.Database = "sys";

        var maskedConnectionStringBuilder = new MySqlConnectionStringBuilder(connectionStringBuilder.ConnectionString)
        {
            Password = string.Empty.PadRight(connectionStringBuilder.Password.Length, '*')
        };
        logger.WriteInformation("Using connection string {0}", maskedConnectionStringBuilder.ConnectionString);

        using (var connection = new MySqlConnection(connectionStringBuilder.ConnectionString))
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                logger.WriteError("Unable to open database connection to {0}: {1}",
                    connection.ConnectionString, connection.Database, ex);
                throw;
            }
            using (var mySqlCommand = new MySqlCommand($"CREATE DATABASE IF NOT EXISTS {initialCatalog}", connection) { CommandType = CommandType.Text })
            {
                if (timeout >= 0) mySqlCommand.CommandTimeout = timeout;
                mySqlCommand.ExecuteNonQuery();
            }

            logger.WriteInformation("Ensured database {0} exists", initialCatalog);
        }
    }
}


