using KKESH_ROP.Enums;
using KKESH_ROP.DTO.MedicalCenter;

namespace KKESH_ROP.DTO.JoinMedicalCenter;

public class CreateJoinMedicalCenterRequestDto
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public string Mobile { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string RequestComments { get; set; }
}

public class UpdateJoinMedicalCenterDto
{
    public JoinMedicalCenterStatus? Status { get; set; }
    public string? RejectionReason { get; set; }
    public string? ProcessedBy { get; set; }
}

public class ProcessJoinMedicalCenterDto
{
    public JoinMedicalCenterStatus Status { get; set; }
    public string ProcessedBy { get; set; }
    public string? RejectionReason { get; set; }
}

public class ProcessJoinMedicalCenterActionDto
{
    public string ProcessedBy { get; set; }
    public string? Comments { get; set; }
}

public class JoinMedicalCenterRequestDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string MedicalCenterId { get; set; }
    public string RequestComments { get; set; }
    public JoinMedicalCenterStatus Status { get; set; }
    public string? RejectionReason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? ProcessedBy { get; set; }
    public MedicalCenterDto? MedicalCenter { get; set; }
}
