using Microsoft.AspNetCore.Mvc;
using Store.G02.Shared.Dtos.Baskets;
using Store.G05.Services.Abstractions;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        // stripe listen --forward-to http://localhost:7271/api/payments/webhook
        [Route("webhook")]
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            
            var signatureHeader = Request.Headers["Stripe-Signature"];

            await serviceManager.PaymentServices.UpdateOrderPaymentStatusAsync(json, signatureHeader);

            return new EmptyResult();
        }

    }
}
