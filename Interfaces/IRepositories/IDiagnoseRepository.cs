using KKESH_ROP.DTO.Diagnose;
using KKESH_ROP.Helpers;

namespace KKESH_ROP.Interfaces.IRepositories;

public interface IDiagnoseRepository
{
    Task<Response<List<DiagnoseDto>>> GetAllAsync();
    Task<Response<DiagnoseDto>> GetByIdAsync(string id);
    Task<Response<DiagnoseDto>> CreateAsync(CreateDiagnoseDto dto);
    Task<Response<bool>> UpdateAsync(string id, UpdateDiagnoseDto dto);
}