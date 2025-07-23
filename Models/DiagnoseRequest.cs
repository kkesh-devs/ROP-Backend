using KKESH_ROP.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KKESH_ROP.Models;

public class DiagnoseRequest
{
    [BsonId]
    public ObjectId _id { get; set; }

    [BsonElement("requestNumber")]
    public int RequestNumber { get; set; }

    [BsonElement("status")]
    public DiagnoseReqStatusEnum Status { get; set; }

    [BsonElement("medicalId")]
    public string MedicalId { get; set; }
    
    [BsonElement("visitId")]
    public string VisitId { get; set; }

    [BsonElement("diagnoseId")]
    public string DiagnoseId { get; set; }

    [BsonElement("assignedTo")]
    public string AssignedTo { get; set; }

    [BsonElement("secondOpinionDrId")]
    public string SecondOpinionDrId { get; set; }

    [BsonElement("createdBy")]
    public string CreatedBy { get; set; }

    [BsonElement("updatedBy")]
    public string UpdatedBy { get; set; }

    [BsonElement("timestamp")]
    public Timestamp Timestamp { get; set; }
}