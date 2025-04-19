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
        public int? BrandId { get; set; }
        public int? TypeId  { get; set; }
        public ProductSortingOptions sortingOptions { get; set; } 
    }
}
