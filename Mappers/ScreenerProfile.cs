using AutoMapper;
using KKESH_ROP.DTO.Screener;
using KKESH_ROP.Models;
using MongoDB.Bson;

namespace KKESH_ROP.Mappers;

public class ScreenerProfile : Profile
{
    public ScreenerProfile()
    {
        CreateMap<Screener, ScreenerDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src._id.ToString()));
            
        CreateMap<CreateScreenerDto, Screener>()
            .ForMember(dest => dest._id, opt => opt.MapFrom(src => ObjectId.GenerateNewId()))
            .ForMember(dest => dest.IsTrained, opt => opt.MapFrom(src => false));
            
        CreateMap<UpdateScreenerDto, Screener>()
            .ForMember(dest => dest._id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.IsTrained, opt => opt.Ignore());
    }
}