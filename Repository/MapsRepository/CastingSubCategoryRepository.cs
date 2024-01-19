using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository.Generics;

namespace ArtistiqueCastingAPI.Repository.MapsRepository;

public class CastingSubCategoryRepository : GenericsRepository<CastingSubCategoryModel>, ICastingSubCategoryRepository
{
    public async void Add(string castingSlug, string subCategorySlug)
    {
        throw new NotImplementedException();
    }
}