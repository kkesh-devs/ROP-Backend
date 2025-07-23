using AutoMapper;
using KKESH_ROP.DTO.Diagnose;
using KKESH_ROP.Models;

namespace KKESH_ROP.Mappers;

public class DiagnoseProfile :  Profile
{
    public DiagnoseProfile()
    {
        CreateMap<Diagnose, DiagnoseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src._id.ToString()));

        CreateMap<CreateDiagnoseDto, Diagnose>();
        CreateMap<UpdateDiagnoseDto, Diagnose>();
    }
}