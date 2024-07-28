using MediatR;
using Moq;
using OlymppMarketsAPI.Application.Commands;
using OlymppMarketsAPI.Domain.Entities;
using OlymppMarketsAPI.Domain.Interfaces;


namespace OlymppMarketsAPI.Tests.Handlers
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly DeleteProductCommandHandler _handler;

        public DeleteProductCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new DeleteProductCommandHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeleteProductSuccessfully()
        {
            // Arrange
            var command = new DeleteProductCommand(1);
            _productRepositoryMock.Setup(repo => repo.DeleteProductAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _productRepositoryMock.Verify(repo => repo.DeleteProductAsync(1), Times.Once);
            Assert.Equal(Unit.Value, result);
        }
    }
}
