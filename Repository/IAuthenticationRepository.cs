using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository.Generics;

namespace ArtistiqueCastingAPI.Repository;

public interface IAuthenticationRepository : IGenericsRepository<AuthenticationModel>
{
    Task<AuthenticationModel> GetLogin(AuthenticationModel model);
}