using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository.Generics;

namespace ArtistiqueCastingAPI.Repository.MapsRepository;

public interface ISubCategoryCategoryRepository : IGenericsRepository<SubCategoryCategoryRepository>
{
    Task<bool> Add(string subCategorySlug, string categorySlug);
    Task<bool> Delete(string subCategorySlug, string categorySlug);
    Task<List<CategoryModel>> GetCategoriesBySubCategory(string slugSubCategory);
}