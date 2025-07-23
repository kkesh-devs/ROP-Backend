using AutoMapper;
using KKESH_ROP.Data;
using KKESH_ROP.DTO.Consultant;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Models;
using MongoDB.Bson;

namespace KKESH_ROP.Repositories;

public class ConsultantRepository(IMapper mapper, ApplicationDbContext context) : IConsultantRepository
{

//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<List<ConsultantDto>>> GetAllAsync()
    {
        try
        {
            var consultants = await context.Consultants.ToListAsync();
            var result = mapper.Map<List<ConsultantDto>>(consultants);
            return new Response<List<ConsultantDto>>(true, "Data retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<List<ConsultantDto>>(true, "Error " + exception.Message, null);
        }
    }

//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<ConsultantDto>> GetByIdAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<ConsultantDto>(false, "Invalid ID format", null);

            var consultant = await context.Consultants.FirstOrDefaultAsync(x => x._id == objectId);
            if (consultant == null)
                return new Response<ConsultantDto>(false, "Consultant not found", null);

            var result = mapper.Map<ConsultantDto>(consultant);
            return new Response<ConsultantDto>(true, "Data retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<ConsultantDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<ConsultantDto>> CreateAsync(CreateConsultantDto dto)
    {
        try
        {
            var consultant = mapper.Map<Consultant>(dto);
            await context.Consultants.AddAsync(consultant);
            await context.SaveChangesAsync();

            var result = mapper.Map<ConsultantDto>(consultant);
            return new Response<ConsultantDto>(true, "Consultant created successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<ConsultantDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<bool>> UpdateAsync(string id, UpdateConsultantDto dto)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<bool>(false, "Invalid ID format", false);

            var consultant = await context.Consultants.FirstOrDefaultAsync(x => x._id == objectId);
            if (consultant == null)
                return new Response<bool>(false, "Consultant not found", false);

            mapper.Map(dto, consultant);
            context.Consultants.Update(consultant);
            await context.SaveChangesAsync();

            return new Response<bool>(true, "Consultant updated successfully", true);
        }
        catch (Exception exception)
        {
            return new Response<bool>(false, "Error " + exception.Message, false);
        }
    }
//____________________________________________________________________________________________________________________________________________________

}
