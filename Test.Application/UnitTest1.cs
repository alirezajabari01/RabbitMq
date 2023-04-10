using System.Data;
using Application.ProductHandlers.Commands;
using Application.ProductHandlers.Commands.SetToDatabase;
using Domain.Interfaces;
using Moq;
using Shouldly;

namespace Test.Application;

public class UnitTest1
{
    private readonly Mock<IRabbitMqManager> _rabbitMqManager;
    private readonly Mock<IUnitOfWork> _unitOfWork;

    public UnitTest1()
    {
        _rabbitMqManager = new();
        _unitOfWork = new();
    }

    [Fact]
    public async Task Handle_Should_ThrowException_When_Message_Is_Null()
    {
        //Arrange
        var setToDatabaseProductCommand = new SetToDatabaseProductCommand(It.IsAny<string>());


        var handler = new SetToDatabaseProductCommandHandler(_rabbitMqManager.Object, _unitOfWork.Object);

        //Act
        _rabbitMqManager.Setup
            (
                x =>
                    x.ConsumeMessage(It.IsAny<string>())
            )
            .Returns(Array.Empty<byte>());
        
        //Assert
        var actual = () => handler.Handle(setToDatabaseProductCommand, default);
        await actual.ShouldThrowAsync<NullReferenceException>();
    }
}