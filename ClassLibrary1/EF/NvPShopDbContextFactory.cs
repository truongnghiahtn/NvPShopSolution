using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace NvPShop.Data.EF
{
    public class NvPShopDbContextFactory : IDesignTimeDbContextFactory<NvPShopDbContext>
    {
        public NvPShopDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuration.GetConnectionString("NvPShopDb");

            var optionsBuilder = new DbContextOptionsBuilder<NvPShopDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new NvPShopDbContext(optionsBuilder.Options);
        }
    }
}
