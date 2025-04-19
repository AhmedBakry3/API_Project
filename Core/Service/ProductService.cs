using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObject;
using Shared.Product;
using Shared.Product.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork , IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandsDto =  _mapper.Map<IEnumerable<ProductBrand> , IEnumerable<BrandDto>>(Brands);
            return BrandsDto;
        }

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications(queryParams);
            var Products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(Specifications);
            var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            var ProductCount = Products.Count();
            return new PaginatedResult<ProductDto>(queryParams.PageSize , ProductCount , 0 , Data);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypesDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
            return TypesDto;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications(id);
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(Specifications);
            return _mapper.Map<Product, ProductDto>(Product);
        }
    }
}
