using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Domain.Exceptions.NotFound
{
    public class BasketNotFoundException(string id) : NotFoundException($"Basket With Id {id} Not Found !!")
    {
    }
}
