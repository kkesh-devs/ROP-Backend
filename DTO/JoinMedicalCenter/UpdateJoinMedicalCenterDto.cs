using KKESH_ROP.Enums;

namespace KKESH_ROP.DTO.JoinMedicalCenter;

public class UpdateJoinMedicalCenterDto
{
    public JoinMedicalCenterStatus? Status { get; set; }
    public string? RejectionReason { get; set; }
    public string? ProcessedBy { get; set; }
}
