using KKESH_ROP.Enums;
using KKESH_ROP.Models;

namespace KKESH_ROP.DTO.DiagnoseRequest;

public class CreateDiagnoseRequestDto
{
    public int RequestNumber { get; set; }
    public string MedicalId { get; set; }
    public string DiagnoseId { get; set; }
    public string AssignedTo { get; set; }
    public string SecondOpinionDrId { get; set; }
    public string CreatedBy { get; set; }
    public string ParentId { get; set; } // Can be null for initial requests
}

public class DiagnoseRequestDto
{
    public string Id { get; set; }
    public int RequestNumber { get; set; }
    public DiagnoseReqStatusEnum Status { get; set; }
    public string MedicalId { get; set; }
    public string DiagnoseId { get; set; }
    public string VisitId { get; set; }
    public string AssignedTo { get; set; }
    public string SecondOpinionDrId { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public string ParentId { get; set; } // Can be null for initial requests
    public Timestamp Timestamp { get; set; }
}
