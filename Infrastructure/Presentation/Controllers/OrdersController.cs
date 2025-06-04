using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.OrderModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class OrdersController(IServiceManager _serviceManager) : ApiBaseController
    {
        //Create Order
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
           var Order = await _serviceManager.OrderService.CreateOrderAsync(orderDto, GetEmailFromToken());
           return Ok(Order);
        }

    }
}
