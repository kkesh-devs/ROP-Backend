using AutoMapper;
using KKESH_ROP.DTO.DiagnoseRequest;
using KKESH_ROP.Enums;
using KKESH_ROP.Models;

namespace KKESH_ROP.Mappers;

public class DiagnoseRequestProfile :  Profile
{
    public DiagnoseRequestProfile()
    {
        CreateMap<DiagnoseRequest, DiagnoseRequestDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src._id.ToString()));

        CreateMap<CreateDiagnoseRequestDto, DiagnoseRequest>()
            .ForMember(dest => dest._id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => DiagnoseReqStatusEnum.New))
            .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(_ => new Timestamp
            {
                CreatedAt = DateTime.UtcNow,
            }))
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId));
    }
}