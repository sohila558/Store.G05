using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Domain.Exceptions.BadRequest
{
    public abstract class BadRequestException(string message) : Exception(message)
    {
    }
}
