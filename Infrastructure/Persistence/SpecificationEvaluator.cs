﻿using DomainLayer.Contracts;
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
            if(specifications.Criteria is not  null)
            {
                Query = Query.Where(specifications.Criteria);
            }
            if(specifications.IncludeExpressions is not null &&specifications.IncludeExpressions.Count > 0)
            {
                specifications.IncludeExpressions.Aggregate(Query, (current, includeExpression) => current.Include(includeExpression));
            }
            return Query;
        }
    }
}
