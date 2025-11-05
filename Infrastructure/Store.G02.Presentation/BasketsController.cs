using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.G02.Shared.Dtos.Baskets;
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
    public class BasketsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id)
        {
            var result = await serviceManager.BasketServices.GetBasketAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SetBasketAsync(BasketDto basketDto)
        {
            var result = await serviceManager.BasketServices.SetBasketAsync(basketDto);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasketAsync(string id)
        {
            await serviceManager.BasketServices.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
