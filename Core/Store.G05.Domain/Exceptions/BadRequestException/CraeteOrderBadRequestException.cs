using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Domain.Exceptions.BadRequestException
{
    public class CraeteOrderBadRequestException() : BadRequestException("Invalid Operation When Create Order !")
    {
    }
}
