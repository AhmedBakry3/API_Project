using AutoMapper;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Options;
using Shared.DataTransferObject.IdentityDtos;
using Shared.DataTransferObject.OrderModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class OrderProfiles : Profile
    {
        public OrderProfiles() 
        {
            CreateMap<AddressDto, OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                    .ForMember(Dest => Dest.DeliveryMethod, Options => Options.MapFrom(src => src.DeliveryMethod.ShortName));

            CreateMap<OrderItem,OrderItemDto>()
                    .ForMember(Dest=>Dest.ProductName, Options => Options.MapFrom(src => src.Product.ProductName))
                    .ForMember(Dest => Dest.PictureUrl, Options => Options.MapFrom<PictureUrlResolver>())
        }
    }
}
