﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMgzavri.FileStore.Infrastructure.Db
{
    public abstract class DbEntityConfiguration<TEntity> where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);
    }
}
