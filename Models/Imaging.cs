using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KKESH_ROP.Models;

public class PatientExam
{
    [BsonId]
    public ObjectId _id { get; set; }
    
    [BsonElement("ExamId")]
    public string ExamId { get; set; }
    
    [BsonElement("PatientMRN")]
    public string PatientMRN { get; set; }
    
    [BsonElement("ExamDate")]
    public DateTime ExamDate { get; set; }
    
    [BsonElement("ExamType")]
    public string ExamType { get; set; }
    
    [BsonElement("Status")]
    public string Status { get; set; }
    
    [BsonElement("Images")]
    public List<ExamImage> Images { get; set; } = new();
    
    [BsonElement("Timestamp")]
    public Timestamp Timestamp { get; set; } = new();
}

public class ExamImage
{
    [BsonElement("ImagePath")]
    public string ImagePath { get; set; }
    
    [BsonElement("ThumbnailPath")]
    public string ThumbnailPath { get; set; }
    
    [BsonElement("ImageName")]
    public string ImageName { get; set; }
    
    [BsonElement("CreatedAt")]
    public DateTime CreatedAt { get; set; }
}
