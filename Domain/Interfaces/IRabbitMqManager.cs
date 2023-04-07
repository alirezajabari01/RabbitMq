namespace Domain.Interfaces;

public interface IRabbitMqManager
{
    string ConsumeMessage(string queueName);
    byte[] MessageEncoder(string message);
}