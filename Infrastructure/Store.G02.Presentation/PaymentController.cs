using Microsoft.AspNetCore.Mvc;
using Store.G05.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController(IServiceManager serviceManager) : ControllerBase
    {
        // Create Payment Intent
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreatePaymentIntent(string basketId)
        {
            var result = await serviceManager.PaymentServices.CreatePaymentIntentAsync(basketId);
            return Ok(result);
        }

    }
}
