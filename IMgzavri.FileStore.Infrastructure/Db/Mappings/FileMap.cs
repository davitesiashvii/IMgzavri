using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMgzavri.FileStore.Infrastructure.Db.Mappings
{
    public class FileMap : DbEntityConfiguration<IMgzavri.FileStore.Domain.File>
    {
        public override void Configure(EntityTypeBuilder<IMgzavri.FileStore.Domain.File> entity)
        {
            entity.ToTable("Files", "storage");

            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasColumnName("Id").ValueGeneratedNever();

            entity.Property(x => x.CreateUserId).HasColumnName("CreateUserId");
            entity.Property(x => x.CreateDate).HasColumnName("CreateDate");
            entity.Property(x => x.DeleteDate).HasColumnName("DeleteDate");
            entity.Property(x => x.DeleteUserId).HasColumnName("DeleteUserId");
            entity.Property(x => x.ContentType).HasColumnName("ContentType");
            entity.Property(x => x.Extension).HasColumnName("Extension");
            entity.Property(x => x.Name).HasColumnName("Name");
            entity.Property(x => x.Size).HasColumnName("Size");

            entity.HasQueryFilter(x => !x.DeleteDate.HasValue);
        }
    }
}
