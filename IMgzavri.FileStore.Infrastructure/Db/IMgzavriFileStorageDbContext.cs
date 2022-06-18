using IMgzavri.FileStore.Infrastructure.Db.Mappings;
using Microsoft.EntityFrameworkCore;

namespace IMgzavri.FileStore.Infrastructure.Db
{
    public class IMgzavriFileStorageDbContext : DbContext
    {
        public DbSet<IMgzavri.FileStore.Domain.File> Files { get; set; }

        public IMgzavriFileStorageDbContext(DbContextOptions<IMgzavriFileStorageDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new FileMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
