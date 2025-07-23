using KKESH_ROP.Enums;
using KKESH_ROP.Models;

namespace KKESH_ROP.DTO.User;

public class CreateUserDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRoleEnum Role { get; set; }
    public string CreatedBy { get; set; }
}


public class UpdateUserDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRoleEnum Role { get; set; }
    public UserStatusEnum Status { get; set; }
    public string UpdatedBy { get; set; }
}


public class UserDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public UserRoleEnum Role { get; set; }
    public UserStatusEnum Status { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public Timestamp Timestamp { get; set; }
}
