namespace DbFirst.DTO;

public class PatientDto
{
    public string Pesel { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Sex { get; set; }

    public ICollection<AdmissionDto> Admissions { get; set; }
    public ICollection<BedAssignmentDto> BedAssignments { get; set; }
}