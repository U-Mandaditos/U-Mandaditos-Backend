using Aplication.Interfaces.Posts;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostRepository: IPostRepository
{
    private BackendDbContext _context;
    
    public PostRepository(BackendDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await _context.Posts
            .Include(p => p.PosterUser) 
            .Include(p => p.PickUpLocation)
            .Include(p => p.DeliveryLocation) 
            .ToListAsync();
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        return await _context.Posts
            .Include(p => p.PosterUser) // Incluir usuario
            .Include(p => p.PickUpLocation) // Incluir ubicación de recogida
            .Include(p => p.DeliveryLocation) // Incluir ubicación de entrega
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public async Task AddAsync(Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Post post)
    {
        _context.Posts.Update(post);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post is null) return false;
        _context.Posts.Update(post);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> MarkAsAcceptedAsync(int idPost)
    {
        var post = await _context.Posts.FindAsync(idPost);
        if (post is null) return false;
        post.Accepted = true;
        _context.Posts.Update(post);
        return await _context.SaveChangesAsync() > 0;
    }
    
    public async Task<IEnumerable<Post>> GetPostsByLocationIdAsync(int idLocation)
    {
        var posts = await _context.Posts
            .Where(p => p.IdPickUpLocation.HasValue && p.IdPickUpLocation.Value == idLocation)
            .Include(p => p.PickUpLocation)
            .Include(p => p.DeliveryLocation)
            .Include(p => p.PosterUser)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        Console.WriteLine($"Posts encontrados: {posts.Count}");
        return posts;
    }
    
    public async Task<IEnumerable<Post>> GetPostsByPosterUserId(int idPosterUser)
    {
        return await _context.Posts
            .Include(p => p.PosterUser)
            .Include(p => p.PosterUser!.ProfilePic) // Carga la relación ProfilePic
            .Include(p => p.PickUpLocation)
            .Include(p => p.DeliveryLocation)
            .Where(p => p.IdPosterUser == idPosterUser)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new Post 
            {
                Id = p.Id,
            
                PosterUser = p.PosterUser != null ? new User
                {
                    Name = p.PosterUser.Name,
                    LastLocation = p.PosterUser.LastLocation,
                    Rating = p.PosterUser.Rating,
                    ProfilePic = p.PosterUser.ProfilePic 
                } : null,
            
                PickUpLocation = p.PickUpLocation != null ? new Location
                {
                    Latitude = p.PickUpLocation.Latitude,
                    Longitude = p.PickUpLocation.Longitude,
                    Name = p.PickUpLocation.Name
                } : null,
            
                DeliveryLocation = p.DeliveryLocation != null ? new Location
                {
                    Latitude = p.DeliveryLocation.Latitude,
                    Longitude = p.DeliveryLocation.Longitude,
                    Name = p.DeliveryLocation.Name
                } : null,
            
                // Resto de propiedades
                CreatedAt = p.CreatedAt,
                // ...
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetPostsActive(int idPosterUser)
    {
        
        return await _context.Posts
            .Include(p => p.PosterUser)
            .Include(p => p.PosterUser!.ProfilePic) // Carga la relación ProfilePic
            .Include(p => p.PickUpLocation)
            .Include(p => p.DeliveryLocation)
            .Where(p => p.IdPosterUser == idPosterUser)
            .Where(p=>p.Completed == false)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new Post 
            {
                Id = p.Id,
                Title = p.Title,  
                Description = p.Description, 
                SugestedValue = p.SugestedValue,
            
                PosterUser = p.PosterUser != null ? new User
                {
                    Name = p.PosterUser.Name,
                    LastLocation = p.PosterUser.LastLocation,
                    Rating = p.PosterUser.Rating,
                    ProfilePic = p.PosterUser.ProfilePic 
                } : null,
            
                PickUpLocation = p.PickUpLocation != null ? new Location
                {
                    Latitude = p.PickUpLocation.Latitude,
                    Longitude = p.PickUpLocation.Longitude,
                    Name = p.PickUpLocation.Name
                } : null,
            
                DeliveryLocation = p.DeliveryLocation != null ? new Location
                {
                    Latitude = p.DeliveryLocation.Latitude,
                    Longitude = p.DeliveryLocation.Longitude,
                    Name = p.DeliveryLocation.Name
                } : null,
            
                // Resto de propiedades
                CreatedAt = p.CreatedAt,
                // ...
            })
            .ToListAsync();
    }
}