using AutoMapper;
using KKESH_ROP.DTO.ProductManager;
using KKESH_ROP.Models;

namespace KKESH_ROP.Mappers;

public class ProductManagerProfile : Profile
{
    public ProductManagerProfile()
    {
        CreateMap<ProductManager, ProductManagerDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src._id.ToString()));

        CreateMap<CreateProductManagerDto, ProductManager>()
            .ForMember(dest => dest._id, opt => opt.Ignore());

        CreateMap<UpdateProductManagerDto, ProductManager>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}