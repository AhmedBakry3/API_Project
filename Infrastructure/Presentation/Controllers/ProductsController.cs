using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObject.ProductModuleDtos;
using Shared.Product;
using Shared.Product.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class ProductsController(IServiceManager _serviceManager) : ApiBaseController
    {
        //Get All Products
        //GET : BaseURl/api/Products
        [HttpGet]
        [Cache(300)]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }

        //Get Product By Id
        //GET: BaseURL/api/Products/10
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }
        //Get All Types
        //GET: BaseURL/api/Products/Types
        [HttpGet("Types")]
        public async Task<ActionResult<TypeDto>> GetTypes()
        {
            var Types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }

        //Get All Brands
        //GET: BaseURL/api/Products/Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<TypeDto>> GetBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }
    }
}
