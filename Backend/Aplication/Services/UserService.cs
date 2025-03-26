using Aplication.DTOs;
using Aplication.DTOs.General;
using Aplication.DTOs.Locations;
using Aplication.DTOs.Media;
using Aplication.DTOs.Users;
using Aplication.Interfaces;
using Aplication.Interfaces.Helpers;
using Aplication.Interfaces.Locations;
using Aplication.Interfaces.Users;
using Domain.Entities;

namespace Aplication.Services;

public class UserService: IUserService
{
    public readonly IUserRepository _userRepository;
    public readonly IFirebaseStorageService _firebaseService;
    public readonly ICareerRepository _careerRepository;
    public readonly ILocationRepository _locationRepository;

    public UserService(IUserRepository userRepository,  IFirebaseStorageService firebaseService, ICareerRepository careerRepository, ILocationRepository locationRepository)
    {
        _userRepository = userRepository;
        _firebaseService = firebaseService;
        _careerRepository = careerRepository;
        _locationRepository = locationRepository;
    }
    
    public async Task<ResponseDTO<UserResponseDTO>> CreateUserAsync(UserRequestDTO userRequest)
    {
        try
        {
            var user = new User();
            var fileName = $"{userRequest.name}-{userRequest.dni}";
            var photo = await _firebaseService.UploadProfilePicture(userRequest.Photo, fileName, "image/jpeg");

            user.Name = userRequest.name;
            user.Email = userRequest.email;
            user.Password = userRequest.password;
            user.Dni = userRequest.dni;

            // Carrera
            user.CareerId = userRequest.career;
            user.Career = await _careerRepository.GetByIdAsync(userRequest.career);

            // Foto de perfil
            user.ProfilePic = new Media(fileName, photo);

            await _userRepository.AddAsync(user);

            var userR = new UserResponseDTO
            {
                Name = user.Name,
                Link = user.ProfilePic.Link
            };

            return new ResponseDTO<UserResponseDTO>
            {
                Success = true,
                Message = "Usuario registrado exitosamente",
                Data = userR
            };
        } 
        catch(Exception e)
        {
            var errorMessage = e.InnerException?.Message ?? e.Message;

            if(errorMessage.Contains("Cannot insert duplicate key") && errorMessage.Contains("unique index 'IX_Users_Dni'"))
            {
                errorMessage = "Error: Un usuario con este DNI ha sido registrado anteriormente.";
            }

            if (errorMessage.Contains("The INSERT statement conflicted") && errorMessage.Contains("Careers"))
            {
                errorMessage = "Error: Se est� haciendo referencia a una carrera inexistente.";
            }

            return new ResponseDTO<UserResponseDTO>
            {
                Success = false,
                Message = errorMessage,
                Data = null
            };
        }
        
    }

    public async Task<ResponseDTO<UserProfileResponseDTO>> GetByIdAsync(int id)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                return new ResponseDTO<UserProfileResponseDTO>
                {
                    Success = false,
                    Message = $"Usuario con id={id} no encontrado"
                };
            }

            var data = new UserProfileResponseDTO
            {
                Name = user.Name,
                Dni = user.Dni,
                Email = user.Email,
                BirthDay = user.BirthDay,
                Score = user.Rating,
                ProfilePic = new MediaResponseDTO
                {
                    Id = user.ProfilePic.Id,
                    Name = user.ProfilePic.Name,
                    Link = user.ProfilePic.Link
                },
                LastLocation = user.LastLocation != null ? new LastLocationUserDTO
                {
                    Description = user.LastLocation.Description,
                    Name = user.LastLocation.Name,
                    Id = user.LastLocation.Id
                } : null,
                Career = new CareerResponseDTO
                {
                    Id = user.Career.Id,
                    Name = user.Career.Name
                }
            };

            return new ResponseDTO<UserProfileResponseDTO>
            {
                Success = true,
                Message = $"La informaci�n del usuario con id={id} fue obtenida satisfactoriamente",
                Data = data
            };
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return new ResponseDTO<UserProfileResponseDTO>
            {
                Success = false,
                Message = $"Ocurri� un error al obtener al usuario con id={id}"
            };
        }
    }

    public async Task<ResponseDTO<UpdatedResponseDTO>> UpdateAsync(int id, UserProfileRequestDTO user)
    {
        try 
        {
            var userToUpdate = await _userRepository.GetByIdAsync(id);

            if (userToUpdate is null)
                return new ResponseDTO<UpdatedResponseDTO>
                {
                    Success = false,
                    Message = "Usuario no encontrado",
                    Data = new UpdatedResponseDTO
                    {
                        Updated = false
                    }
                };

            // Validar carrera
            var career = await _careerRepository.GetByIdAsync(user.IdCareer);
            if (career == null)
            {
                return new ResponseDTO<UpdatedResponseDTO>
                {
                    Success = false,
                    Message = "Carrera no encontrada",
                    Data = new UpdatedResponseDTO
                    {
                        Updated = false
                    }
                };
            }

            // Validar Last Location
            var lastLocation = await _locationRepository.GetByIdAsync(user.IdLastLocation);
            if (lastLocation == null)
            {
                return new ResponseDTO<UpdatedResponseDTO>
                {
                    Success = false,
                    Message = "�ltima ubicaci�n no encontrada",
                    Data = new UpdatedResponseDTO
                    {
                        Updated = false
                    }
                };
            }

            // Foto de perfil
            var fileName = $"{user.Name}-{user.Dni}";
            var photo = await _firebaseService.UploadProfilePicture(user.ProfilePic, fileName, "image/jpeg");
            var profilePic = new Media(fileName, photo);

            userToUpdate.Name = user.Name;
            userToUpdate.Dni = user.Dni;
            userToUpdate.Password = user.Password;
            userToUpdate.Email = user.Email;
            userToUpdate.BirthDay = user.BirthDay;
            userToUpdate.Rating = user.Score;
            userToUpdate.ProfilePic = profilePic;
            userToUpdate.LastLocation = lastLocation;
            userToUpdate.Career = career;

            var isUpdated = await _userRepository.UpdateAsync(userToUpdate);

            if (isUpdated == false)
            {
                return new ResponseDTO<UpdatedResponseDTO>
                {
                    Success = false,
                    Message = $"Ocurri� un error al actualizar al usuario con id={id}",
                    Data = new UpdatedResponseDTO
                    {
                        Updated = false,
                    }
                };
            }

            return new ResponseDTO<UpdatedResponseDTO>
            {
                Success = true,
                Message = $"El usuario con id={id} ha sido actualizado",
                Data = new UpdatedResponseDTO
                {
                    Updated = true
                }
            };
        }
        catch(Exception e)
        {
            var errorMessage = e.InnerException?.Message ?? e.Message;

            if (errorMessage.Contains("Cannot insert duplicate key") && errorMessage.Contains("unique index 'IX_Users_Dni'"))
            {
                errorMessage = "Error: Ya existe un usuario con este DNI";
            }

            return new ResponseDTO<UpdatedResponseDTO>
            {
                Success = false,
                Message = errorMessage,
                Data = new UpdatedResponseDTO { 
                    Updated = false
                }
            };
        }
    }
}