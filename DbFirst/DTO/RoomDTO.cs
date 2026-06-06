namespace DbFirst.DTO;

public class RoomDto
{
    public string Id { get; set; }

    public bool HasTv { get; set; }

    public WardDto Ward { get; set; }
}