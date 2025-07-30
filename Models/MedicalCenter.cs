using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using KKESH_ROP.Enums;

namespace KKESH_ROP.Models;

public class MedicalCenter
{
    [BsonId]
    public ObjectId _id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("mobile")]
    public string Mobile { get; set; }
    
    [BsonElement("country")]
    public string Country { get; set; }
    
    [BsonElement("city")]
    public string City { get; set; }
    
    [BsonElement("userId")]
    public string UserId { get; set; }
    
    [BsonElement("status")]
    public MedicalCenterStatus Status { get; set; } = MedicalCenterStatus.Pending;
}