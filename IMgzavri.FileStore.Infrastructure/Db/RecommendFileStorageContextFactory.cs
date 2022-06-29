using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IMgzavri.FileStore.Infrastructure.Db
{
    //public class RecommendFileStorageContextFactory : IDesignTimeDbContextFactory<IMgzavriFileStorageDbContext>
    //{
    //    public IMgzavriFileStorageDbContext CreateDbContext(string[] args)
    //    {
    //        var config = new ConfigurationBuilder()
    //            .SetBasePath(Directory.GetCurrentDirectory())
    //            .AddJsonFile("appsettings.json", optional: false)
    //            .Build();

    //        var connectionString = config.GetConnectionString("IRecommendFileStorageDbContext");

    //        var optionsBuilder = new DbContextOptionsBuilder<IMgzavriFileStorageDbContext>()
    //            .UseSqlServer(connectionString);

    //        return new IMgzavriFileStorageDbContext(optionsBuilder.Options);
    //    }
    //}
}

