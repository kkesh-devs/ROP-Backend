using AutoMapper;
using KKESH_ROP.Data;
using KKESH_ROP.DTO.Diagnose;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Models;
using MongoDB.Bson;

namespace KKESH_ROP.Repositories;

public class DiagnoseRepository(ApplicationDbContext context, IMapper mapper) : IDiagnoseRepository
{
    
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<List<DiagnoseDto>>> GetAllAsync()
    {
        try
        {
            var items = await context.Diagnoses.ToListAsync();
            var result = mapper.Map<List<DiagnoseDto>>(items);
            return new Response<List<DiagnoseDto>>(true, "Data retrieved successfully", result);
        }
        catch (Exception ex)
        {
            return new Response<List<DiagnoseDto>>(false, "Error: " + ex.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<DiagnoseDto>> GetByIdAsync(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return new Response<DiagnoseDto>(false, "Invalid ID format", null);

        var item = await context.Diagnoses.FirstOrDefaultAsync(x => x._id == objectId);
        if (item == null)
            return new Response<DiagnoseDto>(false, "Diagnose not found", null);

        var result = mapper.Map<DiagnoseDto>(item);
        return new Response<DiagnoseDto>(true, "Data retrieved successfully", result);
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<DiagnoseDto>> CreateAsync(CreateDiagnoseDto dto)
    {
        try
        {
            var diagnose = mapper.Map<Diagnose>(dto);
            await context.Diagnoses.AddAsync(diagnose);
            await context.SaveChangesAsync();

            var result = mapper.Map<DiagnoseDto>(diagnose);
            return new Response<DiagnoseDto>(true, "Diagnose Created successfully", result);
        }
        catch (Exception ex)
        {
            return new Response<DiagnoseDto>(false, "Error: " + ex.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<bool>> UpdateAsync(string id, UpdateDiagnoseDto dto)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return new Response<bool>(false, "Invalid ID format", false);

        var diagnose = await context.Diagnoses.FirstOrDefaultAsync(x => x._id == objectId);
        if (diagnose == null)
            return new Response<bool>(false, "Diagnose not found", false);

        mapper.Map(dto, diagnose);
        context.Diagnoses.Update(diagnose);
        await context.SaveChangesAsync();

        return new Response<bool>(true, "Diagnose updated successfully", true);
    }
//____________________________________________________________________________________________________________________________________________________

}