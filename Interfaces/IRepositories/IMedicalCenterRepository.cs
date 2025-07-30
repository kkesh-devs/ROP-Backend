using KKESH_ROP.DTO.MedicalCenter;
using KKESH_ROP.Helpers;
using KKESH_ROP.Enums;

namespace KKESH_ROP.Interfaces.IRepositories;

public interface IMedicalCenterRepository
{
    Task<Response<List<MedicalCenterDto>>> GetAllAsync();
    Task<Response<MedicalCenterDto>> GetByIdAsync(string id);
    Task<Response<MedicalCenterDto>> GetByUserIdAsync(string userId);
    Task<Response<MedicalCenterDto>> CreateAsync(CreateMedicalCenterDto dto);
    Task<Response<bool>> UpdateAsync(string id, UpdateMedicalCenterDto dto);
    Task<Response<bool>> UpdateStatusAsync(string id, MedicalCenterStatus status);
    Task<Response<bool>> ApproveMedicalCenterAsync(string id);
    Task<Response<bool>> SetIntegrationStatusAsync(string id);
    Task<Response<bool>> SetValidationStatusAsync(string id);
    Task<Response<bool>> ActivateMedicalCenterAsync(string id);
    Task<Response<bool>> SetPendingStatusAsync(string id);
}