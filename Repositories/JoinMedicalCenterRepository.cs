using KKESH_ROP.DTO.JoinMedicalCenter;
using KKESH_ROP.DTO.MedicalCenter;
using KKESH_ROP.Enums;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KKESH_ROP.Repositories;

public class JoinMedicalCenterRepository : IJoinMedicalCenterRepository
{
    private readonly IMongoCollection<JoinMedicalCenterRequests> _joinRequests;
    private readonly IMedicalCenterRepository _medicalCenterRepository;

    public JoinMedicalCenterRepository(IMongoDatabase database, IMedicalCenterRepository medicalCenterRepository)
    {
        _joinRequests = database.GetCollection<JoinMedicalCenterRequests>("JoinMedicalCenterRequests");
        _medicalCenterRepository = medicalCenterRepository;
    }

    public async Task<Response<List<JoinMedicalCenterRequestDto>>> GetAllAsync()
    {
        try
        {
            var requests = await _joinRequests.Find(_ => true).ToListAsync();
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

            var request = await _joinRequests.Find(x => x._id == objectId).FirstOrDefaultAsync();
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
            var request = await _joinRequests.Find(x => x.UserId == userId).FirstOrDefaultAsync();
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
            var requests = await _joinRequests.Find(x => x.Status == status).ToListAsync();
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
            // Check if medical center exists for this user
            var existingMedicalCenterResponse = await _medicalCenterRepository.GetByUserIdAsync(dto.UserId);

            string medicalCenterId;
            MedicalCenterDto medicalCenterData;

            if (!existingMedicalCenterResponse.Success)
            {
                // Medical center doesn't exist, create one
                var createMedicalCenterDto = new CreateMedicalCenterDto
                {
                    Name = dto.Name,
                    Mobile = dto.Mobile,
                    Country = dto.Country,
                    City = dto.City,
                    UserId = dto.UserId
                };

                var createMedicalCenterResponse = await _medicalCenterRepository.CreateAsync(createMedicalCenterDto);
                if (!createMedicalCenterResponse.Success)
                {
                    return new Response<JoinMedicalCenterRequestDto>(false,
                        "Failed to create medical center record", null);
                }

                medicalCenterId = createMedicalCenterResponse.Data.Id;
                medicalCenterData = createMedicalCenterResponse.Data;
            }
            else
            {
                // Medical center exists, check status
                var medicalCenter = existingMedicalCenterResponse.Data;
                medicalCenterId = medicalCenter.Id;
                medicalCenterData = medicalCenter;

                // Check if user can create a request based on current status
                var blockedStatuses = new[]
                {
                    MedicalCenterStatus.Pending,
                    MedicalCenterStatus.Approved,
                    MedicalCenterStatus.Integration,
                    MedicalCenterStatus.Validation
                };

                if (blockedStatuses.Contains(medicalCenter.Status))
                {
                    return new Response<JoinMedicalCenterRequestDto>(false,
                        $"Cannot create join request. Medical center status is {medicalCenter.Status}", null);
                }
            }

            // Check if user already has a pending join request
            var existingRequestFilter = Builders<JoinMedicalCenterRequests>.Filter.Eq(x => x.UserId, dto.UserId);
            var existingRequest = await _joinRequests.Find(existingRequestFilter).FirstOrDefaultAsync();

            if (existingRequest != null && existingRequest.Status == JoinMedicalCenterStatus.Pending)
            {
                return new Response<JoinMedicalCenterRequestDto>(false,
                    "User already has a pending join request", null);
            }

            // Create the join request
            var joinRequest = new JoinMedicalCenterRequests
            {
                UserId = dto.UserId,
                MedicalCenterId = medicalCenterId,
                RejectionReason = dto.RequestComments,
                Status = JoinMedicalCenterStatus.Pending,
                ProcessedAt = DateTime.Now,
                ProcessedBy = "user",
                CreatedAt = DateTime.UtcNow
            };

            await _joinRequests.InsertOneAsync(joinRequest);

            var responseDto = new JoinMedicalCenterRequestDto
            {
                Id = joinRequest._id.ToString(),
                UserId = joinRequest.UserId,
                MedicalCenterId = joinRequest.MedicalCenterId,
                RequestComments = joinRequest.RejectionReason,
                Status = joinRequest.Status,
                CreatedAt = joinRequest.CreatedAt,
                MedicalCenter = medicalCenterData
            };

            return new Response<JoinMedicalCenterRequestDto>(true,
                "Join request created successfully", responseDto);
        }
        catch (Exception ex)
        {
            return new Response<JoinMedicalCenterRequestDto>(false,
                $"Error creating join request: {ex.Message}", null);
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
                var result = await _joinRequests.UpdateOneAsync(x => x._id == objectId, combinedUpdate);

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
        // try
        // {
        //     if (!ObjectId.TryParse(id, out var objectId))
        //         return new Response<string>(false, "Invalid ID format", null);

        //     var update = Builders<JoinMedicalCenterRequest>.Update
        //         .Set(x => x.Status, dto.Status)
        //         .Set(x => x.ProcessedAt, DateTime.UtcNow)
        //         .Set(x => x.ProcessedBy, dto.ProcessedBy);

        //     if (!string.IsNullOrEmpty(dto.RejectionReason))
        //         update = update.Set(x => x.RejectionReason, dto.RejectionReason);

        //     var result = await _joinRequests.UpdateOneAsync(x => x._id == objectId, update);

        //     if (result.MatchedCount == 0)
        //         return new Response<string>(false, "Request not found", null);

        //     return new Response<string>(true, "Request processed successfully", id);
        // }
        // catch (Exception ex)
        // {
        //     return new Response<string>(false, ex.Message, null);
        // }

        return new Response<string>(false, "later", null);

    }

    public async Task<Response<string>> DeleteAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<string>(false, "Invalid ID format", null);

            var result = await _joinRequests.DeleteOneAsync(x => x._id == objectId);

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
            MedicalCenterId = request.MedicalCenterId,
            RequestComments = request.RejectionReason,
            Status = request.Status,
            RejectionReason = request.RejectionReason,
            CreatedAt = request.CreatedAt,
            ProcessedAt = request.ProcessedAt,
            ProcessedBy = request.ProcessedBy
        };
    }
}
