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
    public class GetAllProductsQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly GetAllProductsQueryHandler _handler;
        public GetAllProductsQueryHandlerTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();

            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new GetAllProductsQueryHandler(_productRepositoryMock.Object, _mapper); 
        }

        [Fact]
        public async Task Handle_ReturnsListOfProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Test Product 1",
                    Brand = "Test Brand 1",
                    Size = "M",
                    Price = new Price { Id = 1, Amount = 99.99M },
                    Stock = new Stock { Id = 1, Quantity = 10 }
                },
                new Product
                {
                    Id = 2,
                    Name = "Test Product 2",
                    Brand = "Test Brand 2",
                    Size = "L",
                    Price = new Price { Id = 2, Amount = 199.99M },
                    Stock = new Stock { Id = 2, Quantity = 20 }
                }
            };
            _productRepositoryMock.Setup(repo => repo.GetAllProductsAsync())
             .ReturnsAsync(products);

            var query = new GetAllProductsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ProductDTO>>(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(products[0].Id, result[0].Id);
            Assert.Equal(products[0].Name, result[0].Name);
            Assert.Equal(products[0].Brand, result[0].Brand);
            Assert.Equal(products[0].Size, result[0].Size);
            Assert.Equal(products[0].Price.Amount, result[0].Price);
            Assert.Equal(products[0].Stock.Quantity, result[0].Stock);
            Assert.Equal(products[1].Id, result[1].Id);
            Assert.Equal(products[1].Name, result[1].Name);
            Assert.Equal(products[1].Brand, result[1].Brand);
            Assert.Equal(products[1].Size, result[1].Size);
            Assert.Equal(products[1].Price.Amount, result[1].Price);
            Assert.Equal(products[1].Stock.Quantity, result[1].Stock);
        }
    }
}
