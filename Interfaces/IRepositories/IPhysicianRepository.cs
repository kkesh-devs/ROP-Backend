using KKESH_ROP.DTO.Physician;
using KKESH_ROP.Helpers;

namespace KKESH_ROP.Interfaces.IRepositories;

public interface IPhysicianRepository
{
    Task<Response<List<PhysicianDto>>> GetAllAsync();
    Task<Response<PhysicianDto>> GetByIdAsync(string id);
    Task<Response<PhysicianDto>> CreateAsync(CreatePhysicianDto dto);
    Task<Response<bool>> UpdateAsync(string id, UpdatePhysicianDto dto);
    Task<Response<bool>> DeleteAsync(string id);
    Task<Response<PhysicianDto>> GetByUserIdAsync(string userId);
    Task<Response<bool>> UpdateByUserIdAsync(string userId, UpdatePhysicianDto dto);
    Task<Response<List<PhysicianDto>>> GetByStatusAsync(string status);
    Task<Response<PhysicianDto>> ApproveAsync(string id);
    Task<Response<PhysicianDto>> RejectAsync(string id);
}