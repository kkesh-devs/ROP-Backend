using KKESH_ROP.Enums;

namespace KKESH_ROP.DTO.JoinPhysician;

public class JoinPhysicianWithUserDto
{
    public string? Id { get; set; }
    public string UserId { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string HospitalName  { get; set; }
    public string UserEmail { get; set; }
    public string UserMobile { get; set; }
    public string UserCountry { get; set; }
    public string UserCity { get; set; }
    public JoinPhysicianStatus Status { get; set; }
    public string? RejectionReason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? ProcessedBy { get; set; }
}
