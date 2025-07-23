using AutoMapper;
using KKESH_ROP.DTO.User;
using KKESH_ROP.Models;

namespace KKESH_ROP.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src._id.ToString()));

        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest._id, opt => opt.Ignore())
            .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(_ => new Timestamp
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }));

        CreateMap<UpdateUserDto, User>()
            .AfterMap((src, dest) =>
            {
                dest.Timestamp ??= new Timestamp();

                dest.Timestamp.UpdatedAt = DateTime.UtcNow;
            })
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null));
    }
}