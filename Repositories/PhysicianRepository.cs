using AutoMapper;
using KKESH_ROP.Data;
using KKESH_ROP.DTO.Physician;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Models;
using MongoDB.Bson;
using Microsoft.EntityFrameworkCore;

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

            // Try querying by string representation first (common with MongoDB EF Core provider)
            var physician = await context.Physicians.FirstOrDefaultAsync(x => x._id.ToString() == id);
            
            // If that doesn't work, try with ObjectId comparison
            if (physician == null)
            {
                physician = await context.Physicians.FirstOrDefaultAsync(x => x._id == objectId);
            }

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

            // Try querying by string representation first
            var physician = await context.Physicians.FirstOrDefaultAsync(x => x._id.ToString() == id);
            
            // If that doesn't work, try with ObjectId comparison
            if (physician == null)
            {
                physician = await context.Physicians.FirstOrDefaultAsync(x => x._id == objectId);
            }

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

    public async Task<Response<bool>> DeleteAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<bool>(false, "Invalid ID format", false);

            // Try querying by string representation first
            var physician = await context.Physicians.FirstOrDefaultAsync(x => x._id.ToString() == id);
            
            // If that doesn't work, try with ObjectId comparison
            if (physician == null)
            {
                physician = await context.Physicians.FirstOrDefaultAsync(x => x._id == objectId);
            }

            if (physician == null)
                return new Response<bool>(false, "Physician not found", false);

            context.Physicians.Remove(physician);
            await context.SaveChangesAsync();

            return new Response<bool>(true, "Physician deleted successfully", true);
        }
        catch (Exception exception)
        {
            return new Response<bool>(false, "Error " + exception.Message, false);
        }
    }

//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<PhysicianDto>> GetByUserIdAsync(string userId)
    {
        try
        {
            if (!ObjectId.TryParse(userId, out var userObjectId))
                return new Response<PhysicianDto>(false, "Invalid user ID format", null);

            // Try querying by string representation first
            var physician = await context.Physicians.FirstOrDefaultAsync(x => x.UserId.ToString() == userId);
            
         

            if (physician == null)
                return new Response<PhysicianDto>(false, "Physician not found for this user", null);

            var result = mapper.Map<PhysicianDto>(physician);
            return new Response<PhysicianDto>(true, "Data retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<PhysicianDto>(false, "Error " + exception.Message, null);
        }
    }

//____________________________________________________________________________________________________________________________________________________

    // public async Task<Response<List<PhysicianDto>>> GetByStatusAsync(string status)
    // {
    //     try
    //     {
    //         if (!Enum.TryParse<PhysicianStatus>(status, true, out var physicianStatus))
    //             return new Response<List<PhysicianDto>>(false, "Invalid status value", null);

    //         var physicians = await context.Physicians
    //             .Where(x => x.Status == physicianStatus)
    //             .ToListAsync();
            
    //         var result = mapper.Map<List<PhysicianDto>>(physicians);
    //         return new Response<List<PhysicianDto>>(true, "Data retrieved successfully", result);
    //     }
    //     catch (Exception exception)
    //     {
    //         return new Response<List<PhysicianDto>>(false, "Error " + exception.Message, null);
    //     }
    // }

//____________________________________________________________________________________________________________________________________________________

    // public async Task<Response<PhysicianDto>> ApproveAsync(string id)
    // {
    //     try
    //     {
    //         if (!ObjectId.TryParse(id, out var objectId))
    //             return new Response<PhysicianDto>(false, "Invalid ID format", null);

    //         var physician = await context.Physicians.FirstOrDefaultAsync(x => x._id == objectId);
    //         if (physician == null)
    //             return new Response<PhysicianDto>(false, "Physician not found", null);

    //         physician.Status = PhysicianStatus.Active;
    //         context.Physicians.Update(physician);
    //         await context.SaveChangesAsync();

    //         var result = mapper.Map<PhysicianDto>(physician);
    //         return new Response<PhysicianDto>(true, "Physician approved successfully", result);
    //     }
    //     catch (Exception exception)
    //     {
    //         return new Response<PhysicianDto>(false, "Error " + exception.Message, null);
    //     }
    // }

//____________________________________________________________________________________________________________________________________________________

    // public async Task<Response<PhysicianDto>> RejectAsync(string id)
    // {
    //     try
    //     {
    //         if (!ObjectId.TryParse(id, out var objectId))
    //             return new Response<PhysicianDto>(false, "Invalid ID format", null);

    //         var physician = await context.Physicians.FirstOrDefaultAsync(x => x._id == objectId);
    //         if (physician == null)
    //             return new Response<PhysicianDto>(false, "Physician not found", null);

    //         physician.Status = PhysicianStatus.Rejected;
    //         context.Physicians.Update(physician);
    //         await context.SaveChangesAsync();

    //         var result = mapper.Map<PhysicianDto>(physician);
    //         return new Response<PhysicianDto>(true, "Physician rejected successfully", result);
    //     }
    //     catch (Exception exception)
    //     {
    //         return new Response<PhysicianDto>(false, "Error " + exception.Message, null);
    //     }
    // }

//____________________________________________________________________________________________________________________________________________________

}
