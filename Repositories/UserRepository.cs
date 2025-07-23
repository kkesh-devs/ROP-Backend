using AutoMapper;
using KKESH_ROP.Data;
using KKESH_ROP.DTO.Helpers;
using KKESH_ROP.DTO.User;
using KKESH_ROP.Enums;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Models;
using MongoDB.Bson;

namespace KKESH_ROP.Repositories;

public class UserRepository(ApplicationDbContext context, TokenHelper tokenHelper, IMapper mapper) : IUserRepository
{

//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<List<UserDto>>> GetAllAsync()
    {
        try
        {
            var users = await context.Users.ToListAsync();
            var result = mapper.Map<List<UserDto>>(users);
            return new Response<List<UserDto>>(true, "Data retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<List<UserDto>>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<UserDto>> GetByIdAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<UserDto>(false, "Invalid ID format", null);

            var user = await context.Users.FirstOrDefaultAsync(x => x._id == objectId);
            if (user == null)
                return new Response<UserDto>(false, "User not found", null);

            var result = mapper.Map<UserDto>(user);
            return new Response<UserDto>(true, "Data retrieved successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<UserDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<UserDto>> CreateAsync(CreateUserDto dto)
    {
        try
        {
            var existedUser = await context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (existedUser != null)
            {
                return new Response<UserDto>(false, "Email address already exists", null);
            }
            
            var user = mapper.Map<User>(dto);
            
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            user.Status = UserStatusEnum.Active;

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var result = mapper.Map<UserDto>(user);
            return new Response<UserDto>(true, "User created successfully", result);
        }
        catch (Exception exception)
        {
            return new Response<UserDto>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<bool>> UpdateAsync(string id, UpdateUserDto dto)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<bool>(false, "Invalid ID format", false);

            var user = await context.Users.FirstOrDefaultAsync(x => x._id == objectId);
            if (user == null)
                return new Response<bool>(false, "User not found", false);

            mapper.Map(dto, user);
            context.Users.Update(user);
            await context.SaveChangesAsync();

            return new Response<bool>(true, "User updated successfully", true);
        }
        catch (Exception exception)
        {
            return new Response<bool>(false, "Error " + exception.Message, false);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<bool>> DeleteAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return new Response<bool>(false, "Invalid ID format", false);

            var user = await context.Users.Include(u => u.Timestamp).FirstOrDefaultAsync(x => x._id == objectId);
            if (user == null)
                return new Response<bool>(false, "User not found", false);

            user.Status = UserStatusEnum.Deleted;
            user.Timestamp.UpdatedAt = DateTime.UtcNow;

            context.Users.Update(user);
            await context.SaveChangesAsync();

            return new Response<bool>(true, "User deleted successfully", true);
        }
        catch (Exception exception)
        {
            return new Response<bool>(false, "Error " + exception.Message, false);
        }
    }
//____________________________________________________________________________________________________________________________________________________

    public async Task<Response<string>> LoginAsync(LoginDto dto)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
                return new Response<string>(false, "User not found", null);
            
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return new Response<string>(false, "Invalid email or password", null);

            switch (user.Status)
            {
                case UserStatusEnum.Blocked:
                    return new Response<string>(false, "User is blocked", null);
                case UserStatusEnum.Pending:
                    return new Response<string>(false, "User is pending approval", null);
                case UserStatusEnum.Inactive:
                    return new Response<string>(false, "User is inactive", null);
                case UserStatusEnum.Deleted:
                    return new Response<string>(false, "User account has been deleted", null);
                case UserStatusEnum.Active:

                    var tokenDto = new TokenDto
                    {
                        _id = user._id.ToString(),
                        Email = user.Email,
                        Role = user.Role.ToString()
                    };

                        
                    var token = tokenHelper.GenerateToken(tokenDto);
                    return new Response<string>(true, "Login successful", token);
                
                default:
                    return new Response<string>(false, "Unknown user status", null);
            }
        }
        catch (Exception exception)
        {
            return new Response<string>(false, "Error " + exception.Message, null);
        }
    }
//____________________________________________________________________________________________________________________________________________________
}
