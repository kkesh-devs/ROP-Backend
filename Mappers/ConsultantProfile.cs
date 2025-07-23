using AutoMapper;
using KKESH_ROP.DTO.Consultant;
using KKESH_ROP.Models;

namespace KKESH_ROP.Mappers;

public class ConsultantProfile : Profile
{
    public ConsultantProfile()
    {
        CreateMap<Consultant, ConsultantDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src._id.ToString()));

        CreateMap<CreateConsultantDto, Consultant>()
            .ForMember(dest => dest._id, opt => opt.Ignore());

        CreateMap<UpdateConsultantDto, Consultant>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}