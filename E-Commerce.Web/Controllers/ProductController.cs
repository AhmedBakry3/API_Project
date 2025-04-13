using E_Commerce.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    [Route("api/[controller]")] //BaseURL/api/Product
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET : BaseURL/api/Product/10
        [HttpGet("{id}")]
        public ActionResult<Product> GetAction(int id)
        {
            return new Product() { Id = id };
        }
        //BaseURL/api/Product
        [HttpGet]
        public ActionResult<Product> GetAll()
        {
            return new Product();
        }

        [HttpPost]
        public ActionResult<Product> AddProduct(Product product)
        {
            return new Product();
        }
        [HttpPut]
        public ActionResult<Product> UpdateProduct(Product product)
        {
            return new Product();
        }
        [HttpDelete]
        public ActionResult<Product> DeleteProduct(Product product)
        {
            return new Product();
        }
    }
}
