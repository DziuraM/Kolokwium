using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium1API.Models;

public class Service
{
    [Column("service_id")]
    public int ServiceId { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("base_fee")]
    public decimal BaseFee { get; set; }

    public List<AppointmentService>? AppointmentServices { get; set; }
}