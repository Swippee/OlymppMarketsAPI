using AutoMapper;
using MediatR;
using Moq;
using OlymppMarketsAPI.Application.Commands;
using OlymppMarketsAPI.Application.Mappings;
using OlymppMarketsAPI.Domain.Entities;
using OlymppMarketsAPI.Domain.Interfaces;
using OlymppMarketsAPI.DTOs;


namespace OlymppMarketsAPI.Tests.Handlers
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly UpdateProductCommandHandler _handler;

        public UpdateProductCommandHandlerTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();

            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new UpdateProductCommandHandler(_productRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_UpdateProductSuccessfully()
        {
            // Arrange
            var productDto = new ProductDTO
            {
                Id = 1,
                Name = "Updated Product",
                Brand = "Updated Brand",
                Size = "M",
                Price = 199.99M,
                Stock = 20
            };

            var product = new Product
            {
                Id = 1,
                Name = "Updated Product",
                Brand = "Updated Brand",
                Size = "M",
                Price = new Price { Id = 1, Amount = 199.99M },
                Stock = new Stock { Id = 1, Quantity = 20 }
            };

            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(1)).ReturnsAsync(product);
            _productRepositoryMock.Setup(repo => repo.UpdateProductAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

            var command = new UpdateProductCommand(productDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _productRepositoryMock.Verify(repo => repo.UpdateProductAsync(It.Is<Product>(p =>
                p.Id == productDto.Id &&
                p.Name == productDto.Name &&
                p.Brand == productDto.Brand &&
                p.Size == productDto.Size &&
                p.Price.Amount == productDto.Price &&
                p.Stock.Quantity == productDto.Stock
            )), Times.Once);

            Assert.Equal(Unit.Value, result);
        }
    }
}
