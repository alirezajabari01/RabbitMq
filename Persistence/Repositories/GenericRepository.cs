using Domain.Interfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(DatabaseContext context)
    {
        _dbSet = context.Set<TEntity>();
    }
    public void Create(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public TEntity GetById(long id)
    {
        return _dbSet.Find(id)!;
    }

    public List<TEntity> GetAll()
    {
        return _dbSet.ToList();
    }

    public bool Any(Func<TEntity, bool> expression)
    {
        throw new NotImplementedException();
    }
}