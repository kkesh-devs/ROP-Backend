using KKESH_ROP.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KKESH_ROP.Models;

public class Diagnose
{
    [BsonId]
    public ObjectId _id { get; set; }

    [BsonElement("isOnOxygen")]
    public bool IsOnOxygen { get; set; }

    [BsonElement("onOxygenDays")]
    public int OnOxygenDays { get; set; }

    [BsonElement("weightAtExamination")]
    public int WeightAtExamination { get; set; }

    [BsonElement("pmaAtExamination")]
    public int PMAAtExamination { get; set; }

    [BsonElement("indomethacin")]
    public bool Indomethacin { get; set; }

    [BsonElement("ivh")]
    public bool IVH { get; set; }

    [BsonElement("sepsis")]
    public bool Sepsis { get; set; }

    [BsonElement("vitaminE")]
    public bool VitaminE { get; set; }

    [BsonElement("vitaminD")]
    public bool VitaminD { get; set; }

    [BsonElement("seizure")]
    public bool Seizure { get; set; }

    [BsonElement("necrotisingEnterocolitis")]
    public bool NecrotisingEnterocolitis { get; set; }

    [BsonElement("bloodTransfusion")]
    public bool BloodTransfusion { get; set; }

    [BsonElement("bloodTransfusionAmount")]
    public int BloodTransfusionAmount { get; set; }

    [BsonElement("medicalHistory")]
    public string MedicalHistory { get; set; }

    [BsonElement("isPatientTreated")]
    public bool IsPatientTreated { get; set; }

    [BsonElement("eyeSelection")]
    public EyeSelection EyeSelection { get; set; }

    [BsonElement("treatmentType")]
    public TreatmentType TreatmentType { get; set; }

    [BsonElement("antiVEGFType")]
    public AntiVEGFType? AntiVEGFType { get; set; }

    [BsonElement("dosage")]
    public string Dosage { get; set; }

    [BsonElement("doctorId")]
    public string DoctorId { get; set; }

    [BsonElement("treatmentDate")]
    public DateTime TreatmentDate { get; set; }
}