using System.Data;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.ProductHandlers.Commands.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand,int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
           Name = "aa",
           UniqueCode = "354"
        };

        if(_unitOfWork.ProductRepository.Any(a => a.Id == 2))
        {
            throw new DuplicateNameException();
        }

        return Task.FromResult(0);
    }
}