using ArtistiqueCastingAPI.Data;
using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using SliftioHub.Helpers;

namespace ArtistiqueCastingAPI.Repository;

public class AuthenticationRepository : GenericsRepository<AuthenticationModel>, IAuthenticationRepository
{
    private readonly DbContextOptions<DataContext> _context;

    public AuthenticationRepository()
    {
        _context = new DbContextOptions<DataContext>();
    }
    
    public async Task<AuthenticationModel> GetLogin(AuthenticationModel model)
    {
        using (var data = new DataContext(_context))
        {
            AuthenticationModel? authentication = await data.Authentication.AsNoTracking()
                .Where(x => x.Id == model.Id && x.Password == model.Password.HashGenerate())
                .FirstOrDefaultAsync();
            if (authentication == null) return null;
            return authentication;
        }
    }
}