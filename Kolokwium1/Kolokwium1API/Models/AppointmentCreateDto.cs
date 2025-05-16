namespace Kolokwium1API.Models;

public class AppointmentCreateDto
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public string Pwz { get; set; } = null!;
    public List<ServiceCreateDto> Services { get; set; } = new();
}

public class ServiceCreateDto
{
    public string ServiceName { get; set; } = null!;
    public decimal ServiceFee { get; set; }
}

