using Microsoft.EntityFrameworkCore;
using Kolokwium1API.Models;

namespace Kolokwium1API.Data;

public class MedicalContext : DbContext
{
    public MedicalContext(DbContextOptions<MedicalContext> options) : base(options) { }

    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<AppointmentService> AppointmentServices => Set<AppointmentService>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>().ToTable("Appointment");
        modelBuilder.Entity<Patient>().ToTable("Patient");
        modelBuilder.Entity<Doctor>().ToTable("Doctor");
        modelBuilder.Entity<Service>().ToTable("Service");
        modelBuilder.Entity<AppointmentService>().ToTable("Appointment_Service");

        modelBuilder.Entity<AppointmentService>()
            .HasKey(asv => new { asv.ServiceId, asv.AppointmentId });

        modelBuilder.Entity<AppointmentService>()
            .HasOne(asv => asv.Appointment)
            .WithMany(a => a.AppointmentServices)
            .HasForeignKey(asv => asv.AppointmentId);

        modelBuilder.Entity<AppointmentService>()
            .HasOne(asv => asv.Service)
            .WithMany(s => s.AppointmentServices)
            .HasForeignKey(asv => asv.ServiceId);
    }
}