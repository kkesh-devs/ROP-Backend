using KKESH_ROP.Enums;
using System.ComponentModel.DataAnnotations;

namespace KKESH_ROP.DTO.JoinMedicalCenter;

public class ProcessJoinMedicalCenterDto
{
    [Required]
    public JoinMedicalCenterStatus Status { get; set; }
    
    public string? RejectionReason { get; set; }
    
    [Required]
    public string ProcessedBy { get; set; }
}
