using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class DeliveryNotFoundException(int Id) : NotFoundException($"Delivery Method with Id = {Id} Is Not Found ")
    {
    }
}
