namespace ArtistiqueCastingAPI.Repository.Generics;

public interface IGenericsRepository<T> where T : class
{
    Task Add(T obj);
    Task Update(T obj);
    Task Delete(T obj);
    Task<T> GetEntityById(Guid id);
    Task<List<T>> List();
}