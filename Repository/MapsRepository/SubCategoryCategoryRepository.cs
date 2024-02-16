using ArtistiqueCastingAPI.Data;
using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace ArtistiqueCastingAPI.Repository.MapsRepository;

public class SubCategoryCategoryRepository : GenericsRepository<SubCategoryCategoryRepository>, ISubCategoryCategoryRepository
{
    private readonly DbContextOptions<DataContext> _context;

    public SubCategoryCategoryRepository()
    {
        _context = new DbContextOptions<DataContext>();
    }
    
    public async Task<bool> Add(string subCategorySlug, string categorySlug)
    {
        try
        {
            using (var data = new DataContext(_context))
            {
                var subCategoryCategory = new SubCategoryCategoryModel()
                {
                    CategorySlug = categorySlug,
                    SubCategorySlug = subCategorySlug
                };
                await data.SubCategoryCategory.AddAsync(subCategoryCategory);
                await data.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public async Task<bool> Delete(string subCategorySlug, string categorySlug)
    {
        try
        {
            using (var data = new DataContext(_context))
            {
                var subCategoryCategory = await data.SubCategoryCategory.FirstOrDefaultAsync(x => x.CategorySlug == categorySlug && x.SubCategorySlug == subCategorySlug);
                data.SubCategoryCategory.Remove(subCategoryCategory);
                await data.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<List<CategoryModel>> GetCategoriesBySubCategory(string slugSubCategory)
    {
        using (var data = new DataContext(_context))
        {
            List<SubCategoryCategoryModel> list = await data.SubCategoryCategory
                .Where(x => x.SubCategorySlug == slugSubCategory).ToListAsync();

            var listCategory = new List<CategoryModel>();
            foreach (var item in list)
            {
                CategoryModel? category = await data.Category.FirstOrDefaultAsync
                    (x => x.Slug == item.CategorySlug);
                if (category != null) listCategory.Add(category);
            }

            return listCategory;
        }
    }
}