using KKESH_ROP.Enums;

namespace KKESH_ROP.DTO.Physician;

public class CreatePhysicianDto
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string HospitalName { get; set; }
    public string UserId { get; set; }
    public PhysicianStatus Status { get; set; }

}

public class UpdatePhysicianDto
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string HospitalName { get; set; }
    public PhysicianStatus? Status { get; set; }
}


public class PhysicianDto
{
    public string Id { get; set; } // from ObjectId
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string HospitalName { get; set; }
    public string UserId { get; set; }
    public PhysicianStatus Status { get; set; }

}




