using ArtistiqueCastingAPI.Data;
using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace ArtistiqueCastingAPI.Repository;

public class SubCategoryRepository: GenericsRepository<SubCategoryModel>, ISubCategoryRepository
{
    private readonly DbContextOptions<DataContext> _context;

    public SubCategoryRepository()
    {
        _context = new DbContextOptions<DataContext>();
    }
    public async Task<List<SubCategoryModel>> GetByCategory(string slugCategory)
    {
        using(var data = new DataContext(_context))
        {
            List<SubCategoryModel> subCategories = await data.SubCategory
                .Include(x => x.Categories)
                .Where(x => x.Categories.Any(x => x.Slug == slugCategory))
                .ToListAsync();
            
            return subCategories;
        }
    }

    public async Task<SubCategoryModel> GetBySlug(string slug)
    {
        using (var data = new DataContext(_context))
        {
            SubCategoryModel? subcategory = await data.SubCategory
                .FirstOrDefaultAsync(x => x.Slug == slug);
            if (subcategory == null)
            {
                throw new Exception("Categoria não encontrada");
            }
            return subcategory;
        }
    }

    public async Task<List<SubCategoryModel>> GetSubCategoriesByCasting(Guid castingId)
    {
        using (var data = new DataContext(_context))
        {
            return await data.SubCategory
                .Include(x => x.Castings)
                .Where(e => e.Castings.Any(s => s.Id == castingId))
                .ToListAsync();
        }
    }
}