using KKESH_ROP.DTO.JoinMedicalCenter;
using KKESH_ROP.Enums;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KKESH_ROP.Repositories;

public class JoinMedicalCenterRepository : IJoinMedicalCenterRepository
{
    private readonly IMongoCollection<JoinMedicalCenterRequests> _collection;

    public JoinMedicalCenterRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<JoinMedicalCenterRequests>("JoinMedicalCenterRequests");
    }

    public async Task<Response<List<JoinMedicalCenterRequestDto>>> GetAllAsync()
    {
        try
        {
            var requests = await _collection.Find(_ => true).ToListAsync();
            var dtos = requests.Select(MapToDto).ToList();
            return new Response<List<JoinMedicalCenterRequestDto>>(true, "Requests retrieved successfully", dtos);
        }
        catch (Exception ex)
        {
            return new Response<List<JoinMedicalCenterRequestDto>>(false, ex.Message, null);
        }
    }

    public async Task<Response<JoinMedicalCenterRequestDto>> GetByIdAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<JoinMedicalCenterRequestDto>(false, "Invalid ID format", null);

            var request = await _collection.Find(x => x._id == objectId).FirstOrDefaultAsync();
            if (request == null)
                return new Response<JoinMedicalCenterRequestDto>(false, "Request not found", null);

            var dto = MapToDto(request);
            return new Response<JoinMedicalCenterRequestDto>(true, "Request retrieved successfully", dto);
        }
        catch (Exception ex)
        {
            return new Response<JoinMedicalCenterRequestDto>(false, ex.Message, null);
        }
    }

    public async Task<Response<JoinMedicalCenterRequestDto>> GetByUserIdAsync(string userId)
    {
        try
        {
            var request = await _collection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
            if (request == null)
                return new Response<JoinMedicalCenterRequestDto>(false, "Request not found", null);

            var dto = MapToDto(request);
            return new Response<JoinMedicalCenterRequestDto>(true, "Request retrieved successfully", dto);
        }
        catch (Exception ex)
        {
            return new Response<JoinMedicalCenterRequestDto>(false, ex.Message, null);
        }
    }

    public async Task<Response<List<JoinMedicalCenterRequestDto>>> GetByStatusAsync(JoinMedicalCenterStatus status)
    {
        try
        {
            var requests = await _collection.Find(x => x.Status == status).ToListAsync();
            var dtos = requests.Select(MapToDto).ToList();
            return new Response<List<JoinMedicalCenterRequestDto>>(true, "Requests retrieved successfully", dtos);
        }
        catch (Exception ex)
        {
            return new Response<List<JoinMedicalCenterRequestDto>>(false, ex.Message, null);
        }
    }

    public async Task<Response<List<JoinMedicalCenterRequestDto>>> GetPendingRequestsAsync()
    {
        return await GetByStatusAsync(JoinMedicalCenterStatus.Pending);
    }

    public async Task<Response<JoinMedicalCenterRequestDto>> CreateAsync(CreateJoinMedicalCenterRequestDto dto)
    {
        try
        {
            // Check if user already has a pending request
            var existingRequest = await _collection.Find(x => x.UserId == dto.UserId && x.Status == JoinMedicalCenterStatus.Pending).FirstOrDefaultAsync();
            if (existingRequest != null)
                return new Response<JoinMedicalCenterRequestDto>(false, "User already has a pending request", null);

            var request = new JoinMedicalCenterRequests
            {
                UserId = dto.UserId,
                Status = JoinMedicalCenterStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _collection.InsertOneAsync(request);
            var responseDto = MapToDto(request);
            return new Response<JoinMedicalCenterRequestDto>(true, "Request created successfully", responseDto);
        }
        catch (Exception ex)
        {
            return new Response<JoinMedicalCenterRequestDto>(false, ex.Message, null);
        }
    }

    public async Task<Response<string>> UpdateAsync(string id, UpdateJoinMedicalCenterDto dto)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<string>(false, "Invalid ID format", null);

            var update = Builders<JoinMedicalCenterRequests>.Update;
            var updates = new List<UpdateDefinition<JoinMedicalCenterRequests>>();

            if (dto.Status.HasValue)
                updates.Add(update.Set(x => x.Status, dto.Status.Value));

            if (!string.IsNullOrEmpty(dto.RejectionReason))
                updates.Add(update.Set(x => x.RejectionReason, dto.RejectionReason));

            if (!string.IsNullOrEmpty(dto.ProcessedBy))
                updates.Add(update.Set(x => x.ProcessedBy, dto.ProcessedBy));

            if (updates.Any())
            {
                var combinedUpdate = update.Combine(updates);
                var result = await _collection.UpdateOneAsync(x => x._id == objectId, combinedUpdate);

                if (result.MatchedCount == 0)
                    return new Response<string>(false, "Request not found", null);

                return new Response<string>(true, "Request updated successfully", id);
            }

            return new Response<string>(false, "No fields to update", null);
        }
        catch (Exception ex)
        {
            return new Response<string>(false, ex.Message, null);
        }
    }

    public async Task<Response<string>> ProcessAsync(string id, ProcessJoinMedicalCenterDto dto)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<string>(false, "Invalid ID format", null);

            var update = Builders<JoinMedicalCenterRequests>.Update
                .Set(x => x.Status, dto.Status)
                .Set(x => x.ProcessedAt, DateTime.UtcNow)
                .Set(x => x.ProcessedBy, dto.ProcessedBy);

            if (!string.IsNullOrEmpty(dto.RejectionReason))
                update = update.Set(x => x.RejectionReason, dto.RejectionReason);

            var result = await _collection.UpdateOneAsync(x => x._id == objectId, update);

            if (result.MatchedCount == 0)
                return new Response<string>(false, "Request not found", null);

            return new Response<string>(true, "Request processed successfully", id);
        }
        catch (Exception ex)
        {
            return new Response<string>(false, ex.Message, null);
        }
    }

    public async Task<Response<string>> DeleteAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<string>(false, "Invalid ID format", null);

            var result = await _collection.DeleteOneAsync(x => x._id == objectId);

            if (result.DeletedCount == 0)
                return new Response<string>(false, "Request not found", null);

            return new Response<string>(true, "Request deleted successfully", id);
        }
        catch (Exception ex)
        {
            return new Response<string>(false, ex.Message, null);
        }
    }

    private static JoinMedicalCenterRequestDto MapToDto(JoinMedicalCenterRequests request)
    {
        return new JoinMedicalCenterRequestDto
        {
            Id = request._id.ToString(),
            UserId = request.UserId,
            Status = request.Status,
            RejectionReason = request.RejectionReason,
            CreatedAt = request.CreatedAt,
            ProcessedAt = request.ProcessedAt,
            ProcessedBy = request.ProcessedBy
        };
    }
}
