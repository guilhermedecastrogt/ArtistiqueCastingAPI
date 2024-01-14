using ArtistiqueCastingAPI.Data;
using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace ArtistiqueCastingAPI.Repository;

public class CategoryRespository : GenericsRepository<CategoryModel>, ICategoryRepository
{
    private readonly DbContextOptions<DataContext> _context;

    public CategoryRespository()
    {
        _context = new DbContextOptions<DataContext>();
    }
    public async Task<CategoryModel> GetBySlug(string slug)
    {
        using (var data = new DataContext(_context))
        {
            CategoryModel? category = await data.Category.FirstOrDefaultAsync(x => x.Slug == slug);
            if (category == null)
            {
                throw new Exception("Categoria não encontrada");
            }
            return category;
        }
    }
}