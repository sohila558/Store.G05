using Store.G05.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Services.Specifications.Orders
{
    public class OrderWithPaymentIntentSpecification : BaseSpecifications<Guid, Order>
    {
        public OrderWithPaymentIntentSpecification(string paymentIntentId) : base(O => O.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
