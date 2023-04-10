using MediatR;

namespace Application.ProductHandlers.Commands.SetToDatabase;

public record SetToDatabaseProductCommand
(
    string QueueName
) : IRequest<int>;