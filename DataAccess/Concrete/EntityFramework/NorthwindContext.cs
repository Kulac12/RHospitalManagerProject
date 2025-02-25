using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class NorthwindContext : DbContext
    {

        // override on
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .; Database = NORTHWND; User Id = sa; Password = 1; TrustServerCertificate=True;");

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        /// <summary>
        /// yeni eklediklerim
        /// </summary>
        public DbSet<Storage> Storage { get; set; }
        public DbSet<Province> Province { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Storage>()
                .HasOne(s => s.Province)  // Storage -> Province ilişkisi
                .WithMany(p => p.Storage) // Bir Province, birçok Storage içerebilir.
                .HasForeignKey(s => s.ProvinceId) // Foreign key olarak ProvinceId kullanılır.
                .OnDelete(DeleteBehavior.Restrict); // Province silindiğinde Storage etkilenmez.


            //Yorum satırları bir developerın içini döktüğü yerdir. 



        }
    }
}
