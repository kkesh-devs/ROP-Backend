using KKESH_ROP.DTO.Diagnose;

namespace KKESH_ROP.DTO.DiagnoseRequest;

public class CreateDiagnoseRequestWithDiagnoseDto
{
    public CreateDiagnoseRequestDto DiagnoseRequest { get; set; }
    public CreateDiagnoseDto Diagnose { get; set; }
}
