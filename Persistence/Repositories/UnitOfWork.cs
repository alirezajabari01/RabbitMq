using Domain.Interfaces;
using Infrastructure.Database;

namespace Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _databaseContext;
    public IProductRepository ProductRepository { get; }

    public UnitOfWork(DatabaseContext databaseContext,IProductRepository productRepository)
    {
        _databaseContext = databaseContext;
        ProductRepository = productRepository;
    }
    public async Task<int> Complete(CancellationToken cancellationToken)
    {
        return await _databaseContext.SaveChangesAsync(cancellationToken);
    }
}