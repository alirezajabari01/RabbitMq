namespace Domain.Interfaces;

public interface IUnitOfWork
{
    public IProductRepository ProductRepository { get;  }
    Task<int> Complete(CancellationToken cancellationToken);
}