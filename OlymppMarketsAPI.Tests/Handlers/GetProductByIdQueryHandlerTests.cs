using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using OlymppMarketsAPI.Application.Mappings;
using OlymppMarketsAPI.Application.Queries;
using OlymppMarketsAPI.Domain.Entities;
using OlymppMarketsAPI.Domain.Interfaces;
using OlymppMarketsAPI.DTOs;
using OlymppMarketsAPI.Infrastructure.Data;
using OlymppMarketsAPI.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OlymppMarketsAPI.Tests.Handlers
{
    public class GetProductByIdQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly GetProductByIdQueryHandler _handler;
        public GetProductByIdQueryHandlerTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();

            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new GetProductByIdQueryHandler(_productRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ReturnsProductDto()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Brand = "Test Brand",
                Size = "M",
                Price = new Price { Id = 1, Amount = 99.99M },
                Stock = new Stock { Id = 1, Quantity = 10 }
            };

            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(1))
                .ReturnsAsync(product);

            var query = new GetProductByIdQuery(1);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Brand, result.Brand);
            Assert.Equal(product.Size, result.Size);
            Assert.Equal(product.Price.Amount, result.Price);
            Assert.Equal(product.Stock.Quantity, result.Stock);
        }
    }
}
