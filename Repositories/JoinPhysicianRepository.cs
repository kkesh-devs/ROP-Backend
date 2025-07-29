using AutoMapper;
using KKESH_ROP.DTO.JoinPhysician;
using KKESH_ROP.DTO.Physician;
using KKESH_ROP.Enums;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KKESH_ROP.Repositories;

public class JoinPhysicianRepository : IJoinPhysicianRepository
{
    private readonly IMongoCollection<JoinPhysician> _joinPhysicianCollection;
    private readonly IMongoCollection<Physician> _physicianCollection;
    private readonly IMapper _mapper;

    public JoinPhysicianRepository(IMongoDatabase database, IMapper mapper)
    {
        _joinPhysicianCollection = database.GetCollection<JoinPhysician>("JoinPhysicians");
        _physicianCollection = database.GetCollection<Physician>("Physicians");
        _mapper = mapper;
    }

    public async Task<Response<List<JoinPhysicianDto>>> GetAllAsync()
    {
        try
        {
            var joinRequests = await _joinPhysicianCollection.Find(_ => true).ToListAsync();
            var joinRequestDtos = _mapper.Map<List<JoinPhysicianDto>>(joinRequests);

            return new Response<List<JoinPhysicianDto>>(true, "Join physician requests retrieved successfully", joinRequestDtos);
        }
        catch (Exception ex)
        {
            return new Response<List<JoinPhysicianDto>>(false, ex.Message, null);
        }
    }

    public async Task<Response<JoinPhysicianDto>> GetByIdAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<JoinPhysicianDto>(false, "Invalid ID format", null);

            var joinRequest = await _joinPhysicianCollection.Find(x => x._id == objectId).FirstOrDefaultAsync();
            if (joinRequest == null)
                return new Response<JoinPhysicianDto>(false, "Join physician request not found", null);

