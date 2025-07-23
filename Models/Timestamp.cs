using MongoDB.Bson.Serialization.Attributes;

namespace KKESH_ROP.Models;

public class Timestamp
{
    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}