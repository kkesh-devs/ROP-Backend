using AutoMapper;
using KKESH_ROP.DTO.Diagnose;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KKESH_ROP.Repositories;

public class DiagnoseRepository : IDiagnoseRepository
{
    private readonly IMongoCollection<Diagnose> _diagnoses;
    private readonly IMapper _mapper;

    public DiagnoseRepository(IMongoDatabase database, IMapper mapper)
    {
        _diagnoses = database.GetCollection<Diagnose>("Diagnoses");
        _mapper = mapper;
    }

    public async Task<Response<DiagnoseDto>> CreateAsync(CreateDiagnoseDto dto)
    {
        try
        {
            var diagnose = _mapper.Map<Diagnose>(dto);
            diagnose._id = ObjectId.GenerateNewId();
           
            
            await _diagnoses.InsertOneAsync(diagnose);
            
            var responseDto = _mapper.Map<DiagnoseDto>(diagnose);
            return new Response<DiagnoseDto>(true, "Diagnose created successfully", responseDto);
        }
        catch (Exception ex)
        {
            return new Response<DiagnoseDto>(false, ex.Message, null);
        }
    }

    public async Task<Response<DiagnoseDto>> GetByIdAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<DiagnoseDto>(false, "Invalid ID format", null);

            var diagnose = await _diagnoses.Find(x => x._id == objectId).FirstOrDefaultAsync();
            if (diagnose == null)
                return new Response<DiagnoseDto>(false, "Diagnose not found", null);

            var dto = _mapper.Map<DiagnoseDto>(diagnose);
            return new Response<DiagnoseDto>(true, "Diagnose retrieved successfully", dto);
        }
        catch (Exception ex)
        {
            return new Response<DiagnoseDto>(false, ex.Message, null);
        }
    }

    public async Task<Response<DiagnoseDto>> UpdateAsync(string id, UpdateDiagnoseDto dto)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<DiagnoseDto>(false, "Invalid ID format", null);

            var existingDiagnose = await _diagnoses.Find(x => x._id == objectId).FirstOrDefaultAsync();
            if (existingDiagnose == null)
                return new Response<DiagnoseDto>(false, "Diagnose not found", null);

            var updatedDiagnose = _mapper.Map(dto, existingDiagnose);

            await _diagnoses.ReplaceOneAsync(x => x._id == objectId, updatedDiagnose);

            var responseDto = _mapper.Map<DiagnoseDto>(updatedDiagnose);
            return new Response<DiagnoseDto>(true, "Diagnose updated successfully", responseDto);
        }
        catch (Exception ex)
        {
            return new Response<DiagnoseDto>(false, ex.Message, null);
        }
    }

    public async Task<Response<bool>> DeleteAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<bool>(false, "Invalid ID format", false);

            var result = await _diagnoses.DeleteOneAsync(x => x._id == objectId);
            
            if (result.DeletedCount == 0)
                return new Response<bool>(false, "Diagnose not found", false);

            return new Response<bool>(true, "Diagnose deleted successfully", true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, ex.Message, false);
        }
    }
}