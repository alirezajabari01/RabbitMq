using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;

namespace Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>,IProductRepository
{
    public ProductRepository(DatabaseContext context) : base(context)
    {
    }
}