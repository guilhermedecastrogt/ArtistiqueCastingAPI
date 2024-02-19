using System.Globalization;
using System.Text;
using ArtistiqueCastingAPI.Data;
using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using ArtistiqueCastingAPI.Services;

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
        modelSearchByName = modelSearchByName.Replace('-', ' ');
        modelSearchByName = modelSearchByName?.Trim();

        if (string.IsNullOrEmpty(modelSearchByName))
        {
            return new List<CastingModel>();
        }

        using (var data = new DataContext(_context))
        {
            string collation = "SQL_Latin1_General_CP1_CI_AS";

            var allRecords = await data.Casting.ToListAsync();

            var results = allRecords
                .Select(casting => new
                {
                    Casting = casting,
                    Similarity = CastingServices.CalculateLevenshteinSimilarity(
                        modelSearchByName.ToLower(), casting.Name.ToLower()
                    )
                })
                .Where(result => result.Similarity <= 2)
                .OrderBy(result => result.Similarity)
                .Select(result => result.Casting)
                .ToList();

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
            List<CastingModel> list = await data.Casting.AsNoTracking().Take(30).ToListAsync();
            await Task.Delay(1000);
            if (list.Count == 30)
            {
                var tempList = await data.Casting.AsNoTracking().Skip(30).Take(30).ToListAsync();
                
                while (tempList.Count > 0)
                {
                    list.AddRange(tempList);
                    await Task.Delay(1000);
                    tempList = null;
                    tempList = await data.Casting.AsNoTracking().Skip(list.Count).Take(30).ToListAsync();
                }
            }
            {
                var tempList = await data.Casting.AsNoTracking().Skip(30).Take(30).ToListAsync();
                
                while (tempList != null)
                {
                    list.AddRange(tempList);
                    await Task.Delay(1000);
                    tempList = null;
                    tempList = await data.Casting.AsNoTracking().Skip(list.Count).Take(30).ToListAsync();
                }
            }
        
            var CastingTableModel = new List<CastingTableModel>();

            foreach (var item in list)
            {
                var castingTableModel = new CastingTableModel
                {
                    Image = item.Image,
                    Name = item.Name,
                    Id = item.Id
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
            }
            Console.WriteLine("ADICIONOU");
            return CastingTableModel;
        }
    }

    public async Task<List<CastingGeralModel>> ListGeral(int page)
    {
        page = page * 16;
        using (var data = new DataContext(_context))
        {
            var castings = await data.Casting
                .AsNoTracking()
                .Skip(page)
                .Take(16)
                .ToListAsync();
            
            var castingsGeral = new List<CastingGeralModel>();
            
            foreach (var item in castings)
            {
                castingsGeral.Add(new CastingGeralModel
                {
                    id = item.Id,
                    image = item.Image,
                    name = item.Name
                });
            }
            
            return castingsGeral;
        }
    }

    public async Task<int> CountCasting()
    {
        using (var data = new DataContext(_context))
        {
            return await data.Casting.CountAsync();
        }
    }
}