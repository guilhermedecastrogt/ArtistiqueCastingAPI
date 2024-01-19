using ArtistiqueCastingAPI.Repository.Generics;

namespace ArtistiqueCastingAPI.Repository.MapsRepository;

public interface ISubCategoryCategoryRepository : IGenericsRepository<SubCategoryCategoryRepository>
{
    void Add(string subCategorySlug, string categorySlug);
}