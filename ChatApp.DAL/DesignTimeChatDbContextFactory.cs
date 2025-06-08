using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ChatApp.DAL
{
    public class DesignTimeChatDbContextFactory : IDesignTimeDbContextFactory<ChatDbContext>
    {
        public ChatDbContext CreateDbContext(string[] args)
        {
            // Always resolve the path to the server project's appsettings.json
            var serverProjectPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../ChatApp.Server"));
            var appSettingsPath = Path.Combine(serverProjectPath, "appsettings.json");

            if (!File.Exists(appSettingsPath))
            {
                throw new FileNotFoundException($"Could not find 'appsettings.json' at path: {appSettingsPath}");
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(serverProjectPath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("DefaultConnection string is missing or empty in appsettings.json.");

            var optionsBuilder = new DbContextOptionsBuilder<ChatDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ChatDbContext(optionsBuilder.Options);
        }
    }
}