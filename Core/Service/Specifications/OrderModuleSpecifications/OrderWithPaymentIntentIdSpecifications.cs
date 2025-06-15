using DomainLayer.Models.OrderModule;
using Service.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications.OrderModuleSpecifications
{
    internal class OrderWithPaymentIntentIdSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderWithPaymentIntentIdSpecifications(string paymentIntentId) : base(O => O.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
