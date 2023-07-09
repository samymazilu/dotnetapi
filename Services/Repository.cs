using Microsoft.EntityFrameworkCore;
using MyAPI;
namespace MyAPI.Services;
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly MyDbContext Context;

    public Repository(MyDbContext context)
    {
        Context = context;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await Context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public void Add(T entity)
    {
        Context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        Context.Set<T>().Remove(entity);
    }
}
