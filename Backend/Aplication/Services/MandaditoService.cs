using Aplication.DTOs;
using Aplication.DTOs.General;
using Aplication.DTOs.Locations;
using Aplication.DTOs.Mandaditos;
using Aplication.DTOs.Offers;
using Aplication.DTOs.Posts;
using Aplication.DTOs.Users;
using Aplication.Interfaces.Auth;
using Aplication.Interfaces.Helpers;
using Aplication.Interfaces.Mandaditos;
using Domain.Entities;

namespace Aplication.Services;

public class MandaditoService : IMandaditoService
{
    private readonly IMandaditoRepository _mandaditoRepository;
    private readonly ICodeGeneratorService _codeGeneratorService;
    private readonly IAuthenticatedUserService _authenticatedUserService;

    public MandaditoService(IMandaditoRepository mandaditoRepository, ICodeGeneratorService codeGeneratorService, IAuthenticatedUserService authenticatedUserService)
    {
        _mandaditoRepository = mandaditoRepository;
        _codeGeneratorService = codeGeneratorService;
        _authenticatedUserService = authenticatedUserService;
    }

    public async Task<ResponseDTO<MandaditoResponseDTO?>> GetByIdAsync(int id)
    {
        Console.WriteLine("id man: " + id);

        var mandadito = await _mandaditoRepository.GetByIdAsync(id);
        Console.WriteLine(mandadito);

        if (mandadito == null)
        {
            return new ResponseDTO<MandaditoResponseDTO?>()
            {
                Success = false,
                Message = "Mandadito not found",
                Data = null
            };
        }

        var userId = _authenticatedUserService.GetAuthenticatedUserId();
        var isOwner = mandadito.Post?.PosterUser.Id == userId;
        var isRunner = mandadito.Offer?.UserCreator.Id == userId;
        Console.WriteLine("id user" + userId);
        Console.WriteLine("owner" + isOwner);


        if (!(isOwner || isRunner))
        {
            return new ResponseDTO<MandaditoResponseDTO?>()
            {
                Success = false,
                Message = "El usuario autenticado no es el dueño del mandadito ni el runner, por lo tanto no tiene acceso a esta información.",
                Data = null
            };
        }

        var data = new MandaditoResponseDTO
        {
            Id = mandadito.Id,
            SecurityCode = mandadito.SecurityCode,
            AcceptedAt = mandadito.AcceptedAt,
            AcceptedRate = mandadito.AcceptedRate,
            Ratings = mandadito.Ratings.Select(r => new RatingMandaditosDTO()
            {
                DatePosted = r.CreatedAt.ToString("g"),
                Review = r.Review,
                IsRunner = r.RatedRole?.Id == 2,
                Rating = r.RatingNum
            }),
            Offer = mandadito.Offer is null
                    ? null
                    : new OfferDTO
                    {
                        Id = mandadito.Offer.Id,
                        CounterOfferAmount = mandadito.Offer.CounterOfferAmount,
                        IdPost = mandadito.Offer.IdPost,
                        UserCreator = mandadito.Offer.UserCreator is null
                            ? null
                            : new UserResponseMandaditoDTO
                            {
                                Id = mandadito.Offer.UserCreator.Id,
                                Name = mandadito.Offer.UserCreator.Name,
                                LastLocation = mandadito.Offer.UserCreator.LastLocation?.Name,
                                ProfilePicture = mandadito.Offer.UserCreator.ProfilePic?.Link
                            },
                        CreatedAt = mandadito.Offer.CreatedAt.ToString("g"),
                        IsCounterOffer = mandadito.Offer.IsCounterOffer,
                        Accepted = mandadito.Offer.Accepted
                    },
            Post = mandadito.Post is null
                    ? null
                    : new PostMandaditoDTO
                    {
                        Id = mandadito.Post.Id,
                        SuggestedValue = mandadito.Post.SugestedValue,
                        Description = mandadito.Post.Description,
                        CreatedAt = mandadito.Post.CreatedAt.ToString("g"),
                        PosterUser = new UserResponseMandaditoDTO
                        {
                            Id = mandadito.Post.PosterUser.Id,
                            Name = mandadito.Post.PosterUser.Name,
                            LastLocation = mandadito.Post.PosterUser.LastLocation?.Name,
                            ProfilePicture = mandadito.Post.PosterUser.ProfilePic?.Link
                        },
                        PickupLocation = mandadito.Post.PickUpLocation.Name,
                        DeliveryLocation = mandadito.Post.DeliveryLocation.Name
                    }
        };

        return new ResponseDTO<MandaditoResponseDTO?>()
        {
            Success = true,
            Message = "El mandadito fue encontrado correctamente.",
            Data = data
        };
    }

