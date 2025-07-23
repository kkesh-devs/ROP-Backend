
namespace KKESH_ROP.DTO.Patient;


public class CreatePatientDto
{
    public string NationalId { get; set; }
    public string MotherFirstName { get; set; }
    public string MotherMiddleName { get; set; }
    public string MotherLastName { get; set; }
    public string FatherFirstName { get; set; }
    public string FatherMiddleName { get; set; }
    public string FatherLastName { get; set; }
    public string Gender { get; set; }
    public string MotherMobile { get; set; }
    public string FatherMobile { get; set; }
    public string Email { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public DateTime BirthDate { get; set; }
    public string HospitalId { get; set; }
    public string MedicalId { get; set; }
    public string Nationality { get; set; }
    public int BirthWeight { get; set; }
    public int BirthGestationalAge { get; set; }
    public bool RDS { get; set; }
    public bool PDA { get; set; }
    public bool Hydrocephalus { get; set; }
    public string Neonatologist { get; set; }
    public int BirthOrder { get; set; }
    public string BirthType { get; set; }
    public string CreatedBy { get; set; } // Assuming ObjectId will be string in DTO
}


public class RetrievePatientDto
{
    public string Id { get; set; }
    public string NationalId { get; set; }
    public string MotherFirstName { get; set; }
    public string MotherMiddleName { get; set; }
    public string MotherLastName { get; set; }
    public string FatherFirstName { get; set; }
    public string FatherMiddleName { get; set; }
    public string FatherLastName { get; set; }
    public string Gender { get; set; }
    public string MotherMobile { get; set; }
    public string FatherMobile { get; set; }
    public string Email { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public DateTime BirthDate { get; set; }
    public string HospitalId { get; set; }
    public string MedicalId { get; set; }
    public string Nationality { get; set; }
    public int BirthWeight { get; set; }
    public int BirthGestationalAge { get; set; }
    public bool RDS { get; set; }
    public bool PDA { get; set; }
    public bool Hydrocephalus { get; set; }
    public string Neonatologist { get; set; }
    public int BirthOrder { get; set; }
    public string BirthType { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class UpdatePatientDto
{
    public string MotherFirstName { get; set; }
    public string MotherMiddleName { get; set; }
    public string MotherLastName { get; set; }
    public string FatherFirstName { get; set; }
    public string FatherMiddleName { get; set; }
    public string FatherLastName { get; set; }
    public string Gender { get; set; }
    public string MotherMobile { get; set; }
    public string FatherMobile { get; set; }
    public string Email { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public DateTime BirthDate { get; set; }
    public string HospitalId { get; set; }
    public string MedicalId { get; set; }
    public string Nationality { get; set; }
    public int BirthWeight { get; set; }
    public int BirthGestationalAge { get; set; }
    public bool RDS { get; set; }
    public bool PDA { get; set; }
    public bool Hydrocephalus { get; set; }
    public string Neonatologist { get; set; }
    public int BirthOrder { get; set; }
    public string BirthType { get; set; }
    public string UpdatedBy { get; set; }
}
