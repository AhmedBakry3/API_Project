using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        //Property Signature for Every Dynamic Part in Query
        public Expression<Func<TEntity, bool>> Criteria { get; }
        public List<Expression<Func<TEntity,object>>> IncludeExpressions { get; }
    }
}
