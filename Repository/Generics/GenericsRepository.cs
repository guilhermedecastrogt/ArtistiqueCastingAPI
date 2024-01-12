using System.Runtime.InteropServices;
using ArtistiqueCastingAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;

namespace ArtistiqueCastingAPI.Repository.Generics;

public class GenericsRepository<T> : IGenericsRepository<T> where T : class
{
    private readonly DbContextOptions<DataContext> _context;

    public GenericsRepository()
    {
        _context = new DbContextOptions<DataContext>();
    }
    
    public async Task Add(T obj)
    {
        using(var data = new DataContext(_context))
        {
            await data.Set<T>().AddAsync(obj);
            await data.SaveChangesAsync();
        }
    }

    public async Task Update(T obj)
    {
        using(var data = new DataContext(_context))
        {
            data.Set<T>().Update(obj);
            await data.SaveChangesAsync();
        }
    }

    public async Task Delete(T obj)
    {
        using (var data = new DataContext(_context))
        {
            data.Set<T>().Remove(obj);
            await data.SaveChangesAsync();
        }
    }

    public async Task<T> GetEntityById(Guid id)
    {
        using (var data = new DataContext(_context))
        {
            var entity = await data.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new Exception("Entity not found");
            }
            return entity;
        }
    }

    public async Task<List<T>> List()
    {
        using (var data = new DataContext(_context))
        {
            return await data.Set<T>().ToListAsync();
        }
    }
    
    // To detect redundant calls
    bool _disposed = false;
    readonly SafeHandle _handle = new SafeFileHandle(IntPtr.Zero, true);

    
    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _handle.Dispose();
            // Free any other managed objects here.
            //
        }

        _disposed = true;
    }
}


