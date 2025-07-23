using KKESH_ROP.DTO.Patient;
using KKESH_ROP.Helpers;

namespace KKESH_ROP.Interfaces.IRepositories;

public interface IPatientRepository
{
    Task<Response<List<RetrievePatientDto>>> GetAllAsync();
    Task<Response<RetrievePatientDto>> GetByIdAsync(string id);
    Task<Response<RetrievePatientDto>> CreateAsync(CreatePatientDto dto);
    Task<Response<bool>> UpdateAsync(string id, UpdatePatientDto dto);
}