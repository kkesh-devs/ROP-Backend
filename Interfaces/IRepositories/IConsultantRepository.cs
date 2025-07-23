using KKESH_ROP.DTO.Consultant;
using KKESH_ROP.Helpers;

namespace KKESH_ROP.Interfaces.IRepositories;

public interface IConsultantRepository
{
    Task<Response<List<ConsultantDto>>> GetAllAsync();
    Task<Response<ConsultantDto>> GetByIdAsync(string id);
    Task<Response<ConsultantDto>> CreateAsync(CreateConsultantDto dto);
    Task<Response<bool>> UpdateAsync(string id, UpdateConsultantDto dto);
}