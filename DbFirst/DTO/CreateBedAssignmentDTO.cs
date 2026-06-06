namespace DbFirst.DTO;

public class CreateBedAssignmentDto
{
    public int WardId { get; set; }

    public int BedTypeId { get; set; }

    public DateTime From { get; set; }

    public DateTime? To { get; set; }
}