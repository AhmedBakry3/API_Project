using Shared.DataTransferObject;
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
        Task<IEnumerable<ProductDto>> GetAllProducts();

        //Get Product By Id
        Task<ProductDto> GetProductById(int id);

        //Get All Brands
        Task<IEnumerable<BrandDto>> GetAllBrands();

        //Get All Types
        Task<IEnumerable<TypeDto>> GetAllTypes();
    }
}