            var joinRequestDto = _mapper.Map<JoinPhysicianDto>(joinRequest);
            return new Response<JoinPhysicianDto>(true, "Join physician request retrieved successfully", joinRequestDto);
        }
        catch (Exception ex)
        {
            return new Response<JoinPhysicianDto>(false, ex.Message, null);
        }
    }

    public async Task<Response<CreateJoinPhysicianResponseDto>> CreateAsync(JoinPhysicianWithUserDto dto)
    {
        try
        {
            // First, check if there are any existing join requests for this user
            var existingRequests = await _joinPhysicianCollection
                .Find(x => x.UserId == dto.UserId)
                .ToListAsync();

            // If there's a pending request, don't allow another one
            var pendingRequest = existingRequests.FirstOrDefault(x => x.Status == JoinPhysicianStatus.Pending);
            if (pendingRequest != null)
                return new Response<CreateJoinPhysicianResponseDto>(false, "User already has a pending join physician request", null);

            // Check if physician record exists
            var existingPhysician = await _physicianCollection
                .Find(x => x.UserId == dto.UserId)
                .FirstOrDefaultAsync();

            // If there are no join requests at all, this is the first time
            // Create physician record first, then create join request
            if (!existingRequests.Any())
            {
                if (existingPhysician == null)
                {
                    // Create physician record with pending status
                    var createPhysicianDto = new CreatePhysicianDto
                    {
                        UserId = dto.UserId,
                        FirstName = dto.UserFirstName, // Will need to be updated later
                        LastName = dto.UserLastName,  // Will need to be updated later
                        Mobile = dto.UserMobile,    // Will need to be updated later
                        Country = dto.UserCountry,   // Will need to be updated later
                        City = dto.UserCity,      // Will need to be updated later
                        HospitalName = dto.HospitalName, // Will need to be updated later
                        Status = PhysicianStatus.Pending
                        
                    };

                    var physicianModel = _mapper.Map<Physician>(createPhysicianDto);
                    await _physicianCollection.InsertOneAsync(physicianModel);
                }
            }
            else
            {
                // There are existing join requests (but no pending ones)
                // Check if all previous requests were rejected
                var hasApprovedRequest = existingRequests.Any(x => x.Status == JoinPhysicianStatus.Approved);
                
                if (hasApprovedRequest)
                    return new Response<CreateJoinPhysicianResponseDto>(false, "User already has an approved join physician request", null);

                // User can resubmit if previous requests were rejected
                // Make sure physician record should NOT exist if all requests were rejected
                if (existingPhysician != null)
                {
                    // Remove the physician record since all previous requests were rejected
                    await _physicianCollection.DeleteOneAsync(x => x.UserId == dto.UserId);
                }
            }

            // Create the join request
            var joinRequest = _mapper.Map<JoinPhysician>(dto);
            await _joinPhysicianCollection.InsertOneAsync(joinRequest);

            // Get the physician record to return in response
            var physicianRecord = await _physicianCollection
                .Find(x => x.UserId == dto.UserId)
                .FirstOrDefaultAsync();

            if (physicianRecord == null)
                return new Response<CreateJoinPhysicianResponseDto>(false, "Failed to retrieve physician record after creation", null);

            // Create response with only physician profile data
            var response = new CreateJoinPhysicianResponseDto
            {
                PhysicianProfile = _mapper.Map<PhysicianDto>(physicianRecord)
            };

            return new Response<CreateJoinPhysicianResponseDto>(true, "Join physician request created successfully", response);
        }
        catch (Exception ex)
        {
            return new Response<CreateJoinPhysicianResponseDto>(false, ex.Message, null);
        }
    }

    public async Task<Response<bool>> UpdateAsync(string id, UpdateJoinPhysicianDto dto)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<bool>(false, "Invalid ID format", false);

            var joinRequest = await _joinPhysicianCollection.Find(x => x._id == objectId).FirstOrDefaultAsync();
            if (joinRequest == null)
                return new Response<bool>(false, "Join physician request not found", false);

            if (joinRequest.Status != JoinPhysicianStatus.Pending)
                return new Response<bool>(false, "Only pending requests can be updated", false);

            // Since UpdateJoinPhysicianDto is now empty, we just return success
            // You can add update logic here if you add fields to UpdateJoinPhysicianDto in the future
            
            return new Response<bool>(true, "Join physician request updated successfully", true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, ex.Message, false);
        }
    }

    public async Task<Response<bool>> ProcessAsync(string id, ProcessJoinPhysicianDto dto)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<bool>(false, "Invalid ID format", false);

            var filter = Builders<JoinPhysician>.Filter.Eq(x => x._id, objectId);
            var update = Builders<JoinPhysician>.Update
                .Set(x => x.Status, dto.Status)
                .Set(x => x.ProcessedAt, DateTime.UtcNow)
                .Set(x => x.ProcessedBy, dto.ProcessedBy);

            if (!string.IsNullOrEmpty(dto.RejectionReason))
                update = update.Set(x => x.RejectionReason, dto.RejectionReason);

            var result = await _joinPhysicianCollection.UpdateOneAsync(filter, update);
            if (result.MatchedCount == 0)
                return new Response<bool>(false, "Join physician request not found", false);

            return new Response<bool>(true, "Join physician request processed successfully", true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, ex.Message, false);
        }
    }

    public async Task<Response<List<JoinPhysicianDto>>> GetByStatusAsync(JoinPhysicianStatus status)
    {
        try
        {
            var joinRequests = await _joinPhysicianCollection.Find(x => x.Status == status).ToListAsync();
            var joinRequestDtos = _mapper.Map<List<JoinPhysicianDto>>(joinRequests);

            return new Response<List<JoinPhysicianDto>>(true, "Join physician requests retrieved successfully", joinRequestDtos);
        }
        catch (Exception ex)
        {
            return new Response<List<JoinPhysicianDto>>(false, ex.Message, null);
        }
    }

    public async Task<Response<JoinPhysicianDto>> GetByUserIdAsync(string userId)
    {
        try
        {
            var joinRequest = await _joinPhysicianCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
            if (joinRequest == null)
                return new Response<JoinPhysicianDto>(false, "Join physician request not found", null);

            var joinRequestDto = _mapper.Map<JoinPhysicianDto>(joinRequest);
            return new Response<JoinPhysicianDto>(true, "Join physician request retrieved successfully", joinRequestDto);
        }
        catch (Exception ex)
        {
            return new Response<JoinPhysicianDto>(false, ex.Message, null);
        }
    }

    public async Task<Response<bool>> DeleteAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<bool>(false, "Invalid ID format", false);

            var result = await _joinPhysicianCollection.DeleteOneAsync(x => x._id == objectId);
            if (result.DeletedCount == 0)
                return new Response<bool>(false, "Join physician request not found", false);

            return new Response<bool>(true, "Join physician request deleted successfully", true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, ex.Message, false);
        }
    }

    public async Task<Response<List<JoinPhysicianDto>>> GetPendingRequestsAsync()
    {
        return await GetByStatusAsync(JoinPhysicianStatus.Pending);
    }

    public async Task<Response<JoinPhysicianWithUserDto>> GetJoinRequestWithUserDetailsAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<JoinPhysicianWithUserDto>(false, "Invalid ID format", null);

            var joinRequest = await _joinPhysicianCollection.Find(x => x._id == objectId).FirstOrDefaultAsync();
            if (joinRequest == null)
                return new Response<JoinPhysicianWithUserDto>(false, "Join physician request not found", null);

            // Get physician details
            var physician = await _physicianCollection.Find(x => x.UserId == joinRequest.UserId).FirstOrDefaultAsync();
            if (physician == null)
                return new Response<JoinPhysicianWithUserDto>(false, "Physician not found", null);

            var result = new JoinPhysicianWithUserDto
            {
                Id = joinRequest._id.ToString(),
                UserId = joinRequest.UserId,
                UserFirstName = physician.FirstName,
                UserLastName = physician.LastName,
                UserEmail = "", // Not available in Physician model
                UserMobile = physician.Mobile,
                UserCountry = physician.Country,
                UserCity = physician.City,
                Status = joinRequest.Status,
                RejectionReason = joinRequest.RejectionReason,
                CreatedAt = joinRequest.CreatedAt,
                ProcessedAt = joinRequest.ProcessedAt,
                ProcessedBy = joinRequest.ProcessedBy
            };

            return new Response<JoinPhysicianWithUserDto>(true, "Join physician request with user details retrieved successfully", result);
        }
        catch (Exception ex)
        {
            return new Response<JoinPhysicianWithUserDto>(false, ex.Message, null);
        }
    }
}
