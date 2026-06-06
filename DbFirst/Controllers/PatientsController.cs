using DbFirst.DTO;
using DbFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbFirst.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    private readonly HospitalContext _context;

    public PatientsController(HospitalContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPatients([FromQuery] string? search)
    {
        var patientsQuery = _context.Patients.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var likePattern = $"%{search}%";

            patientsQuery = patientsQuery.Where(p =>
                EF.Functions.Like(p.FirstName, likePattern) ||
                EF.Functions.Like(p.LastName, likePattern));
        }

        var patients = await patientsQuery
            .Select(p => new PatientDto
            {
                Pesel = p.Pesel,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Age = p.Age,
                Sex = p.Sex ? "Male" : "Female",

                Admissions = p.Admissions.Select(a => new AdmissionDto
                {
                    Id = a.Id,
                    AdmissionDate = a.AdmissionDate,
                    DischargeDate = a.DischargeDate,
                    Ward = new WardDto
                    {
                        Id = a.Ward.Id,
                        Name = a.Ward.Name,
                        Description = a.Ward.Description
                    }
                }).ToList(),

                BedAssignments = p.BedAssignments.Select(b => new BedAssignmentDto
                {
                    Id = b.Id,
                    From = b.From,
                    To = b.To,
                    Bed = new BedDto
                    {
                        Id = b.Bed.Id
                    }
                }).ToList()
            })
            .ToListAsync();

        return Ok(patients);
    }
}