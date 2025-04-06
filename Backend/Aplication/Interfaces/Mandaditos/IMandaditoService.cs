using Aplication.DTOs.General;
using Aplication.DTOs.Mandaditos;
using Domain.Entities;

namespace Aplication.Interfaces.Mandaditos;

public interface IMandaditoService
{
    Task<ResponseDTO<MandaditoResponseDTO?>> GetByIdAsync(int id);
    Task<IEnumerable<MandaditoHistoryResponseDTO>?> GetHistoryAsync(int userId);
    Task<ResponseDTO<MandaditoResponseMinDTO?>> CreateAsync(MandaditoRequestDTO dto);
    Task<Dictionary<string, List<Mandadito>>> Execute();
    Task<Dictionary<string, List<Mandadito>>> ExecuteGet();
}