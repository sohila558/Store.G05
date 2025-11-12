using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.G02.Shared.Dtos.Orders;
using Store.G05.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderRequest request)
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.OrderServices.CreateOrderAsync(request, userEmailClaim.Value);
            return Ok(result);
        }

        [HttpGet("deliveryMethod")]
        public async Task<IActionResult> GetAllDeliveryMethod()
        {
            var result = await _serviceManager.OrderServices.GetAllDeliveryMethodAsync();
            return Ok(result);
        }

        [HttpGet("OrderById")]
        [Authorize]
        public async Task<IActionResult> GetOrderByIdForSpecificUser(Guid id)
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.OrderServices.GetOrderByIdForSpecificUserAsync(id, userEmailClaim.Value);
            return Ok(result);
        }

        [HttpGet]
        [Authorize("Orders")]
        public async Task<IActionResult> GetOrdersForSpecificUser()
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.OrderServices.GetOrdersForSpecificUserAsync(userEmailClaim.Value);
            return Ok(result);
        }
    }
}
