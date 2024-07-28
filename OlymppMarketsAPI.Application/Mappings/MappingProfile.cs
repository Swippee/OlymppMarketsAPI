using AutoMapper;
using OlymppMarketsAPI.Domain.Entities;
using OlymppMarketsAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OlymppMarketsAPI.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock.Quantity));

            CreateMap<ProductDTO, Product>()
              .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Price { Amount = src.Price }))
              .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => new Stock { Quantity = src.Stock }));
        }
    }
}
