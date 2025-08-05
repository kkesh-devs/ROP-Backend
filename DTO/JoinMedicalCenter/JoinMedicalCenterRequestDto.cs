using KKESH_ROP.Enums;

namespace KKESH_ROP.DTO.JoinMedicalCenter;

public class JoinMedicalCenterRequestDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public JoinMedicalCenterStatus Status { get; set; }
    public string? RejectionReason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? ProcessedBy { get; set; }
}
