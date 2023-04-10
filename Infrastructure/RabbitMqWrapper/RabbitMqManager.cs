using System.Text;
using System.Text.Json;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using Infrastructure.RabbitMqFluentBuilder;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.RabbitMqWrapper;

public class RabbitMqManager : IRabbitMqBuilder, IQueueDeclareBuilder, IDeclareExchange, IBindExchange, IPublisher,
    IRabbitMqManager
{
    private readonly DefaultObjectPool<IModel> _objectPool;
    private readonly IModel _channel;

    public RabbitMqManager(IPooledObjectPolicy<IModel> objectPolicy)
    {
        _objectPool = new DefaultObjectPool<IModel>(objectPolicy, Environment.ProcessorCount * 2);
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
    }

    public IDeclareExchange DeclareQueue(string queueName, bool durable, bool exclusive, bool autoDelete)
    {
        _channel.QueueDeclare
        (
            queueName,
            durable,
            exclusive,
            autoDelete,
            null
        );
        return this;
    }

    public IPublisher BindExchangeToQueue(string queueName, string exchangeName, string routingKey)
    {
        _channel.QueueBind
        (
            queueName,
            exchangeName,
            routingKey
        );
        return this;
    }

    public byte[] MessageEncoder(string message)
    {
        return message.EncodeMessage();
    }

    public void Publish(string exchange, string routingKey, byte[] body)
    {
        _channel.BasicPublish
        (
            exchange,
            routingKey,
            null,
            body
        );
    }

    public IBindExchange DeclareExchange(string exchange, string type, bool durable, bool autoDelete)
    {
        _channel.ExchangeDeclare
        (
            exchange,
            type,
            durable,
            autoDelete
        );
        return this;
    }

    public byte[] ConsumeMessage(string queueName)
    {
        return _channel.BasicGet(queueName, true)
            .Body
            .ToArray();
    }

    public string Build()
    {
        throw new NotImplementedException();
    }
}