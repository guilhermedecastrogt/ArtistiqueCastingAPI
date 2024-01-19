using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository.Generics;

namespace ArtistiqueCastingAPI.Repository;

public interface ICastingRepository : IGenericsRepository<CastingModel>
{
    Task<List<CastingModel>> FilterByCategoryAndSubCategory(string categorySlug, string? subCategorySlug);
}