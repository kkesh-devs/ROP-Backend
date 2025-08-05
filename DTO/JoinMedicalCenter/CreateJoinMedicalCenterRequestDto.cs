using System.ComponentModel.DataAnnotations;

namespace KKESH_ROP.DTO.JoinMedicalCenter;

public class CreateJoinMedicalCenterRequestDto
{
    [Required]
    public string UserId { get; set; }
}
