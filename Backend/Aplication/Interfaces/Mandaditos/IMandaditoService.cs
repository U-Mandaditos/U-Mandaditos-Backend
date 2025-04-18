using Aplication.DTOs.General;
using Aplication.DTOs.Mandaditos;
using Aplication.DTOs.Posts;
using Domain.Entities;

namespace Aplication.Interfaces.Mandaditos;

public interface IMandaditoService
{
    Task<ResponseDTO<MandaditoResponseDTO?>> GetByIdAsync(int id);
    Task<int> DeliveriesCount(int idUser);
    Task<IEnumerable<MandaditoHistoryResponseDTO>?> GetHistoryAsync(int userId);
    Task<ResponseDTO<MandaditoResponseMinDTO?>> CreateAsync(MandaditoRequestDTO dto);
    Task<Dictionary<string, List<Mandadito>>> Execute();
    Task<Dictionary<string, List<Mandadito>>> ExecuteGet();
    Task<ResponseDTO<IEnumerable<MandaditoRunnerResposeDTO>?>> GetActivesByRunnerId();
    Task<ResponseDTO<MandaditoPostResponseDTO?>> GetByPostIdAsync(int idPost);
 
 }