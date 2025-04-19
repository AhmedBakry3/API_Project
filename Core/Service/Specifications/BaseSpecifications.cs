using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification
{
    abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) =>  IncludeExpressions.Add(includeExpression);
        #endregion

        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) => OrderBy = orderByExpression;

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByExpression) => OrderByDescending = orderByExpression;


        #endregion

        #region Pagination
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPaginated { get; set; }

        //Total Count = 40
        // PageSize = 10
        // PageIndex = 3
        //10 , 10 , 10 , 10
        protected void ApplyPagination(int PageSize, int PageIndex)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageSize - 1) * PageIndex;
        }
        #endregion
    }
}
