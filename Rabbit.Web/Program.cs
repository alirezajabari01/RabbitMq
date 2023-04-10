using System.Reflection;
using Application;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.RabbitMq;
using Infrastructure.RabbitMqFluentBuilder;
using Infrastructure.RabbitMqWrapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using Persistence;
using Persistence.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Core.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var configure = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase(databaseName: "rabbit"));
var rabbitConfig = configure.GetSection("rabbit");
builder.Services.Configure<RabbitOptions>(rabbitConfig);
builder.Services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
builder.Services.AddSingleton<IPooledObjectPolicy<IModel>, RabbitModelPooledObjectPolicy>();
builder.Services.AddSingleton<IRabbitMqManager, RabbitMqManager>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IQueueDeclareBuilder, RabbitMqManager>();
builder.Services.AddApplicationServices();
// builder.Services.AddRabbitMqServices(configure);
// builder.Services.AddPersistenceServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();