using KKESH_ROP.DTO.User;
using KKESH_ROP.Helpers;

namespace KKESH_ROP.Interfaces.IRepositories;

public interface IUserRepository
{
    Task<Response<List<UserDto>>> GetAllAsync();
    Task<Response<UserDto>> GetByIdAsync(string id);
    Task<Response<UserDto>> Register(RegisterUserDto dto);
    Task<Response<UserDto>> CreateAsync(CreateUserDto dto);
    Task<Response<bool>> UpdateAsync(string id, UpdateUserDto dto);
    Task<Response<bool>> DeleteAsync(string id);
    Task<Response<string>> LoginAsync(LoginDto dto);
}