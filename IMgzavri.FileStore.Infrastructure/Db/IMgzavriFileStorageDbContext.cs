using IMgzavri.FileStore.Domain;
using IMgzavri.FileStore.Infrastructure.Db.Mappings;
using Microsoft.EntityFrameworkCore;

namespace IMgzavri.FileStore.Infrastructure.Db
{
    public class IMgzavriFileStorageDbContext : DbContext
    {


        public IMgzavriFileStorageDbContext(DbContextOptions<IMgzavriFileStorageDbContext> options) : base(options)
        {
            
        }

        public DbSet<IMgzavri.FileStore.Domain.File> Files { get; set; }

        public DbSet<fl> Fi{ get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("data source=localhost; initial catalog=IMgzavriDb1; integrated security=true; App=WT.Core.WebApi;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.AddConfiguration(new FileMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
