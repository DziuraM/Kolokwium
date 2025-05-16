using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium1API.Models;

public class AppointmentService
{
    [Column("appoitment_id")]
    public int AppointmentId { get; set; }

    [Column("service_id")]
    public int ServiceId { get; set; }

    [Column("service_fee")]
    public decimal ServiceFee { get; set; }

    public Appointment? Appointment { get; set; }
    public Service? Service { get; set; }
}

