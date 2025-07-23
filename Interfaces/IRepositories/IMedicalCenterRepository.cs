using KKESH_ROP.DTO.MedicalCenter;
using KKESH_ROP.Helpers;

namespace KKESH_ROP.Interfaces.IRepositories;

public interface IMedicalCenterRepository
{
    Task<Response<List<MedicalCenterDto>>> GetAllAsync();
    Task<Response<MedicalCenterDto>> GetByIdAsync(string id);
    Task<Response<MedicalCenterDto>> CreateAsync(CreateMedicalCenterDto dto);
    Task<Response<bool>> UpdateAsync(string id, UpdateMedicalCenterDto dto);
}