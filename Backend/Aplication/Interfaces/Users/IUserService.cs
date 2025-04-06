using Aplication.DTOs.General;
using System.Threading.Tasks;
using Aplication.DTOs.Users;
using Azure;
using Aplication.DTOs.Users.Profile;

namespace Aplication.Interfaces.Users;

public interface IUserService
{
    Task<ResponseDTO<UserResponseDTO>> CreateUserAsync(UserRequestDTO userRequest);
    Task<UserResponseDTO?> GetByEmailAsync(string email);
    Task<ResponseDTO<UserProfileResponseDTO>> GetByIdAsync(int id);
    Task<ResponseDTO<UserPublicProfileInfoResponseDTO>> GetPublicProfileInfoAsync(int idUser);
    Task<ResponseDTO<UserPrivateProfileInfoResponseDTO>> GetPrivateProfileInfoAsync();
    Task<ResponseDTO<UpdatedResponseDTO>> UpdateAsync(int id, UserProfileRequestDTO user);
    Task<ResponseDTO<UpdatedResponseDTO>> UpdateProfileAsync(UserUpdateProfileRequestDTO user);
    Task<ResponseDTO<bool>> ChangePasswordAsync(int id, string password);
    Task<ResponseDTO<UserProfileResponseDTO>> GetUser();
}