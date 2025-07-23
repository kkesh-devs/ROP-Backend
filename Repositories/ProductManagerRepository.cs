using AutoMapper;
using KKESH_ROP.Data;
using KKESH_ROP.DTO.ProductManager;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Models;
using MongoDB.Bson;

namespace KKESH_ROP.Repositories;

public class ProductManagerRepository(IMapper mapper, ApplicationDbContext context) : IProductManagerRepository
{

//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<List<ProductManagerDto>>> GetAllAsync()
    {
        try
        {
            var managers = await context.ProductManagers.ToListAsync();
            var result = mapper.Map<List<ProductManagerDto>>(managers);
            return new Response<List<ProductManagerDto>>(true, "Data retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<List<ProductManagerDto>>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<ProductManagerDto>> GetByIdAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<ProductManagerDto>(false, "Invalid ID format", null);

            var manager = await context.ProductManagers.FirstOrDefaultAsync(x => x._id == objectId);
            if (manager == null)
                return new Response<ProductManagerDto>(false, "Product Manager not found", null);

            var result = mapper.Map<ProductManagerDto>(manager);
            return new Response<ProductManagerDto>(true, "Data retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<ProductManagerDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<ProductManagerDto>> CreateAsync(CreateProductManagerDto dto)
    {
        try
        {
            var manager = mapper.Map<ProductManager>(dto);
            await context.ProductManagers.AddAsync(manager);
            await context.SaveChangesAsync();

            var result = mapper.Map<ProductManagerDto>(manager);
            return new Response<ProductManagerDto>(true, "Product Manager created successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<ProductManagerDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<bool>> UpdateAsync(string id, UpdateProductManagerDto dto)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<bool>(false, "Invalid ID format", false);

            var manager = await context.ProductManagers.FirstOrDefaultAsync(x => x._id == objectId);
            if (manager == null)
                return new Response<bool>(false, "Product Manager not found", false);

            mapper.Map(dto, manager);
            context.ProductManagers.Update(manager);
            await context.SaveChangesAsync();

            return new Response<bool>(true, "Product Manager updated successfully", true);
        }
        catch (Exception exception)
        {
            return new Response<bool>(false, "Error " + exception.Message, false);
        }
    }
//____________________________________________________________________________________________________________________________________________________

}
