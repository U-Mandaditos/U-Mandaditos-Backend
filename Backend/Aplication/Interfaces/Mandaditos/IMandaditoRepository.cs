using Domain.Entities;

namespace Aplication.Interfaces.Mandaditos
{
    public interface IMandaditoRepository
    {
        Task<IEnumerable<Mandadito>> GetAllAsync();
        Task<Mandadito?> GetByIdAsync(int id);
        Task AddAsync(Mandadito mandadito);
        Task<bool> UpdateAsync(Mandadito mandadito);
        Task<bool> DeleteAsync(int id);
        Task<int> DeliveriesCount(int idUser);
        Task<Dictionary<string, List<Mandadito>>> GetHistoryMandaditos(int userId);
        Task<Dictionary<string, List<Mandadito>>> GetHistoryMandaditosLikeRunner(int userId);
    }
}
