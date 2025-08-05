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
    Task<Response<List<DiagnoseRequestDto>>> GetChildrenByParentIdAsync(string parentId);
    Task<Response<List<DiagnoseRequestDto>>> GetByHospitalIdAsync(string hospitalId);
    
    // Status-specific methods
    Task<Response<string>> SetStatusInProgress(string id);
    Task<Response<string>> SetStatusNotPossible(string id);
    Task<Response<string>> SetStatusSecondOpinionRequired(string id);
    Task<Response<string>> SetStatusFollowUpScheduled(string id);
    Task<Response<string>> SetStatusRetakeImaging(string id);
    Task<Response<string>> SetStatusPostImagingReview(string id);
    Task<Response<string>> SetStatusCompleted(string id);
    Task<Response<string>> SetStatusNeglected(string id);
    Task<Response<string>> SetStatusCancelled(string id);
}