using ArtistiqueCastingAPI.Data;
using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace ArtistiqueCastingAPI.Repository.MapsRepository;

public class CastingSubCategoryRepository : GenericsRepository<CastingSubCategoryModel>, ICastingSubCategoryRepository
{
    private readonly DbContextOptions<DataContext> _context;

    public CastingSubCategoryRepository()
    {
        _context = new DbContextOptions<DataContext>();
    }
    public async void Add(Guid castingId, string subCategorySlug)
    {
        using (var data = new DataContext(_context))
        {
            CastingSubCategoryModel castingSubCategory = new CastingSubCategoryModel
            {
                CastingId = castingId,
                SubCategorySlug = subCategorySlug
            };
            await data.CastingSubCategory.AddAsync(castingSubCategory);
            await data.SaveChangesAsync();
        }
    }
}