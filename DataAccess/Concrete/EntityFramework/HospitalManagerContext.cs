using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class HospitalManagerContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .; Database = R2HospitalManagement; User Id = sa; Password = 1; TrustServerCertificate=True;");

            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);

        }
        #region Tables

        
        public DbSet<User> User { get; set; }
        public DbSet<Polyclinic> Polyclinic { get; set; }

        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        /// <summary>
        /// yeni eklediklerim
        /// </summary>
        #endregion



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne<Polyclinic>()  // Polyclinic ile ilişki
                .WithMany()  // Polyclinic'in User'lara sahip olması gerekmiyor
                .HasForeignKey(u => u.PoliklinikId)  // PolyclinicId yabancı anahtar
                .OnDelete(DeleteBehavior.SetNull);  // İlişkili poliklinik silindiğinde, User'da PolyclinicId null olur
        }
    }
}
