using System.ComponentModel.DataAnnotations;
using KKESH_ROP.Enums;

namespace KKESH_ROP.DTO.JoinPhysician;

public class ProcessJoinPhysicianDto
{
    [Required]
    public JoinPhysicianStatus Status { get; set; }
    
    public string? RejectionReason { get; set; }
    
    [Required]
    public string ProcessedBy { get; set; }
}
