using ClientHub.Domain.Abstractions;

namespace ClientHub.Infrastructure.Repositories;

internal abstract class Repository<T>
    where T : Entity
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task AddAsync(T entity)
    {
        await DbContext.AddAsync(entity);
    }

    public void Update(T entity)
    {
        DbContext.Update(entity);
    }

    public async Task RemoveAsync(Guid id)
    {
        var entity = await DbContext.FindAsync(typeof(T), id);

        DbContext.Remove(entity);
    }
}