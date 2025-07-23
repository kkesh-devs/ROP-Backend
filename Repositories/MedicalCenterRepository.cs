using AutoMapper;
using KKESH_ROP.Data;
using KKESH_ROP.DTO.MedicalCenter;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Models;
using MongoDB.Bson;

namespace KKESH_ROP.Repositories;

public class MedicalCenterRepository(IMapper mapper, ApplicationDbContext context) : IMedicalCenterRepository
{

//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<List<MedicalCenterDto>>> GetAllAsync()
    {
        try
        {
            var centers = await context.MedicalCenters.ToListAsync();
            var result = mapper.Map<List<MedicalCenterDto>>(centers);
            return new Response<List<MedicalCenterDto>>(true, "Data retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<List<MedicalCenterDto>>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<MedicalCenterDto>> GetByIdAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<MedicalCenterDto>(false, "Invalid ID format", null);

            var center = await context.MedicalCenters.FirstOrDefaultAsync(x => x._id == objectId);
            if (center == null)
                return new Response<MedicalCenterDto>(false, "Medical center not found", null);

            var result = mapper.Map<MedicalCenterDto>(center);
            return new Response<MedicalCenterDto>(true, "Data retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<MedicalCenterDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<MedicalCenterDto>> CreateAsync(CreateMedicalCenterDto dto)
    {
        try
        {
            var center = mapper.Map<MedicalCenter>(dto);
            await context.MedicalCenters.AddAsync(center);
            await context.SaveChangesAsync();

            var result = mapper.Map<MedicalCenterDto>(center);
            return new Response<MedicalCenterDto>(true, "Medical center created successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<MedicalCenterDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<bool>> UpdateAsync(string id, UpdateMedicalCenterDto dto)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<bool>(false, "Invalid ID format", false);

            var center = await context.MedicalCenters.FirstOrDefaultAsync(x => x._id == objectId);
            if (center == null)
                return new Response<bool>(false, "Medical center not found", false);

            mapper.Map(dto, center);
            context.MedicalCenters.Update(center);
            await context.SaveChangesAsync();

            return new Response<bool>(true, "Medical center updated successfully", true);
        }
        catch (Exception exception)
        {
            return new Response<bool>(false, "Error " + exception.Message, false);
        }
    }
//____________________________________________________________________________________________________________________________________________________

}
