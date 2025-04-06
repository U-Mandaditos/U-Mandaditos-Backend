using Aplication.DTOs.General;
using Aplication.DTOs.Mandaditos;
using Domain.Entities;

namespace Aplication.Interfaces.Mandaditos;

public interface IMandaditoService
{
    Task<ResponseDTO<MandaditoResponseDTO?>> GetByIdAsync(int id);
    Task<int> DeliveriesCount(int idUser);
    Task<IEnumerable<MandaditoHistoryResponseDTO>?> GetHistoryAsync(int userId);
    Task<MandaditoResponseMinDTO?> CreateAsync(MandaditoRequestDTO dto);
    Task<Dictionary<string, List<Mandadito>>> Execute();
    Task<Dictionary<string, List<Mandadito>>> ExecuteGet();
}