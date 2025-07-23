using AutoMapper;
using KKESH_ROP.DTO.Screener;
using KKESH_ROP.Models;

namespace KKESH_ROP.Mappers;

public class ScreenerProfile : Profile
{
    public ScreenerProfile()
    {
        CreateMap<Screener, ScreenerDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src._id.ToString()));

        CreateMap<CreateScreenerDto, Screener>()
            .ForMember(dest => dest._id, opt => opt.Ignore());

        CreateMap<UpdateScreenerDto, Screener>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}