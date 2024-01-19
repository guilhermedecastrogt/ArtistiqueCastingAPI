using ArtistiqueCastingAPI.Data;
using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace ArtistiqueCastingAPI.Repository;

public class CastingRepository : GenericsRepository<CastingModel>, ICastingRepository
{
    private readonly DbContextOptions<DataContext> _context;
    public CastingRepository()
    {
        _context = new DbContextOptions<DataContext>();
    }

    public async Task<List<CastingModel>> FilterByCategoryAndSubCategory(string categorySlug, string? subCategorySlug)
    {
        throw new NotImplementedException();
    }
}