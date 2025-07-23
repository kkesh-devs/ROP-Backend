using AutoMapper;
using KKESH_ROP.Data;
using KKESH_ROP.DTO.Physician;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Models;
using MongoDB.Bson;

namespace KKESH_ROP.Repositories;

public class PhysicianRepository(IMapper mapper, ApplicationDbContext context) : IPhysicianRepository
{

//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<List<PhysicianDto>>> GetAllAsync()
    {
        try
        {
            var physicians = await context.Physicians.ToListAsync();
            var result = mapper.Map<List<PhysicianDto>>(physicians);
            return new Response<List<PhysicianDto>>(true, "Data retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<List<PhysicianDto>>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<PhysicianDto>> GetByIdAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<PhysicianDto>(false, "Invalid ID format", null);

            var physician = await context.Physicians.FirstOrDefaultAsync(x => x._id == objectId);
            if (physician == null)
                return new Response<PhysicianDto>(false, "Physician not found", null);

            var result = mapper.Map<PhysicianDto>(physician);
            return new Response<PhysicianDto>(true, "Data retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<PhysicianDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<PhysicianDto>> CreateAsync(CreatePhysicianDto dto)
    {
        try
        {
            var physician = mapper.Map<Physician>(dto);
            await context.Physicians.AddAsync(physician);
            await context.SaveChangesAsync();

            var result = mapper.Map<PhysicianDto>(physician);
            return new Response<PhysicianDto>(true, "Physician created successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<PhysicianDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<bool>> UpdateAsync(string id, UpdatePhysicianDto dto)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<bool>(false, "Invalid ID format", false);

            var physician = await context.Physicians.FirstOrDefaultAsync(x => x._id == objectId);
            if (physician == null)
                return new Response<bool>(false, "Physician not found", false);

            mapper.Map(dto, physician);
            context.Physicians.Update(physician);
            await context.SaveChangesAsync();

            return new Response<bool>(true, "Physician updated successfully", true);
        }
        catch (Exception exception)
        {
            return new Response<bool>(false, "Error " + exception.Message, false);
        }
    }
//____________________________________________________________________________________________________________________________________________________

}
