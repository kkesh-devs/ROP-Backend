using KKESH_ROP.Enums;

namespace KKESH_ROP.DTO.JoinPhysician;

public class CreateJoinPhysicianRequestDto
{

    public string UserId { get; set; }
     public string MiddleName { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string HospitalName  { get; set; }
    public string UserEmail { get; set; }
    public string UserMobile { get; set; }
    public string UserCountry { get; set; }
    public string UserCity { get; set; }
  
}
