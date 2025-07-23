using KKESH_ROP.DTO.Physician;
using KKESH_ROP.Helpers;

namespace KKESH_ROP.Interfaces.IRepositories;

public interface IPhysicianRepository
{
    Task<Response<List<PhysicianDto>>> GetAllAsync();
    Task<Response<PhysicianDto>> GetByIdAsync(string id);
    Task<Response<PhysicianDto>> CreateAsync(CreatePhysicianDto dto);
    Task<Response<bool>> UpdateAsync(string id, UpdatePhysicianDto dto);
}