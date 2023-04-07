using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.RabbitMq;
using Infrastructure.RabbitMqWrapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddRabbitMqServices
    (
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase(databaseName: "rabbit"));
        services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
        services.AddSingleton<IPooledObjectPolicy<IModel>, RabbitModelPooledObjectPolicy>();
        services.AddSingleton<IRabbitMqManager, RabbitMqManager>();
    }
}