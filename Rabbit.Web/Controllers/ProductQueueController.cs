using System.Text;
using Domain.Interfaces;
using Infrastructure.Common;
using Infrastructure.RabbitMqFluentBuilder;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Rabbit.Web.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductQueueController : ControllerBase
{
    private readonly IQueueDeclareBuilder _builder;
    private readonly IRabbitMqManager _rabbitMqManager;

    public ProductQueueController(IQueueDeclareBuilder builder, IRabbitMqManager rabbitMqManager)
    {
        _builder = builder;
        _rabbitMqManager = rabbitMqManager;
    }

    [HttpPost]
    public void CreateQueueProduct(string message, string queue, string exchange, string exchangeType,
        string routingKey = "")
    {
        var body = message.EncodeMessage();

        _builder
            .DeclareQueue(queue, true, false, false)
            .DeclareExchange(exchange, exchangeType, true, false)
            .BindExchangeToQueue(queue, exchange, routingKey)
            .Publish(exchange, routingKey, body);
    }

    [HttpGet]
    public IActionResult GetMessage(string queue)
    {
        return Ok(_rabbitMqManager.ConsumeMessage(queue));
    }
}