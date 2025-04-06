using Aplication.DTOs.General;
using Aplication.DTOs.Posts;
using Aplication.Interfaces.Auth;
using Aplication.Interfaces.Locations;
using Aplication.Interfaces.Posts;
using Aplication.Interfaces.Users;
using Domain.Entities;

namespace Aplication.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IAuthenticatedUserService _authenticatedUserService;

    public PostService(IPostRepository postRepository, IUserRepository userRepository, ILocationRepository locationRepository, IAuthenticatedUserService authenticatedUserService)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _locationRepository = locationRepository;
        _authenticatedUserService = authenticatedUserService;
    }

    public async Task<ResponseDTO<PostResponseDTO>> CreateAsync(PostRequestDTO dto)
    {
        var userId = _authenticatedUserService.GetAuthenticatedUserId(); //Obtiene el id del usuario autenticado
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return new ResponseDTO<PostResponseDTO>
            {
                Success = false,
                Message = "El usuario no existe.",
                Data = null
            };
        }

        var pickUpLocation = await _locationRepository.GetByIdAsync(dto.IdPickUpLocation);
        if (pickUpLocation == null)
        {
            return new ResponseDTO<PostResponseDTO>
            {
                Success = false,
                Message = "La ubicación de recogida no existe.",
                Data = null
            };
        }

        var deliveryLocation = await _locationRepository.GetByIdAsync(dto.IdDeliveryLocation);
        if (deliveryLocation == null)
        {
            return new ResponseDTO<PostResponseDTO>
            {
                Success = false,
                Message = "La ubicación de entrega no existe.",
                Data = null
            };
        }

        var post = new Post
        {
            Title = dto.Title,
            Description = dto.Description,
            SugestedValue = dto.SuggestedValue,
            IdPickUpLocation = dto.IdPickUpLocation,
            IdDeliveryLocation = dto.IdDeliveryLocation,
            IdPosterUser = userId,
            CreatedAt = DateTime.Now,
            Completed = false,
            Accepted = false,
        };

        await _postRepository.AddAsync(post);

        return new ResponseDTO<PostResponseDTO>
        {
            Success = true,
            Message = "Se ha creado un nuevo post correctamente.",
            Data = new PostResponseDTO
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                SuggestedValue = post.SugestedValue,
                PosterUserName = post.PosterUser != null ? post.PosterUser.Name : "Usuario desconocido",
                PickUpLocation = pickUpLocation.Name,
                DeliveryLocation = deliveryLocation.Name,
                CreatedAt = post.CreatedAt.ToString("yyyy-MM-dd HH:mm")
            }
        };
    }

    public async Task<ResponseDTO<IEnumerable<PostResponseDTO>>> GetAllNearAsync(int currentLocationId)
    {
        var posts = await _postRepository.GetAllAsync();

        var nearPosts = posts.Where(post => post.IdPickUpLocation == currentLocationId && post.Completed == false).Select(post => new PostResponseDTO
        {
            Id = post.Id,
            Title = post.Title,
            PickUpLocation = post.DeliveryLocation.Name,
            DeliveryLocation = post.PickUpLocation.Name,
            SuggestedValue = post.SugestedValue,
            PosterUserName = post.PosterUser.Name,
            CreatedAt = post.CreatedAt.ToString("hh:mm tt") //SOlo la hora y minutos
        });

        return new ResponseDTO<IEnumerable<PostResponseDTO>>
        {
            Success = true,
            Message = "Se han encontrado los siguientes posts cercanos.",
            Data = nearPosts
        };
    }

    public async Task<IEnumerable<PostResponseDTO>> GetPostByLocation(int currentLocationId)
    {
        var posts = await _postRepository.GetPostsByLocationIdAsync(currentLocationId);
        var allPosts = await _postRepository.GetAllAsync();
        Console.WriteLine($"Total posts: {posts.Count()}");
        return posts.Select(post => new PostResponseDTO
        {
            Id = post.Id,
            Title = post.Title,
            Description = post.Description,
            SuggestedValue = post.SugestedValue,
            PosterUserName = post.PosterUser?.Name ?? string.Empty, 
            CreatedAt = post.CreatedAt.ToString("hh:mm tt"),
            PickUpLocation = post.PickUpLocation?.Name ?? string.Empty,
            DeliveryLocation = post.DeliveryLocation?.Name ?? string.Empty
        }).ToList();
    }

    public async Task<IEnumerable<PostResponseDTO>> GetPostsByPosterUserIdAsync(int idPosterUser)
    {
        var posts = await _postRepository.GetPostsByPosterUserId(idPosterUser);

        var postDtos = posts.Select(p => new PostResponseDTO
        {
            Id = p.Id,
            Description = p.Description,
            SuggestedValue = p.SugestedValue,
            PosterUserName = p.PosterUser?.Name ?? "Usuario desconocido",
            CreatedAt = p.CreatedAt.ToString("hh:mm tt"),
            PickUpLocation = p.PickUpLocation?.Name ?? "Ubicación no disponible",
            DeliveryLocation = p.DeliveryLocation?.Name ?? "Ubicación no disponible"
        }).ToList();

        return postDtos;
    }

    public async Task<int> GetPostsCountAsync(int idPosterUser)
    {
        var posts = await _postRepository.GetPostsByPosterUserId(idPosterUser);
        return posts.Count();
    }

    public async Task<ResponseDTO<PostResponseDTO>> GetPostByIdAsync(int idPost)
    {
        var post = await _postRepository.GetByIdAsync(idPost);
        if (post == null)
        {
            return new ResponseDTO<PostResponseDTO>
            {
                Success = false,
                Message = "El post no existe.",
                Data = null
            };
        }

        return new ResponseDTO<PostResponseDTO>
        {
            Success = true,
            Message = "El post fue encontrado correctamente.",
            Data = new PostResponseDTO
            {
                Id = post.Id,
                Description = post.Description,
                CreatedAt = post.CreatedAt.ToString("yyyy-MM-dd HH:mm"),
                PosterUserName = post.PosterUser?.Name ?? "Usuario desconocido",
                DeliveryLocation = post.DeliveryLocation?.Name ?? "Ubicación no disponible",
                PickUpLocation = post.PickUpLocation?.Name ?? "Ubicación no disponible",
                SuggestedValue = post.SugestedValue,
                Title = post.Title,
                Completed = post.Completed,
                Accepted = post.Accepted,
            }
        };
    }

    public async Task<ResponseDTO<bool>> MarkAsAcceptedAsync(int idPost)
    {
        var success = await _postRepository.MarkAsAcceptedAsync(idPost);

        return new ResponseDTO<bool>
        {
            Success = success,
            Message = success ? "El post fue marcado como completado." : "No se pudo marcar el post como completado.",
            Data = success
        };
    }

    public async Task<IEnumerable<PostResponseDTO>> GetActivePosts()
    {
        var userId = _authenticatedUserService.GetAuthenticatedUserId();
        var posts = await _postRepository.GetPostsActive(userId);

        var postDtos = posts.Select(p => new PostResponseDTO
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            SuggestedValue = p.SugestedValue,
            PosterUserName = p.PosterUser?.Name ?? "Usuario desconocido",
            CreatedAt = p.CreatedAt.ToString("hh:mm tt"),
            PickUpLocation = p.PickUpLocation?.Name ?? "Ubicación no disponible",
            DeliveryLocation = p.DeliveryLocation?.Name ?? "Ubicación no disponible"
        }).ToList();

        return postDtos;
    }
}