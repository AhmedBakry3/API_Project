﻿using DomainLayer.Models;
using Service.Specification;
using Shared.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class ProductCountSpecifications : BaseSpecifications<Product , int>
    {
        public ProductCountSpecifications(ProductQueryParams queryParams) :
            base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId) &&
            (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
            && (string.IsNullOrEmpty(queryParams.SearchValue) || (p.Name.ToLower().Contains(queryParams.SearchValue.ToLower()))))
        { }
    }
}
