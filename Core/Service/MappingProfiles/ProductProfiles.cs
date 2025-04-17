using AutoMapper;
using DomainLayer.Models;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class ProductProfiles : Profile
    {
        public ProductProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.BrandName, Options => Options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.TypeName, Options => Options.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, Options => Options.MapFrom<PictureUrlResolver>());
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType,TypeDto>();
        }
    }
}
