using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    static class SpecificationEvaluator
    {
        //Create Query
        //_dbcontext.product.where(p => p.id == id).Include(p => p.productbrands).Include(p=>p.prodType)
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> InputQuery, ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var Query = InputQuery;
            //Query = _dbcontext.Products

            if (specifications.Criteria is not  null)
            {
                Query = Query.Where(specifications.Criteria);
            }
            //Query = _dbcontext.Products.where(P=>P.id==id.value)


            //Query = _dbcontext.Products
            //IncludeExpression = (p => p.ProductBrand)
            //Query = _dbcontext.Products.Include(p => p.ProductBrand)
            //IncludExpression =(p => p.ProductType)

            if (specifications.IncludeExpressions is not null &&specifications.IncludeExpressions.Count > 0)
            {
                Query =  specifications.IncludeExpressions.Aggregate(Query, (current, includeExpression) => current.Include(includeExpression));
            }
            //Query = _dbcontext.Products.where(P=>P.id==id.value).Include(p => p.ProductBrand).Include(p => p.ProductType);

            return Query;
        }
    }
}
