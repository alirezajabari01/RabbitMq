namespace Domain.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    void Create(TEntity entity);
    void Update(TEntity entity);
    TEntity GetById(long id);
    List<TEntity> GetAll();
    bool Any(Func<TEntity, bool> expression);
}