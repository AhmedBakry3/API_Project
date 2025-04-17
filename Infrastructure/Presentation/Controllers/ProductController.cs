﻿using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //BaseURL/api/Product
    public class ProductController(IServiceManager _serviceManager) : ControllerBase
    {
        //Get All Products
        //BaseURl/api/Product
        [HttpGet]
        public async Task<ActionResult<ProductDto>> GetAllProducts()
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync();
            return Ok(products);
        }

        //Get Product By Id
        //BaseURL/api/Product/10
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }
        //Get All Types
        //BaseURL/api/Product/Types
        [HttpGet("Types")]
        public async Task<ActionResult<TypeDto>> GetTypes()
        {
            var Types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }

        //Get All Brands
        //BaseURL/api/Product/Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<TypeDto>> GetBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }
    }
}
