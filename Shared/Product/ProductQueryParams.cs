using Shared.Product.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Product
{
    public class ProductQueryParams
    {
        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;
        public int? BrandId { get; set; }
        public int? TypeId  { get; set; }
        public ProductSortingOptions sort { get; set; } 
        public string? search { get; set; }
        public int pageNumber { get; set; } = 1;

        private int pageSize = DefaultPageSize;

        public int PageSize
        {
            get => pageSize;

            set => pageSize = value > MaxPageSize ? MaxPageSize : value;
        }   


    }
}
