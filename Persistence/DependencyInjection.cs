using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class DependencyInjection
{
    public static void AddPersistenceServices(this IServiceCollection collection)
    {
        collection.AddScoped<IProductRepository, ProductRepository>();
        collection.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        collection.AddTransient<IUnitOfWork, UnitOfWork>();
    }
}