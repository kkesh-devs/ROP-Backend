using KKESH_ROP.DTO.Screener;
using KKESH_ROP.Helpers;

namespace KKESH_ROP.Interfaces.IRepositories;

public interface IScreenerRepository
{
    Task<Response<List<ScreenerDto>>> GetAllAsync();
    Task<Response<ScreenerDto>> GetByIdAsync(string id);
    Task<Response<ScreenerDto>> CreateAsync(CreateScreenerDto dto);
    Task<Response<bool>> UpdateAsync(string id, UpdateScreenerDto dto);
}