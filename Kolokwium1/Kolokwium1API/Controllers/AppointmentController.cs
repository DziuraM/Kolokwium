using Kolokwium1API.Data;
using Kolokwium1API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium1API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly MedicalContext _context;

    public AppointmentsController(MedicalContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointment(int id)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Include(a => a.AppointmentServices)
            .ThenInclude(asv => asv.Service)
            .FirstOrDefaultAsync(a => a.AppointmentId == id);

        if (appointment == null)
            return NotFound();

        return Ok(new
        {
            date = appointment.Date,
            patient = new
            {
                firstName = appointment.Patient?.FirstName,
                lastName = appointment.Patient?.LastName,
                dateOfBirth = appointment.Patient?.DateOfBirth
            },
            doctor = new
            {
                doctorId = appointment.Doctor?.DoctorId,
                pwz = appointment.Doctor?.Pwz
            },
            appointmentServices = appointment.AppointmentServices.Select(asv => new
            {
                name = asv.Service?.Name,
                serviceFee = asv.ServiceFee
            })
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAppointment([FromBody] AppointmentCreateDto dto)
    {
        if (await _context.Appointments.AnyAsync(a => a.AppointmentId == dto.AppointmentId))
            return Conflict($"Appointment with ID {dto.AppointmentId} already exists.");

        var patient = await _context.Patients.FindAsync(dto.PatientId);
        if (patient == null)
            return NotFound($"Patient with ID {dto.PatientId} not found.");

        var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Pwz == dto.Pwz);
        if (doctor == null)
            return NotFound($"Doctor with PWZ '{dto.Pwz}' not found.");

        var appointmentServices = new List<AppointmentService>();
        foreach (var s in dto.Services)
        {
            var service = await _context.Services.FirstOrDefaultAsync(sv => sv.Name == s.ServiceName);
            if (service == null)
                return NotFound($"Service '{s.ServiceName}' not found.");

            appointmentServices.Add(new AppointmentService
            {
                ServiceId = service.ServiceId,
                ServiceFee = s.ServiceFee
            });
        }

        var appointment = new Appointment
        {
            AppointmentId = dto.AppointmentId,
            PatientId = dto.PatientId,
            DoctorId = doctor.DoctorId,
            Date = DateTime.Now,
            AppointmentServices = appointmentServices
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAppointment), new { id = appointment.AppointmentId }, null);
    }

}