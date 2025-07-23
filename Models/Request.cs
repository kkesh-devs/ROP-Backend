using KKESH_ROP.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KKESH_ROP.Models;

public class Request
{
    [BsonId]
    public ObjectId _id { get; set; }

    [BsonElement("requestType")]
    public string RequestType { get; set; }

    [BsonElement("assignedTo")]
    public string AssignedTo { get; set; }

    [BsonElement("status")]
    public RequestStatusEnum Status { get; set; }
    
    [BsonElement("createdBy")]
    public string CreatedBy { get; set; }
    
    [BsonElement("updatedBy")]
    public string UpdatedBy { get; set; }

    [BsonElement("timestamps")]
    public Timestamp Timestamp { get; set; }
}