using KKESH_ROP.DTO.DiagnoseRequest;
using KKESH_ROP.Enums;
using KKESH_ROP.Helpers;

namespace KKESH_ROP.Interfaces.IRepositories;

public interface IDiagnoseRequestRepository
{
    Task<Response<List<DiagnoseRequestDto>>> GetAllAsync();
    Task<Response<DiagnoseRequestDto>> GetByIdAsync(string id);
    Task<Response<DiagnoseRequestDto>> CreateAsync(CreateDiagnoseRequestDto dto);
    Task<Response<string>> UpdateRequestStatus(string id, DiagnoseReqStatusEnum status);
}