using DbUp;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace $ext_safeprojectname$.DbUpConsole
{
    class Program
    {
        static int Main(string[] args)
        {
            var builder = CreateConfigurationBuilder(args);
            var configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("MainDB");
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = args.FirstOrDefault();
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ConnectionString is null or empty. You should set it!");
                Console.ResetColor();
                return -1;
            }

            EnsureDatabase.For.MySqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .MySqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }

        private static IConfigurationBuilder CreateConfigurationBuilder(string[] args) => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args);
    }
}
