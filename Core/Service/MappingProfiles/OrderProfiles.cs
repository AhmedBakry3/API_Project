using AutoMapper;
using DomainLayer.Models.OrderModule;
using Shared.DataTransferObject.IdentityDtos;
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
            CreateMap<AddressDto, OrderAddress>();
        }
    }
}
