using System.ComponentModel.DataAnnotations;

namespace KKESH_ROP.DTO.JoinPhysician;

public class CreateJoinPhysicianDto
{
    [Required]
    public string UserId { get; set; }
}
