using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Domain.Exceptions
{
    public class BasketCreateOrUpdateBadRequestException() : BadRequestException("Invalid Oepration When Create Or Update Basket !")
    {
    }
}
