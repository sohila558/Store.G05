using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Domain.Exceptions.NotFound
{
    public class DeliveryMethodNotFound(int id) : NotFoundException($"Delivery Method With Id {id} Was Not Found !")
    {
    }
}