    public async Task<IEnumerable<MandaditoHistoryResponseDTO>?> GetHistoryAsync(int userId)
    {
        var mandaditosByUser = (await _mandaditoRepository.GetAllAsync())
            .Where(mandadito => mandadito.Post != null && mandadito.Post.PosterUser.Id == userId && mandadito.Post.Completed)
            .OrderByDescending(mandadito => mandadito.DeliveredAt);

        return mandaditosByUser.Select(man => new MandaditoHistoryResponseDTO
        {
            Id = man.Id,
            PickupLocationName = man.Post?.PickUpLocation.Name,
            DeliveryLocationName = man.Post?.DeliveryLocation.Name,
            Title = man.Post?.Description,
            RunnerName = man.Offer?.UserCreator?.Name,
            DeliveredAt = man.DeliveredAt
        });
    }

    public async Task<ResponseDTO<MandaditoResponseMinDTO?>> CreateAsync(MandaditoRequestDTO dto)
    {
        string securityCode = _codeGeneratorService.GenerateMandaditoCode(6);
        var mandadito = new Mandadito(
            securityCode: securityCode,
            acceptedRate: dto.AcceptedRate,
            idPost: dto.PostId,
            idOffer: dto.OfferId,
            acceptedAt: DateTime.Now);

        try
        {
            // Intentar agregar el mandadito
            await _mandaditoRepository.AddAsync(mandadito);

            // Verificar si la creación fue exitosa al intentar obtenerlo por su Id
            var createdMandadito = await _mandaditoRepository.GetByIdAsync(mandadito.Id);
            if (createdMandadito == null)
            {
                return new ResponseDTO<MandaditoResponseMinDTO?>
                {
                    Success = false,
                    Message = "Hubo un problema al crear el mandadito, no se encontró el registro después de la creación.",
                    Data = null
                };
            }

            // Si fue exitoso, retornamos los datos
            return new ResponseDTO<MandaditoResponseMinDTO?>
            {
                Success = true,
                Message = "Mandadito created successfully",
                Data = new MandaditoResponseMinDTO
                {
                    Id = mandadito.Id,
                    SecurityCode = mandadito.SecurityCode,
                    AcceptedAt = mandadito.AcceptedAt,
                    AcceptedRate = mandadito.AcceptedRate,
                    OfferId = mandadito.IdOffer,
                    PostId = mandadito.IdPost
                }
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<MandaditoResponseMinDTO?>
            {
                Success = false,
                Message = $"Error creating Mandadito: {ex.Message}",
                Data = null
            };
        }
    }


    public async Task<Dictionary<string, List<Mandadito>>> Execute()
    {
        var userId = _authenticatedUserService.GetAuthenticatedUserId();
        return await _mandaditoRepository.GetHistoryMandaditos(userId);
    }

    public async Task<Dictionary<string, List<Mandadito>>> ExecuteGet()
    {
        var userId = _authenticatedUserService.GetAuthenticatedUserId();
        return await _mandaditoRepository.GetHistoryMandaditosLikeRunner(userId);
    }
}