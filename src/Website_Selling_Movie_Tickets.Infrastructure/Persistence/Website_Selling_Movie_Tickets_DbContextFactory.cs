using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Website_Selling_Movie_Tickets.Infrastructure.Persistence
{
    public class Website_Selling_Movie_Tickets_DbContextFactory : IDesignTimeDbContextFactory<DBContext>
    {
        public DBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
            var connectionString = configuration.GetSection("DatabaseSettings:ConnectionString").Value;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("ConnectionString", "Connection string is not configured.");
            }

            optionsBuilder.UseSqlServer(connectionString);
            return new DBContext(optionsBuilder.Options);
        }
    }
}
