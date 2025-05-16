using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium1API.Models;

public class Patient
{
    [Column ("patient_id")]
    public int PatientId { get; set; }
    
    [Column ("first_name")]
    public string FirstName { get; set; }
    
    [Column ("last_name")]
    public string LastName { get; set; }
    
    [Column ("date_of_birth")]
    public DateTime DateOfBirth { get; set; }
    
    public List<Appointment>? Appointments { get; set; }
    
    
}