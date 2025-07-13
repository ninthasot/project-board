namespace SharedKernel.Entities;

public class Attachment : BaseEntity<Guid>
{
    public Guid CardId { get; set; }
    public required string FileName { get; set; }
    public required string FileType { get; set; }
    public required string Path { get; set; }
    public long FileSize { get; set; }
    public DateTimeOffset UploadedAtUtc { get; set; }
    public Guid UploadedBy { get; set; }
}
