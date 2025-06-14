using DomainLayer.Models.ProductModule;
using Service.Specification;
using Shared.Product;
using Shared.Product.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product , int>
    {
        //Get All Products with Brands And Types
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams) : 
            base(p=>(!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId) &&
            (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId) 
            && (string.IsNullOrEmpty(queryParams.search) ||(p.Name.ToLower().Contains(queryParams.search.ToLower()))))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (queryParams.sort) 
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }
            ApplyPagination(queryParams.PageSize, queryParams.pageNumber);
        }
        //Get Product By Id
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
