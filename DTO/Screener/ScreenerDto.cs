using KKESH_ROP.Enums;

namespace KKESH_ROP.DTO.Screener;

public class CreateScreenerDto
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string HospitalId { get; set; }
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ScreenerStatusEnum Status { get; set; } = ScreenerStatusEnum.Active;
    public bool IsTrained { get; set; } = false;
}

public class UpdateScreenerDto
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string HospitalId { get; set; }
    public string Email { get; set; }
    public ScreenerStatusEnum Status { get; set; }
    public bool IsTrained { get; set; }
}

public class ScreenerDto
{
    public string Id { get; set; } // ObjectId string
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string HospitalId { get; set; }
    public string UserId { get; set; }
    public string Email { get; set; }
    public ScreenerStatusEnum Status { get; set; }
    public bool IsTrained { get; set; }
}