using Aplication.Interfaces;
using Aplication.DTOs;
using Domain.Entities;
using Aplication.DTOs.General;

public class RatingService : IRatingService
{

    private readonly IRatingRepository _ratingRepository;

    public RatingService(IRatingRepository ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    public async Task<ResponseDTO<RatingResponseDTO>> CreateAsync(RatingRequestDTO ratingRequest)
    {
        var rating = new Rating
        {
            IdMandadito = ratingRequest.IdMandadito,
            IdRater = ratingRequest.IdRater,
            IdRatedUser = ratingRequest.IdRatedUser,
            RatingNum = ratingRequest.RatingNum,
            Review = ratingRequest.Review,
            IdRatedRole = ratingRequest.IsOwner ? 1 : 2,
        };

        await _ratingRepository.AddAsync(rating);
        return new ResponseDTO<RatingResponseDTO>
        {
            Success = true,
            Message = "Reseña creada con éxito.",
            Data = new RatingResponseDTO
            {
                Id = rating.Id,
                UserName = rating.RaterUser?.Name ?? "Unknown",
                ProfilePic = rating.RaterUser?.ProfilePic?.Link ?? "Unknown",
                Score = rating.RatingNum,
                Review = rating.Review,
                DatePosted = rating.CreatedAt.ToString("g"),
                isRunner = rating.RatedRole?.Name == "Runner" ? true : false
            }
        };


    }

    /* Obtiene todas las reseñas de un usuario que ha sido evaluado */
    public async Task<IEnumerable<RatingResponseDTO?>> GetByRatedUserAsync(int idRatedUser)
    {
        var ratings = await _ratingRepository.GetByRatedUserAsync(idRatedUser);
        return ratings.Select(rating => new RatingResponseDTO
        {
            Id = rating.Id,
            UserName = rating.RaterUser?.Name ?? "Unknown",
            ProfilePic = rating.RaterUser?.ProfilePic?.Link ?? "Unknown",
            Score = rating.RatingNum,
            Review = rating.Review,
            DatePosted = rating.CreatedAt.ToString("g"),
            isRunner = rating.RatedRole?.Name == "Runner" ? true : false
        });
    }

    /* ------------------------------------------ Extras */
    public async Task<bool> UpdateAsync(int id, RatingRequestDTO ratingRequest)
    {
        var rating = await _ratingRepository.GetByIdAsync(id);
        if (rating == null)
        {
            return false;
        }

        rating.Review = ratingRequest.Review;
        rating.RatingNum = ratingRequest.RatingNum;

        await _ratingRepository.UpdateAsync(rating);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {

        var rating = await _ratingRepository.GetByIdAsync(id);
        if (rating == null)
        {
            return false;
        }

        await _ratingRepository.DeleteAsync(id);
        return true;
    }

    public async Task<RatingResponseDTO?> GetByIdAsync(int id)
    {
        var rating = await _ratingRepository.GetByIdAsync(id);
        if (rating == null)
        {
            return null;
        }

        return new RatingResponseDTO
        {
            Id = rating.Id,
            UserName = rating.RaterUser?.Name ?? "Unknown",
            Score = rating.RatingNum,
            Review = rating.Review
        };
    }

/*     public async Task<IEnumerable<RatingResponseDTO?>> GetByMandaditoAsync(int idMandadito)
    {
        var ratings = await _ratingRepository.GetByMandaditoAsync(idMandadito);
        return ratings.Select(rating => new RatingResponseDTO
        {
            Id = rating.Id,
            UserName = rating.RaterUser?.Name ?? "Unknown",
            ProfilePic = rating.RaterUser?.ProfilePic?.Link ?? "Unknown",
            Score = rating.RatingNum,
            Review = rating.Review,
            DatePosted = rating.CreatedAt,
            isRunner = rating.RatedRole?.Name == "Runner" ? true : false
        });
    } */

}