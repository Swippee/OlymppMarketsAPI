using AutoMapper;
using MediatR;
using OlymppMarketsAPI.Domain.Entities;
using OlymppMarketsAPI.Domain.Interfaces;

namespace OlymppMarketsAPI.Application.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(request.Product.Id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with Id {request.Product.Id} not found.");
            }

            var product = _mapper.Map<Product>(request.Product);
            await _productRepository.UpdateProductAsync(product);
            return Unit.Value;
        }

    }
}
