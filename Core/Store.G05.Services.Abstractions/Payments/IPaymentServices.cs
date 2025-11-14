using Store.G02.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Services.Abstractions.Payments
{
    public interface IPaymentServices
    {
        Task<BasketDto> CreatePaymentIntentAsync(string basketId);
    }
}
