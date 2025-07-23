using AutoMapper;
using KKESH_ROP.Data;
using KKESH_ROP.DTO.Patient;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Models;
using MongoDB.Bson;

namespace KKESH_ROP.Repositories;

public class PatientRepository(IMapper mapper, ApplicationDbContext context) : IPatientRepository
{
  
    
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<List<RetrievePatientDto>>> GetAllAsync()
    {
        try
        {
            var patients = await context.Patients.ToListAsync();
            var result = mapper.Map<List<RetrievePatientDto>>(patients);
            return new Response<List<RetrievePatientDto>>(true, "Data retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<List<RetrievePatientDto>>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<RetrievePatientDto>> GetByIdAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<RetrievePatientDto>(false, "Invalid ID format", null);

            var patient = await context.Patients.FirstOrDefaultAsync(x => x._id == objectId);
            if (patient == null)
                return new Response<RetrievePatientDto>(false, "Patient not found", null);

            var result = mapper.Map<RetrievePatientDto>(patient);
            return new Response<RetrievePatientDto>(true, "Data retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<RetrievePatientDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<RetrievePatientDto>> CreateAsync(CreatePatientDto dto)
    {
        try
        {
            var patient = mapper.Map<Patient>(dto);

            await context.Patients.AddAsync(patient);
            await context.SaveChangesAsync();

            var result = mapper.Map<RetrievePatientDto>(patient);
            return new Response<RetrievePatientDto>(true, "Patient created successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<RetrievePatientDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<bool>> UpdateAsync(string id, UpdatePatientDto dto)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<bool>(false, "Invalid ID format", false);

            var patient = await context.Patients.FirstOrDefaultAsync(x => x._id == objectId);
            if (patient == null)
                return new Response<bool>(false, "Patient not found", false);

            mapper.Map(dto, patient);

            context.Patients.Update(patient);
            await context.SaveChangesAsync();

            return new Response<bool>(true, "Patient updated successfully", true);
        }
        catch (Exception exception)
        {
            return new Response<bool>(false, "Error " + exception.Message, false);
        }
    }
//____________________________________________________________________________________________________________________________________________________

}