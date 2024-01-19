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
                .Include(c => c.Category)
                .Where(c => c.Category.Slug == slugCategory)
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
}