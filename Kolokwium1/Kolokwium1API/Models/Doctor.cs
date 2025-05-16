using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium1API.Models;

public class Doctor
{
    [Column("doctor_id")]
    public int DoctorId { get; set; }

    [Column("first_name")]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    public string LastName { get; set; } = null!;

    [Column("PWZ")]
    public string Pwz { get; set; } = null!;

    public List<Appointment>? Appointments { get; set; }
}