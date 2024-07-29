using AutoMapper;
using MediatR;
using OlymppMarketsAPI.Domain.Entities;
using OlymppMarketsAPI.Domain.Interfaces;
using OlymppMarketsAPI.DTOs;

namespace OlymppMarketsAPI.Application.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Product> products = (List<Domain.Entities.Product>)await _productRepository.GetAllProductsAsync();
            return _mapper.Map<List<ProductDTO>>(products);
        }
    }
}
