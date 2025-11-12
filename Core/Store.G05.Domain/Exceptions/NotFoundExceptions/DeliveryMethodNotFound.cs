using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Domain.Exceptions.NotFoundExceptions
{
    public class DeliveryMethodNotFound(int id) : NotFoundException($"Delivery Method with id {id} Not Found !")
    {
    }
}
