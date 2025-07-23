namespace KKESH_ROP.DTO.Consultant;

public class CreateConsultantDto
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string HospitalId { get; set; }
    public string UserId { get; set; }
}

public class UpdateConsultantDto
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string HospitalId { get; set; }
}

public class ConsultantDto
{
    public string Id { get; set; } // mapped from ObjectId
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string HospitalId { get; set; }
    public string UserId { get; set; }

}
