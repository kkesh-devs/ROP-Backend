using AutoMapper;
using KKESH_ROP.DTO.DiagnoseRequest;
using KKESH_ROP.Enums;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KKESH_ROP.Repositories;

public class DiagnoseRequestRepository : IDiagnoseRequestRepository
{
    private readonly IMongoCollection<DiagnoseRequest> _diagnoseRequests;
    private readonly IMapper _mapper;

    public DiagnoseRequestRepository(IMongoDatabase database, IMapper mapper)
    {
        _diagnoseRequests = database.GetCollection<DiagnoseRequest>("DiagnoseRequests");
        _mapper = mapper;
    }

    public async Task<Response<List<DiagnoseRequestDto>>> GetAllAsync()
    {
        try
        {
            var requests = await _diagnoseRequests.Find(_ => true).ToListAsync();
            var dtos = _mapper.Map<List<DiagnoseRequestDto>>(requests);
            return new Response<List<DiagnoseRequestDto>>(true, "Requests retrieved successfully", dtos);
        }
        catch (Exception ex)
        {
            return new Response<List<DiagnoseRequestDto>>(false, ex.Message, null);
        }
    }

    public async Task<Response<DiagnoseRequestDto>> GetByIdAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<DiagnoseRequestDto>(false, "Invalid ID format", null);

            var request = await _diagnoseRequests.Find(x => x._id == objectId).FirstOrDefaultAsync();
            if (request == null)
                return new Response<DiagnoseRequestDto>(false, "Request not found", null);

            var dto = _mapper.Map<DiagnoseRequestDto>(request);
            return new Response<DiagnoseRequestDto>(true, "Request retrieved successfully", dto);
        }
        catch (Exception ex)
        {
            return new Response<DiagnoseRequestDto>(false, ex.Message, null);
        }
    }

    public async Task<Response<DiagnoseRequestDto>> CreateAsync(CreateDiagnoseRequestDto dto)
    {
        try
        {
            var request = _mapper.Map<DiagnoseRequest>(dto);
            await _diagnoseRequests.InsertOneAsync(request);
            
            var responseDto = _mapper.Map<DiagnoseRequestDto>(request);
            return new Response<DiagnoseRequestDto>(true, "Request created successfully", responseDto);
        }
        catch (Exception ex)
        {
            return new Response<DiagnoseRequestDto>(false, ex.Message, null);
        }
    }

    public async Task<Response<string>> UpdateRequestStatus(string id, DiagnoseReqStatusEnum status)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<string>(false, "Invalid ID format", null);

            var update = Builders<DiagnoseRequest>.Update
                .Set(x => x.Status, status)
                .Set(x => x.Timestamp.UpdatedAt, DateTime.UtcNow);

            var result = await _diagnoseRequests.UpdateOneAsync(x => x._id == objectId, update);

            if (result.MatchedCount == 0)
                return new Response<string>(false, "Request not found", null);

            return new Response<string>(true, "Request status updated successfully", id);
        }
        catch (Exception ex)
        {
            return new Response<string>(false, ex.Message, null);
        }
    }

    public async Task<Response<List<DiagnoseRequestDto>>> GetChildrenByParentIdAsync(string parentId)
    {
        try
        {
            var requests = await _diagnoseRequests.Find(x => x.ParentId == parentId).ToListAsync();
            var dtos = _mapper.Map<List<DiagnoseRequestDto>>(requests);
            return new Response<List<DiagnoseRequestDto>>(true, "Child requests retrieved successfully", dtos);
        }
        catch (Exception ex)
        {
            return new Response<List<DiagnoseRequestDto>>(false, ex.Message, null);
        }
    }

    public async Task<Response<List<DiagnoseRequestDto>>> GetByHospitalIdAsync(string hospitalId)
    {
        try
        {
            var requests = await _diagnoseRequests.Find(x => x.HospitalId == hospitalId).ToListAsync();
            var dtos = _mapper.Map<List<DiagnoseRequestDto>>(requests);
            return new Response<List<DiagnoseRequestDto>>(true, "Hospital requests retrieved successfully", dtos);
        }
        catch (Exception ex)
        {
            return new Response<List<DiagnoseRequestDto>>(false, ex.Message, null);
        }
    }

    // Status-specific methods
    public async Task<Response<string>> SetStatusInProgress(string id) => 
        await UpdateRequestStatus(id, DiagnoseReqStatusEnum.InProgress);

    public async Task<Response<string>> SetStatusNotPossible(string id) => 
        await UpdateRequestStatus(id, DiagnoseReqStatusEnum.NotPossible);

    public async Task<Response<string>> SetStatusSecondOpinionRequired(string id) => 
        await UpdateRequestStatus(id, DiagnoseReqStatusEnum.SecondOpinionRequired);

    public async Task<Response<string>> SetStatusFollowUpScheduled(string id) => 
        await UpdateRequestStatus(id, DiagnoseReqStatusEnum.FollowUpScheduled);

    public async Task<Response<string>> SetStatusRetakeImaging(string id) => 
        await UpdateRequestStatus(id, DiagnoseReqStatusEnum.RetakeImaging);

    public async Task<Response<string>> SetStatusPostImagingReview(string id) => 
        await UpdateRequestStatus(id, DiagnoseReqStatusEnum.PostImagingReview);

    public async Task<Response<string>> SetStatusCompleted(string id) => 
        await UpdateRequestStatus(id, DiagnoseReqStatusEnum.Completed);

    public async Task<Response<string>> SetStatusNeglected(string id) => 
        await UpdateRequestStatus(id, DiagnoseReqStatusEnum.Neglected);

    public async Task<Response<string>> SetStatusCancelled(string id) => 
        await UpdateRequestStatus(id, DiagnoseReqStatusEnum.Cancelled);
}
