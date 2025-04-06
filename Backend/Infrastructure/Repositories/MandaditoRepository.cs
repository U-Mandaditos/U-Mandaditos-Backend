using Aplication.DTOs.Mandaditos;
using Aplication.Interfaces.Mandaditos;
using Domain.Entities;
using FirebaseAdmin.Auth.Hash;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MandaditoRepository : IMandaditoRepository
    {
        private readonly BackendDbContext _context;

        public MandaditoRepository(BackendDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mandadito>> GetAllAsync()
        {
            return await _context.Mandaditos
                .Include(m => m.Post)
                .ThenInclude(p => p!.PosterUser)
                .ThenInclude(u => u.LastLocation)
                .Include(m => m.Post)
                .ThenInclude(p => p!.PosterUser)
                .ThenInclude(u => u.ProfilePic)
                .Include(m => m.Post)
                .ThenInclude(p => p!.PickUpLocation)
                .Include(m => m.Post)
                .ThenInclude(p => p!.DeliveryLocation)
                .Include(m => m.Offer)
                .ThenInclude(o => o!.UserCreator)
                .ThenInclude(u => u!.LastLocation)
                .Include(m => m.Offer)
                .ThenInclude(o => o!.UserCreator)
                .ThenInclude(u => u!.ProfilePic)
                .ToListAsync();
        }

                public async Task<Mandadito?> GetByIdAsync(int id)
        {
            return await _context.Mandaditos
                .Include(m => m.Post)
                    .ThenInclude(p => p!.PosterUser)
                        .ThenInclude(u => u.LastLocation)
                .Include(m => m.Post)
                    .ThenInclude(p => p!.PosterUser)
                        .ThenInclude(u => u.ProfilePic)
                .Include(m => m.Post)
                    .ThenInclude(p => p!.PickUpLocation)
                .Include(m => m.Post)
                    .ThenInclude(p => p!.DeliveryLocation)
                .Include(m => m.Offer)
                    .ThenInclude(o => o!.UserCreator)
                        .ThenInclude(u => u!.LastLocation)
                .Include(m => m.Offer)
                    .ThenInclude(o => o!.UserCreator)
                        .ThenInclude(u => u!.ProfilePic)
                .Include(m => m.Ratings)
                    .ThenInclude(r => r.RatedRole) 
                .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task AddAsync(Mandadito mandadito)
        {
            _context.Mandaditos.Add(mandadito);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var mandadito = await _context.Mandaditos.FindAsync(id);
            if (mandadito is null) return false;

            _context.Mandaditos.Remove(mandadito);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Dictionary<string, List<Mandadito>>> GetHistoryMandaditos(int userId)
        {
            var mandaditos = await _context.Mandaditos
                .Include(m => m.Post)
                .ThenInclude(p => p.PickUpLocation)
                .Include(m => m.Post)
                .ThenInclude(p => p.DeliveryLocation)
                .Include(m => m.Offer)
                .Where(m => m.Post.IdPosterUser == userId)
                .OrderByDescending(m => m.AcceptedAt)
                .ToListAsync();

            var history = new Dictionary<string, List<Mandadito>>();
        
            foreach (var mandadito in mandaditos)
            {
                var dateKey = GetDateKey(mandadito.AcceptedAt);
            
                if (!history.ContainsKey(dateKey))
                {
                    history[dateKey] = new List<Mandadito>();
                }
            
                history[dateKey].Add(mandadito);
            }

            return history;
        }
        
        public async Task<Dictionary<string, List<Mandadito>>> GetHistoryMandaditosLikeRunner(int userId)
        {
            var mandaditos = await _context.Mandaditos
                .Include(m => m.Post)
                .ThenInclude(p => p.PickUpLocation)
                .Include(m => m.Post)
                .ThenInclude(p => p.DeliveryLocation)
                .Include(m => m.Offer)
                .Where(m => m.Offer.IdUserCreator == userId)
                .OrderByDescending(m => m.AcceptedAt)
                .ToListAsync();

            var history = new Dictionary<string, List<Mandadito>>();
        
            foreach (var mandadito in mandaditos)
            {
                var dateKey = GetDateKey(mandadito.AcceptedAt);
            
                if (!history.ContainsKey(dateKey))
                {
                    history[dateKey] = new List<Mandadito>();
                }
            
                history[dateKey].Add(mandadito);
            }

            return history;
        }
        
        private string GetDateKey(DateTime fecha)
        {
            var today = DateTime.Today;
            var yesterday = today.AddDays(-1);

            if (fecha.Date == today)
                return "Hoy";
            if (fecha.Date == yesterday)
                return "Ayer";
        
            return fecha.ToString("d 'de' MMMM");
        }

        public async Task<bool> UpdateAsync(Mandadito mandadito)
        {
            _context.Mandaditos.Update(mandadito);
            return await _context.SaveChangesAsync() > 0;
        }
        
        public async Task<int> DeliveriesCount(int idUser)
        {

            var count = await _context.Mandaditos
                .Join(
                    _context.Offers,
                    mandadito => mandadito.IdOffer,
                    offer => offer.Id,
                    (mandadito, offer) => new { mandadito, offer }
                    )
                .Where(m => m.offer.IdUserCreator == idUser && m.offer.Accepted == true)
                .CountAsync();
     
            Console.WriteLine($"{count}");

            return count;
        }
    }
}