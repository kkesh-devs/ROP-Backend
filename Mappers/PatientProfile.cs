using AutoMapper;
using KKESH_ROP.DTO.Patient;
using KKESH_ROP.Models;

namespace KKESH_ROP.Mappers;

public class PatientProfile :  Profile
{
    public PatientProfile()
    {
        // Create DTO → Model
        CreateMap<CreatePatientDto, Patient>()
            .ForMember(dest => dest._id, opt => opt.Ignore())
            .ForMember(dest => dest.UpdateBy, opt => opt.Ignore())
            .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(_ => new Timestamp
            {
                CreatedAt = DateTime.UtcNow
            }));

        // Model → Retrieve DTO
        CreateMap<Patient, RetrievePatientDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src._id.ToString()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Timestamp.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.Timestamp.UpdatedAt))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.ToString()))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdateBy.ToString()));

        // Update DTO → Model (used in PUT/PATCH)
        CreateMap<UpdatePatientDto, Patient>()
            .ForMember(dest => dest._id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(_ => new Timestamp
            {
                UpdatedAt = DateTime.UtcNow
            }));
    }
}