using AutoMapper;
using KKESH_ROP.Data;
using KKESH_ROP.DTO.Screener;
using KKESH_ROP.Enums;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Models;
using MongoDB.Bson;

namespace KKESH_ROP.Repositories;

public class ScreenerRepository(IMapper mapper, ApplicationDbContext context) : IScreenerRepository
{

//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<List<ScreenerDto>>> GetAllAsync()
    {
        try
        {
            var screeners = await context.Screeners.ToListAsync();
            var result = mapper.Map<List<ScreenerDto>>(screeners);
            return new Response<List<ScreenerDto>>(true, "Data retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<List<ScreenerDto>>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<ScreenerDto>> GetByIdAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<ScreenerDto>(false, "Invalid ID format", null);

            var screener = await context.Screeners.FirstOrDefaultAsync(x => x._id == objectId);
            if (screener == null)
                return new Response<ScreenerDto>(false, "Screener not found", null);

            var result = mapper.Map<ScreenerDto>(screener);
            return new Response<ScreenerDto>(true, "Data retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<ScreenerDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<ScreenerDto>> CreateAsync(CreateScreenerDto dto)
    {
        try
        {
            var screener = mapper.Map<Screener>(dto);
            await context.Screeners.AddAsync(screener);
            await context.SaveChangesAsync();

            var result = mapper.Map<ScreenerDto>(screener);
            return new Response<ScreenerDto>(true, "Screener created successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<ScreenerDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<bool>> UpdateAsync(string id, UpdateScreenerDto dto)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<bool>(false, "Invalid ID format", false);

            var screener = await context.Screeners.FirstOrDefaultAsync(x => x._id == objectId);
            if (screener == null)
                return new Response<bool>(false, "Screener not found", false);

            mapper.Map(dto, screener);
            context.Screeners.Update(screener);
            await context.SaveChangesAsync();

            return new Response<bool>(true, "Screener updated successfully", true);
        }
        catch (Exception exception)
        {
            return new Response<bool>(false, "Error " + exception.Message, false);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<bool>> UpdateStatusAsync(string id, ScreenerStatusEnum status)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<bool>(false, "Invalid ID format", false);

            var screener = await context.Screeners.FirstOrDefaultAsync(x => x._id == objectId);
            if (screener == null)
                return new Response<bool>(false, "Screener not found", false);

            screener.Status = status;
            context.Screeners.Update(screener);
            await context.SaveChangesAsync();

            var statusMessage = status == ScreenerStatusEnum.Active ? "activated" : "deactivated";
            return new Response<bool>(true, $"Screener {statusMessage} successfully", true);
        }
        catch (Exception exception)
        {
            return new Response<bool>(false, "Error " + exception.Message, false);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<List<ScreenerDto>>> GetByStatusAsync(ScreenerStatusEnum status)
    {
        try
        {
            var screeners = await context.Screeners.Where(x => x.Status == status).ToListAsync();
            var result = mapper.Map<List<ScreenerDto>>(screeners);
            return new Response<List<ScreenerDto>>(true, $"Screeners with status {status} retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<List<ScreenerDto>>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<ScreenerDto>> GetByUserIdAsync(string userId)
    {
        try
        {
            var screener = await context.Screeners.FirstOrDefaultAsync(x => x.UserId == userId);
            if (screener == null)
                return new Response<ScreenerDto>(false, "Screener not found for this user", null);

            var result = mapper.Map<ScreenerDto>(screener);
            return new Response<ScreenerDto>(true, "Screener data retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<ScreenerDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

}
