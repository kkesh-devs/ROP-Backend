using AutoMapper;
using KKESH_ROP.DTO.JoinPhysician;
using KKESH_ROP.Models;

namespace KKESH_ROP.Mappers;

public class JoinPhysicianProfile : Profile
{
    public JoinPhysicianProfile()
    {
        CreateMap<JoinPhysician, JoinPhysicianDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src._id.ToString()));

        CreateMap<CreateJoinPhysicianDto, JoinPhysician>()
            .ForMember(dest => dest._id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.ProcessedAt, opt => opt.Ignore())
            .ForMember(dest => dest.ProcessedBy, opt => opt.Ignore())
            .ForMember(dest => dest.RejectionReason, opt => opt.Ignore());

        CreateMap<UpdateJoinPhysicianDto, JoinPhysician>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<ProcessJoinPhysicianDto, JoinPhysician>()
            .ForMember(dest => dest._id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.RejectionReason, opt => opt.MapFrom(src => src.RejectionReason))
            .ForMember(dest => dest.ProcessedBy, opt => opt.MapFrom(src => src.ProcessedBy))
            .ForMember(dest => dest.ProcessedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}
