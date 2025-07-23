using KKESH_ROP.DTO.ProductManager;
using KKESH_ROP.Helpers;

namespace KKESH_ROP.Interfaces.IRepositories;

public interface IProductManagerRepository
{
    Task<Response<List<ProductManagerDto>>> GetAllAsync();
    Task<Response<ProductManagerDto>> GetByIdAsync(string id);
    Task<Response<ProductManagerDto>> CreateAsync(CreateProductManagerDto dto);
    Task<Response<bool>> UpdateAsync(string id, UpdateProductManagerDto dto);
}