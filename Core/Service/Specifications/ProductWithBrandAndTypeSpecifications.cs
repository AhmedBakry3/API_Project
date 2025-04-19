using DomainLayer.Models;
using Service.Specification;
using Shared.Enums;
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
        public ProductWithBrandAndTypeSpecifications(int? BrandId, int? TypeId , ProductSortingOptions sortingOptions) : 
            base(p=>(!BrandId.HasValue || p.BrandId == BrandId) &&
            (!TypeId.HasValue || p.TypeId == TypeId))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (sortingOptions) 
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderBy(p => p.Price);
                    break;
                default:
                    break;

            }
        }
        //Get Product By Id
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
