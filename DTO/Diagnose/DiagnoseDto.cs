using KKESH_ROP.Enums;

namespace KKESH_ROP.DTO.Diagnose;

public class CreateDiagnoseDto
{
    public bool IsOnOxygen { get; set; }
    public int OnOxygenDays { get; set; }
    public int WeightAtExamination { get; set; }
    public int PMAAtExamination { get; set; }
    public bool Indomethacin { get; set; }
    public bool IVH { get; set; }
    public bool Sepsis { get; set; }
    public bool VitaminE { get; set; }
    public bool VitaminD { get; set; }
    public bool Seizure { get; set; }
    public bool NecrotisingEnterocolitis { get; set; }
    public bool BloodTransfusion { get; set; }
    public int BloodTransfusionAmount { get; set; }
    public string MedicalHistory { get; set; }
    public bool IsPatientTreated { get; set; }
    public EyeSelection EyeSelection { get; set; }
    public TreatmentType TreatmentType { get; set; }
    public AntiVEGFType? AntiVEGFType { get; set; }
    public string Dosage { get; set; }
    public string DoctorId { get; set; }
    public DateTime TreatmentDate { get; set; }
}

public class DiagnoseDto : CreateDiagnoseDto
{
    public string Id { get; set; }
}

public class UpdateDiagnoseDto : CreateDiagnoseDto
{
    // Same as create; optionally exclude immutable fields
}
