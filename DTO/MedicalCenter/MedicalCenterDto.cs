using KKESH_ROP.Enums;

namespace KKESH_ROP.DTO.MedicalCenter;

public class CreateMedicalCenterDto
{
    public string Name { get; set; }
    public string Mobile { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string UserId { get; set; }
    public MedicalCenterStatus? Status { get; set; }
}

public class UpdateMedicalCenterDto
{
    public string Name { get; set; }
    public string Mobile { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public MedicalCenterStatus? Status { get; set; }
}

public class MedicalCenterDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Mobile { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string UserId { get; set; }
    public MedicalCenterStatus Status { get; set; }
}
