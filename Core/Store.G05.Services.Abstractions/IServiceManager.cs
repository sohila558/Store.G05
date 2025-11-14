using Store.G05.Services.Abstractions.Auth;
using Store.G05.Services.Abstractions.Baskets;
using Store.G05.Services.Abstractions.Orders;
using Store.G05.Services.Abstractions.Payments;
using Store.G05.Services.Abstractions.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Services.Abstractions
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
        IBasketServices BasketServices { get; }
        ICacheServices CacheServices { get; }
        IAuthServices AuthServices { get; }
        IOrderServices OrderServices { get; }
        IPaymentServices PaymentServices { get; }
    }
}
