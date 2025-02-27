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
            optionsBuilder.UseSqlServer("Server = .; Database = HospitalManagemenet3; User Id = sa; Password = 1; TrustServerCertificate=True;");

            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);

        }
        #region Tables

        //public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Polyclinic> Polyclinic { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        /// <summary>
        /// yeni eklediklerim
        /// </summary>


        #endregion

        #region

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Appointment>()
            //    .HasOne(a => a.Polyclinic)
            //    .WithMany()
            //    .HasForeignKey(a => a.PolyclinicId)
            //    .OnDelete(DeleteBehavior.Restrict);
                

            #endregion

        }
    }
}
