using DomainLayer.Models.ProductModule;
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
            && (string.IsNullOrEmpty(queryParams.search) || (p.Name.ToLower().Contains(queryParams.search.ToLower()))))
        { }
    }
}
