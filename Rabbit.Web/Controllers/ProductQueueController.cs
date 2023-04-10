using Application.ProductHandlers.Commands;
using Application.ProductHandlers.Commands.SetToDatabase;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using Infrastructure.RabbitMqFluentBuilder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Rabbit.Web.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductQueueController : ControllerBase
{
    private readonly IQueueDeclareBuilder _builder;
    private readonly IRabbitMqManager _rabbitMqManager;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public ProductQueueController(IQueueDeclareBuilder builder, IRabbitMqManager rabbitMqManager, IMediator mediator, IUnitOfWork unitOfWork)
    {
        _builder = builder;
        _rabbitMqManager = rabbitMqManager;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public List<Product> GetAllProducts()
    {
        return _unitOfWork.ProductRepository.GetAll();
    }
    [HttpPost]
    public async Task<IActionResult> SetToDatabaseProduct(string queueName)
    {
        var response = await _mediator.Send
        (
            new SetToDatabaseProductCommand(queueName)
        );

        return Ok(response);
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

    [HttpPost]
    public void SendMessageToQueue([FromBody] Product message, string exchange, string routingKey = "")
    {
        var body = message.EncodeMessage();
        _builder
            .Publish(exchange, routingKey, body);
    }

    [HttpGet]
    public IActionResult GetMessage(string queue)
    {
        return Ok
        (
            _rabbitMqManager.ConsumeMessage(queue)
                .DecodeMessage<Product>()
        );
    }
}