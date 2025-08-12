using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KKESH_ROP.Models;

public class Patient {
    
    [BsonId]
    public ObjectId _id { get; set; }

    [BsonElement("nationalId")]
    public string NationalId { get; set; }

    [BsonElement("motherFirstName")]
    public string MotherFirstName { get; set; }

    [BsonElement("motherMiddleName")]
    public string MotherMiddleName { get; set; }

    [BsonElement("motherLastName")]
    public string MotherLastName { get; set; }

    [BsonElement("fatherFirstName")]
    public string FatherFirstName { get; set; }

    [BsonElement("fatherMiddleName")]
    public string FatherMiddleName { get; set; }

    [BsonElement("fatherLastName")]
    public string FatherLastName { get; set; }

    [BsonElement("gender")]
    public string Gender { get; set; }

    [BsonElement("motherMobile")]
    public string MotherMobile { get; set; }

    [BsonElement("fatherMobile")]
    public string FatherMobile { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("region")]
    public string Region { get; set; }

    [BsonElement("city")]
    public string City { get; set; }

    [BsonElement("birthDate")]
    public DateTime BirthDate { get; set; }

    [BsonElement("hospitalId")]
    public string HospitalId { get; set; }

    [BsonElement("medicalId")]
    public string MedicalId { get; set; }

    [BsonElement("nationality")]
    public string Nationality { get; set; }

    [BsonElement("birthWeight")]
    public int BirthWeight { get; set; }

    [BsonElement("birthGestationalAge")]
    public int BirthGestationalAge { get; set; }

    [BsonElement("rds")]
    public bool RDS { get; set; }

    [BsonElement("pda")]
    public bool PDA { get; set; }

    [BsonElement("hydrocephalus")]
    public bool Hydrocephalus { get; set; }

    [BsonElement("neonatologist")]
    public string Neonatologist { get; set; }

    [BsonElement("birthOrder")]
    public int BirthOrder { get; set; }

    [BsonElement("birthType")]
    public string BirthType { get; set; }

    [BsonElement("createdBy")]
    public ObjectId CreatedBy { get; set; }

    [BsonElement("updateBy")]
    public ObjectId UpdateBy { get; set; }

    [BsonElement("timestamp")]
    public Timestamp Timestamp { get; set; }
}