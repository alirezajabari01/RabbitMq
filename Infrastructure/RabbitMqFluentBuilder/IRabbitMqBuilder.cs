namespace Infrastructure.RabbitMqFluentBuilder;

public interface IRabbitMqBuilder
{
}

public interface IQueueDeclareBuilder
{
    IDeclareExchange DeclareQueue(string queueName, bool durable, bool exclusive, bool autoDelete);
}

public interface IDeclareExchange
{
    IBindExchange DeclareExchange(string exchange, string type, bool durable, bool autoDelete);
}

public interface IBindExchange
{
    IPublisher BindExchangeToQueue(string queueName, string exchangeName, string routingKey);
}

public interface IPublisher
{
    IBindExchange Publish(string exchange, string routingKey, byte[] body);
}