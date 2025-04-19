using Shared;
using Shared.DataTransferObject;
using Shared.Product;
using Shared.Product.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        //Get All Products
        Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);

        //Get Product By Id
        Task<ProductDto> GetProductByIdAsync(int id);

        //Get All Brands
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();

        //Get All Types
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
    }
}
