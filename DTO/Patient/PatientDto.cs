namespace KKESH_ROP.DTO.Patient;

public class CreatePatientDto
{
    public string NationalId { get; set; }
    public string FatherFirstName { get; set; }
    public string FatherMiddleName { get; set; }
    public string FatherLastName { get; set; }
    public string Gender { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public DateTime BirthDate { get; set; }
    public string HospitalId { get; set; }
    public string Nationality { get; set; }
    public string CreatedBy { get; set; }
}

public class RetrievePatientDto
{
    public string Id { get; set; }
    public string NationalId { get; set; }
    public string FatherFirstName { get; set; }
    public string FatherMiddleName { get; set; }
    public string FatherLastName { get; set; }
    public string Gender { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public DateTime BirthDate { get; set; }
    public string HospitalId { get; set; }
    public string Nationality { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class UpdatePatientDto
{
    public string FatherFirstName { get; set; }
    public string FatherMiddleName { get; set; }
    public string FatherLastName { get; set; }
    public string Gender { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public DateTime BirthDate { get; set; }
    public string HospitalId { get; set; }
    public string Nationality { get; set; }
    public string UpdatedBy { get; set; }
}
