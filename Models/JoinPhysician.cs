using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using KKESH_ROP.Enums;

namespace KKESH_ROP.Models;

public class JoinPhysician
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId _id { get; set; }
    
    [BsonElement("userId")]
    public string UserId { get; set; }
    
    [BsonElement("status")]
    public JoinPhysicianStatus Status { get; set; } = JoinPhysicianStatus.Pending;
    
    [BsonElement("rejectionReason")]
    public string? RejectionReason { get; set; }
    
    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [BsonElement("processedAt")]
    public DateTime? ProcessedAt { get; set; }
    
    [BsonElement("processedBy")]
    public string? ProcessedBy { get; set; }
}
