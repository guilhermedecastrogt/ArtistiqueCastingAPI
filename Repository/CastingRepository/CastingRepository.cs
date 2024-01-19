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
        using (var data = new DataContext(_context))
        {
            if (subCategorySlug != null)
            {
                List<CastingModel>? castings = await data.Casting.AsNoTracking()
                    .Include(x => x.SubCategorys)
                    .Where(x => x.SubCategorys.Any(x => x.Slug == subCategorySlug))
                    .ToListAsync();
                
                if (castings == null)
                {
                    throw new Exception("Nenhum casting encontrado");
                }
                return castings;
            }
            throw new Exception("Nenhum casting encontrado");
        }
    }
}