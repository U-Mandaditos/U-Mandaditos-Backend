using Aplication.DTOs;
using Aplication.DTOs.General;

namespace Aplication.Interfaces {
    public interface IRatingService
    {
        Task<ResponseDTO<RatingResponseDTO>> CreateAsync(RatingRequestDTO ratingRequestDTO);
        Task<bool> UpdateAsync(int id, RatingRequestDTO ratingRequestDTO);
        Task<bool> DeleteAsync(int id);
        Task<RatingResponseDTO?> GetByIdAsync(int id);
        Task<IEnumerable<RatingResponseDTO?>> GetByRatedUserAsync(int idRatedUser);
/*         Task<IEnumerable<RatingResponseDTO?>> GetByMandaditoAsync(int idMandadito);
 */    }
}