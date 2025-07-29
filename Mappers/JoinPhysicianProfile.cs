using AutoMapper;
using KKESH_ROP.DTO.JoinPhysician;
namespace KKESH_ROP.Mappers;
using KKESH_ROP.Enums;
using KKESH_ROP.Models;

public class JoinPhysicianProfile : Profile
{
    public JoinPhysicianProfile()
    {
        CreateMap<JoinPhysicianRequests, JoinPhysicianDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src._id.ToString()));

        CreateMap<CreateJoinPhysicianDto, JoinPhysicianRequests>()
            .ForMember(dest => dest._id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.ProcessedAt, opt => opt.Ignore())
            .ForMember(dest => dest.ProcessedBy, opt => opt.Ignore())
            .ForMember(dest => dest.RejectionReason, opt => opt.Ignore());

        CreateMap<UpdateJoinPhysicianDto, JoinPhysicianRequests>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<ProcessJoinPhysicianDto, JoinPhysicianRequests>()
            .ForMember(dest => dest._id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.RejectionReason, opt => opt.MapFrom(src => src.RejectionReason))
            .ForMember(dest => dest.ProcessedBy, opt => opt.MapFrom(src => src.ProcessedBy))
            .ForMember(dest => dest.ProcessedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}
