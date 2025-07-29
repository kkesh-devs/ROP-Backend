using KKESH_ROP.DTO.JoinPhysician;
using KKESH_ROP.Enums;
using KKESH_ROP.Helpers;

namespace KKESH_ROP.Interfaces.IRepositories;

public interface IJoinPhysicianRepository
{
    Task<Response<List<JoinPhysicianDto>>> GetAllAsync();
    Task<Response<JoinPhysicianDto>> GetByIdAsync(string id);
    Task<Response<CreateJoinPhysicianResponseDto>> CreateAsync(CreateJoinPhysicianRequestDto dto);
    Task<Response<bool>> UpdateAsync(string id, UpdateJoinPhysicianDto dto);
    Task<Response<bool>> ProcessAsync(string id, ProcessJoinPhysicianDto dto);
    Task<Response<List<JoinPhysicianDto>>> GetByStatusAsync(JoinPhysicianStatus status);
    Task<Response<JoinPhysicianDto>> GetByUserIdAsync(string userId);
    Task<Response<bool>> DeleteAsync(string id);
    Task<Response<List<JoinPhysicianDto>>> GetPendingRequestsAsync();
    Task<Response<JoinPhysicianWithUserDto>> GetJoinRequestWithUserDetailsAsync(string id);
}
