using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository.Generics;

namespace ArtistiqueCastingAPI.Repository.MapsRepository;

public interface ICastingSubCategoryRepository : IGenericsRepository<CastingSubCategoryModel>
{
    void Add(Guid castingId, string subCategorySlug);
}