using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using KKESH_ROP.Enums;

namespace KKESH_ROP.Models;

public class Screener
{
    [BsonId]
    public ObjectId _id { get; set; }

    [BsonElement("firstName")]
    public string FirstName { get; set; }
    
    [BsonElement("middleName")]
    public string MiddleName { get; set; }
    
    [BsonElement("lastName")]
    public string LastName { get; set; }

    [BsonElement("mobile")]
    public string Mobile { get; set; }
    
    [BsonElement("country")]
    public string Country { get; set; }
    
    [BsonElement("city")]
    public string City { get; set; }
    
    [BsonElement("hospitalId")]
    public string HospitalId { get; set; }
    
    [BsonElement("userId")]
    public string UserId { get; set; }
    
    [BsonElement("status")]
    public ScreenerStatusEnum Status { get; set; } = ScreenerStatusEnum.Active;
}