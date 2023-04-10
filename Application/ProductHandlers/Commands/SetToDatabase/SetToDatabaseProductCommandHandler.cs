using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using MediatR;
using NullReferenceException = System.NullReferenceException;

namespace Application.ProductHandlers.Commands.SetToDatabase;

public class SetToDatabaseProductCommandHandler : IRequestHandler<SetToDatabaseProductCommand, int>
{
    private readonly IRabbitMqManager _rabbitMqManager;
    private readonly IUnitOfWork _unitOfWork;

    public SetToDatabaseProductCommandHandler(IRabbitMqManager rabbitMqManager, IUnitOfWork unitOfWork)
    {
        _rabbitMqManager = rabbitMqManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(SetToDatabaseProductCommand request, CancellationToken cancellationToken)
    {
        var message = _rabbitMqManager.ConsumeMessage(request.QueueName);

        if (IsMessageEmpty(message))
        {
            throw new NullReferenceException();
        }

        var product = message.DecodeMessage<Product>();

        _unitOfWork.ProductRepository.Create(product);

        return await _unitOfWork.Complete(cancellationToken);
    }

    public bool IsMessageEmpty(byte[] message)
    {
        if (message.Length <= 0)
            return true;

        return false;
    }
}