using DomainLayer.Models.OrderModule;
using Service.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications.OrderModuleSpecifications
{
    internal class OrderSpecifications : BaseSpecifications<Order, Guid>
    {
        //Get All Orders By Email
        public OrderSpecifications(string Email) : base(O => O.buyerEmail == Email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderByDescending(O => O.OrderDate);
        }

        //Get Order By Id
        public OrderSpecifications(Guid Id) : base(O => O.Id == Id)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
        }
    }
}
