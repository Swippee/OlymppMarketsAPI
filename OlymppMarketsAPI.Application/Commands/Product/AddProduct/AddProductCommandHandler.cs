using MediatR;
using OlymppMarketsAPI.Domain.Entities;
using OlymppMarketsAPI.Domain.Interfaces;

namespace OlymppMarketsAPI.Application.Commands
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, int>
    {
        private readonly IProductRepository _productRepository;

        public AddProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {

            var product = new Product
            {
                Name = request.Name,
                Brand = request.Brand,
                Size = request.Size,
                Price = new Price
                {
                    Amount = request.Price
                },
                Stock = new Stock
                {
                    Quantity = request.Stock
                }
            };

            await _productRepository.CreateProductAsync(product);

            return product.Id;
        }


    }
}
