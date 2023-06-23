using Microsoft.EntityFrameworkCore;
using HospitalDatabase1.Data.Models;
using System;

namespace HospitalDatabase1.Data
{
    public class HospitalContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-LBAOKKU\\SQLEXPRESS;Initial Catalog=EFHospitalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder
                .Entity<Patient>()
                .HasMany(v => v.Visitations)
                .WithOne(p => p.Patient)
                .HasForeignKey(p => p.PatientId)
                .HasPrincipalKey(p => p.PatientId);

            modelBuilder
                .Entity<Doctor>()
                .HasMany(v => v.Visitations)
                .WithOne(d => d.Doctor)
                .HasForeignKey(d => d.DoctorId)
                .HasPrincipalKey(d => d.DoctorId);

            modelBuilder
                .Entity<Patient>()
                .HasMany(d => d.Diagnoses)
                .WithOne(p => p.Patient)
                .HasForeignKey(p => p.PatientId)
                .HasPrincipalKey(p => p.PatientId);

            modelBuilder
                .Entity<Patient>()
                .HasMany(t => t.PatientMedicaments)
                .WithOne(t => t.Patient)
                .HasForeignKey(t => t.PatientId)
                .HasPrincipalKey(t => t.PatientId);

            modelBuilder
               .Entity<PatientMedicament>()
               .HasKey(t => new { t.PatientId, t.MedicamentId });

            modelBuilder
                .Entity<Medicament>()
                .HasMany(m => m.PatientMedicament)
                .WithOne(m => m.Medicament)
                .HasForeignKey(m => m.MedicamentId)
                .HasPrincipalKey(m => m.MedicamentId);
        }
    }
}