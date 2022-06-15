using IMgzavri.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Infrastructure.Db
{
    public class IMgzavriDbContext : DbContext
    {

        public IMgzavriDbContext(DbContextOptions<IMgzavriDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer(Configuration.GetConnectionString("IMgzavriDbContext"));
        //}

        public DbSet<Users> Users { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //User
            modelBuilder.Entity<RefreshToken>()
                .HasOne(x=>x.User)
                .WithMany(x=> x.RefreshTokens)
                .HasForeignKey(x=>x.UserId);
        }
    }
}
