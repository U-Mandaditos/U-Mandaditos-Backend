using Aplication.Interfaces.Offers;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly BackendDbContext _context;

        public OfferRepository(BackendDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Offer>> GetOffersByPostId(int idPost)
        {
            return await _context.Offers
                .Include(o => o.UserCreator)
                .Include(o => o.UserCreator)
                    .ThenInclude(u => u!.ProfilePic)
                .Include(o => o.Post)
                .Where(o => o.IdPost == idPost)
                .ToListAsync();
        }

        public async Task<Offer?> GetByIdAsync(int id)
        {
            return await _context.Offers.FindAsync(id);
        }

        public async Task AddAsync(Offer offer)
        {
            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var offer = await _context.Offers.FindAsync(id);
            if (offer is null) return false;

            _context.Offers.Remove(offer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Offer offer)
        {
            _context.Offers.Update(offer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Offer>> QuantityOffersAcceptedByUserAsync(int userId)
        {
            return await _context.Offers.Where(o => o.IdUserCreator == userId && o.Accepted).ToListAsync();
        }

        public async Task<bool> UpdateOfferStateAsync(int offerId, bool isAccepted)
        {
            var offer = await _context.Offers.FindAsync(offerId);
            if (offer is null) return false;

            offer.Accepted = isAccepted;
            _context.Offers.Update(offer);
            return await _context.SaveChangesAsync() > 0;
        }


        /* Obtienes las ofertas que han sido aceptadas de un usuario determinado clasificadas por fecha*/
        public async Task<IEnumerable<Offer>> GetOffersAcceptedByUserAsync(int userId)
        {
            return await _context.Offers
                    .Include(o => o.Post)
                    .Include(o => o.UserCreator)
                    .Where(o => o.IdUserCreator == userId && o.Accepted)
                    .OrderByDescending(o => o.CreatedAt)
                    .ToListAsync();
        }
    }
}


