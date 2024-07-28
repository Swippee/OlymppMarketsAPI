using Moq;
using OlymppMarketsAPI.Application.Commands;
using OlymppMarketsAPI.Domain.Entities;
using OlymppMarketsAPI.Domain.Interfaces;


namespace OlymppMarketsAPI.Tests.Handlers
{
    public class AddProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly AddProductCommandHandler _handler;

        public AddProductCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new AddProductCommandHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_AddsProductSuccessfully()
        {
            // Arrange
            var command = new AddProductCommand
            {
                Name = "New Product",
                Brand = "New Brand",
                Size = "L",
                Price = 49.99M,
                Stock = 20
            };

            _productRepositoryMock.Setup(repo => repo.CreateProductAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _productRepositoryMock.Verify(repo => repo.CreateProductAsync(It.Is<Product>(p =>
                p.Name == command.Name &&
                p.Brand == command.Brand &&
                p.Size == command.Size &&
                p.Price.Amount == command.Price &&
                p.Stock.Quantity == command.Stock)), Times.Once);
        }
    }
}
