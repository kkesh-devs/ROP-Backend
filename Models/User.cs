using KKESH_ROP.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KKESH_ROP.Models;

public class User
{
    [BsonId]
    public ObjectId _id { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }
    
    [BsonElement("password")]
    public string Password { get; set; }
    
    public UserRoleEnum Role { get; set; }

    [BsonElement("status")]
    public UserStatusEnum Status { get; set; }
    
    [BsonElement("createdBy")]
    public string CreatedBy { get; set; }
    
    [BsonElement("updatedBy")]
    public string UpdatedBy { get; set; }

    [BsonElement("timestamp")]
    public Timestamp Timestamp { get; set; }
}