using AutoMapper;
using KKESH_ROP.DTO.MedicalCenter;
using KKESH_ROP.Models;

namespace KKESH_ROP.Mappers;

public class MedicalCenterProfile : Profile
{
    public MedicalCenterProfile()
    {
        CreateMap<MedicalCenter, MedicalCenterDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src._id.ToString()));

        CreateMap<CreateMedicalCenterDto, MedicalCenter>()
            .ForMember(dest => dest._id, opt => opt.Ignore());

        CreateMap<UpdateMedicalCenterDto, MedicalCenter>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}