using Microsoft.AspNetCore.Authorization;
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
    public class PaymentsController(IServiceManager _serviceManager) : ApiBaseController
    {
        [Authorize]
        [HttpPost("{BasketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            var Basket = await _serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(BasketId);
            return Ok(Basket);
        }
    }
}
