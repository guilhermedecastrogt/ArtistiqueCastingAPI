using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository.Generics;

namespace ArtistiqueCastingAPI.Repository.MapsRepository;

public interface ISubCategoryCategoryRepository : IGenericsRepository<SubCategoryCategoryRepository>
{
    void Add(string subCategorySlug, string categorySlug);
    void Delete(string subCategorySlug, string categorySlug);
    Task<List<CategoryModel>> GetCategoriesBySubCategory(string slugSubCategory);
    void DeleteAllCategoriesOfSubCategory(string slugSubCategory);
}