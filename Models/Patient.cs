using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KKESH_ROP.Models;

public class Patient {
    
    [BsonId]
    public ObjectId _id { get; set; }

    [BsonElement("nationalId")]
    public string NationalId { get; set; }

    [BsonElement("fatherFirstName")]
    public string FatherFirstName { get; set; }

    [BsonElement("fatherMiddleName")]
    public string FatherMiddleName { get; set; }

    [BsonElement("fatherLastName")]
    public string FatherLastName { get; set; }

    [BsonElement("gender")]
    public string Gender { get; set; }

    [BsonElement("mobile")]
    public string Mobile { get; set; }

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

    [BsonElement("nationality")]
    public string Nationality { get; set; }

    [BsonElement("createdBy")]
    public ObjectId CreatedBy { get; set; }

    [BsonElement("updateBy")]
    public ObjectId UpdateBy { get; set; }

    [BsonElement("timestamp")]
    public Timestamp Timestamp { get; set; }
}