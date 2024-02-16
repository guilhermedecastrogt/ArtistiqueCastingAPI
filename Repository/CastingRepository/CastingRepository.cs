using ArtistiqueCastingAPI.Data;
using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace ArtistiqueCastingAPI.Repository;

public class CastingRepository : GenericsRepository<CastingModel>, ICastingRepository
{
    private readonly DbContextOptions<DataContext> _context;
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly ICategoryRepository _categoryRepository;
    public CastingRepository()
    {
        _context = new DbContextOptions<DataContext>();
        _subCategoryRepository = new SubCategoryRepository();
        _categoryRepository = new CategoryRespository();
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

            List<CastingModel>? castingsByCategory = new List<CastingModel>();
            List<SubCategoryModel>? subCategorys = await data.SubCategory.AsNoTracking()
                .Include(s => s.Categories)
                .Where(x => x.Categories.Any(x => x.Slug == categorySlug))
                .ToListAsync();
            
            foreach (SubCategoryModel item in subCategorys)
            {
                List<CastingModel>? castings = await data.Casting.AsNoTracking()
                    .Include(x => x.SubCategorys)
                    .Where(x => x.SubCategorys.Any(x => x.Slug == item.Slug))
                    .ToListAsync();
                castingsByCategory.AddRange(castings);
            }

            return castingsByCategory;
        }
    }

    public async Task<List<CastingModel>> SearchCastingByName(string modelSearchByName)
    {
        using (var data = new DataContext(_context))
        {
            List<CastingModel> results = await data.Casting.Where(e => EF.Functions.Like(
                EF.Functions.Collate(
                        e.Name, "SQL_Latin1_General_CP1_CI_AS"),
                "%" + modelSearchByName.ToLower() + "%")).ToListAsync();
            return results;
        }
    }

    public async Task<List<CastingModel>> GetExclusives()
    {
        using (var data = new DataContext(_context))
        {
            return await data.Casting.AsNoTracking()
                .Where(x => x.IsExclusive == true).ToListAsync();
        }
    }

    public async Task<List<CastingTableModel>> ListCastingTable()
    {
        using (var data = new DataContext(_context))
        {
            List<CastingModel> list = await data.Casting.AsNoTracking().ToListAsync();
        
            var CastingTableModel = new List<CastingTableModel>();

            int i = 0;
            foreach (var item in list)
            {
                var castingTableModel = new CastingTableModel
                {
                    Image = item.Image,
                    Name = item.Name
                };
                
                var subCategories = await _subCategoryRepository.GetSubCategoriesByCasting(item.Id);

                if (subCategories.Any())
                {
                    List<string> subCategoriesName = subCategories.Select(subCategory => subCategory.Name).ToList();
                    
                    castingTableModel.SubCategories = subCategoriesName;
                
                    var category = await _categoryRepository.GetCategoriesBySubCategory(subCategories[0].Slug);

                    if (category.Any())
                    {
                        castingTableModel.Category = category[0].Name;
                    }
                }
                CastingTableModel.Add(castingTableModel);
                Console.WriteLine("ADICIONOU: "+i);
                i++;
            }
            return CastingTableModel;
        }
    }
}