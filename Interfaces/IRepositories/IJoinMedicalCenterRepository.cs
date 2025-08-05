using KKESH_ROP.DTO.JoinMedicalCenter;
using KKESH_ROP.Enums;
using KKESH_ROP.Helpers;

namespace KKESH_ROP.Interfaces.IRepositories;

public interface IJoinMedicalCenterRepository
{
    Task<Response<List<JoinMedicalCenterRequestDto>>> GetAllAsync();
    Task<Response<JoinMedicalCenterRequestDto>> GetByIdAsync(string id);
    Task<Response<JoinMedicalCenterRequestDto>> GetByUserIdAsync(string userId);
    Task<Response<List<JoinMedicalCenterRequestDto>>> GetByStatusAsync(JoinMedicalCenterStatus status);
    Task<Response<List<JoinMedicalCenterRequestDto>>> GetPendingRequestsAsync();
    Task<Response<JoinMedicalCenterRequestDto>> CreateAsync(CreateJoinMedicalCenterRequestDto dto);
    Task<Response<string>> UpdateAsync(string id, UpdateJoinMedicalCenterDto dto);
    Task<Response<string>> ProcessAsync(string id, ProcessJoinMedicalCenterDto dto);
    Task<Response<string>> DeleteAsync(string id);
}
