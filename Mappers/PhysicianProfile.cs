using AutoMapper;
using KKESH_ROP.DTO.Physician;
using KKESH_ROP.Enums;
using KKESH_ROP.Models;

namespace KKESH_ROP.Mappers;

public class PhysicianProfile : Profile
{
    public PhysicianProfile()
    {
        CreateMap<Physician, PhysicianDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src._id.ToString()))
            .ForMember(dest => dest.HospitalName, opt => opt.MapFrom(src => src.HospitalName))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

        CreateMap<CreatePhysicianDto, Physician>()
            .ForMember(dest => dest._id, opt => opt.Ignore());

        CreateMap<UpdatePhysicianDto, Physician>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}