using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository.Generics;

namespace ArtistiqueCastingAPI.Repository;

public interface ICategoryRepository : IGenericsRepository<CategoryModel>
{
    Task<CategoryModel> GetBySlug(string slug);
}