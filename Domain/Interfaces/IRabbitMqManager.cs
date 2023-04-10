namespace Domain.Interfaces;

public interface IRabbitMqManager
{
    byte[] ConsumeMessage(string queueName);

}