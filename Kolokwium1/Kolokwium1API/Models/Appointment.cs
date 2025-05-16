using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium1API.Models;

public class Appointment
{
    [Column("appoitment_id")]
    public int AppointmentId { get; set; }

    [Column("patient_id")]
    public int PatientId { get; set; }

    [Column("doctor_id")]
    public int DoctorId { get; set; }

    [Column("date")]
    public DateTime Date { get; set; }

    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }
    public List<AppointmentService>? AppointmentServices { get; set; }
}