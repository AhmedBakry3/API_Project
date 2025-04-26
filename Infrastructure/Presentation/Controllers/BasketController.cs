using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.BasketModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //BaseUrl/api/basket
    public class BasketController(IServiceManager _serviceManager) : ControllerBase
    {
        //Get Basket
        [HttpGet] //GET : BaseUrl/api/basket
        public async Task<IActionResult> GetBasket(string Key)
        {
            var Backet = await _serviceManager.BasketService.GetBasketAsync(Key);
            return Ok(Backet);

        }
        //Create Or Update Basket
        [HttpPost] //POST : BaseUrl/api/basket
        public async Task<IActionResult> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }

        //Delete Basket 
        [HttpDelete("{Key}")] //Delete : BaseUrl/api/basket/acsfhasgs
        public async Task<IActionResult> DeleteBasket(string Key)
        {
            var Result = await _serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(Result);
        }
    }
}
