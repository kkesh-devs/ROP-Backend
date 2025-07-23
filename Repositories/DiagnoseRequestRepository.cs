using AutoMapper;
using KKESH_ROP.Data;
using KKESH_ROP.DTO.DiagnoseRequest;
using KKESH_ROP.Enums;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Models;
using MongoDB.Bson;

namespace KKESH_ROP.Repositories;

public class DiagnoseRequestRepository(IMapper mapper, ApplicationDbContext context) : IDiagnoseRequestRepository
{

//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<List<DiagnoseRequestDto>>> GetAllAsync()
    {
        try
        {
            var diagnoseRequests = await context.DiagnoseRequests.ToListAsync();
            var result = mapper.Map<List<DiagnoseRequestDto>>(diagnoseRequests);
            return new Response<List<DiagnoseRequestDto>>(true, "Data retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<List<DiagnoseRequestDto>>(true, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<DiagnoseRequestDto>> GetByIdAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<DiagnoseRequestDto>(false, "Invalid ID format", null);

            var diagnoseRequest = await context.DiagnoseRequests.FirstOrDefaultAsync(x => x._id == objectId);
            if (diagnoseRequest == null)
                return new Response<DiagnoseRequestDto>(false, "Diagnose Request not found", null);

            var result = mapper.Map<DiagnoseRequestDto>(diagnoseRequest);
            return new Response<DiagnoseRequestDto>(true, "Data retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<DiagnoseRequestDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<DiagnoseRequestDto>> CreateAsync(CreateDiagnoseRequestDto dto)
    {
        try
        {
            var diagnoseRequest = mapper.Map<DiagnoseRequest>(dto);
            await context.DiagnoseRequests.AddAsync(diagnoseRequest);
            await context.SaveChangesAsync();

            var result = mapper.Map<DiagnoseRequestDto>(diagnoseRequest);
            return new Response<DiagnoseRequestDto>(true, "Diagnose Request created successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<DiagnoseRequestDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<string>> UpdateRequestStatus(string id, DiagnoseReqStatusEnum status)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<string>(false, "Invalid ID format", null);

            var diagnoseRequest = await context.DiagnoseRequests.Include(
                diagnoseRequest => diagnoseRequest.Timestamp).FirstOrDefaultAsync(x => x._id == objectId);
            
            if (diagnoseRequest == null)
                return new Response<string>(false, "Diagnose Request not found", null);
            
            diagnoseRequest.Status = status;
            diagnoseRequest.Timestamp.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();

            return new Response<string>(true, "Status updated successfully", null);
        }
        catch (Exception exception)
        {
            return new Response<string>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________
}
