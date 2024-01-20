using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository.Generics;

namespace ArtistiqueCastingAPI.Repository;

public interface ISubCategoryRepository : IGenericsRepository<SubCategoryModel>
{
    Task<List<SubCategoryModel>> GetByCategory(string slugCategory);
    Task<SubCategoryModel> GetBySlug(string slug);
    Task<List<SubCategoryModel>> GetSubCategoriesByCasting(Guid castingId);
}