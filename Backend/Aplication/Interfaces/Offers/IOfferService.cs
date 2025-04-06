using Aplication.DTOs;
using Aplication.DTOs.General;


namespace Aplication.Interfaces {
    public interface IOfferService {
        Task<OfferResponseDTO?> GetOfferByIdAsync(int id);
        Task<ResponseDTO<IEnumerable<OfferResponseDTO>>> GetOffersByPostIdAsync(int idPost);
        Task<ResponseDTO<OfferResponseDTO>> CreateOfferAsync(OfferRequestDTO dto);
        Task<OfferResponseDTO?> UpdateOfferStateAsync(int id, bool isAccepted);
        Task<int> QuantityOffersAcceptedByUserAsync(int idUser);
        Task<bool> DeleteOfferAsync(int id);
        Task<IEnumerable<OfferHistoryResponseDTO>> GetOffersAcceptedByUserAsync(int idUser);
    }
}