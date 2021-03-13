using LionStore.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace LionStore.Data.Repositories
{
    public class Specification<TSource> : ISpecification<TSource>
    {
        public Expression<Func<TSource, bool>> BaseFilter { get; }
        public Expression<Func<TSource, bool>> Filter { get; private set; }
        public List<Expression<Func<TSource, object>>> Includes { get; private set; }
        public Expression<Func<TSource, object>> OrderBy { get; private set; }
        public Expression<Func<TSource, object>> OrderByDescending { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }

        public Specification(Expression<Func<TSource, bool>> baseFilter)
        {
            BaseFilter = baseFilter;
        }
        public Specification<TSource> AddInclude(Expression<Func<TSource, object>> expression)
        {
            if (Includes == null) Includes = new List<Expression<Func<TSource, object>>>();
            Includes.Add(expression);
            return this;
        }
        public Specification<TSource> ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            return this;
        }
        public Specification<TSource> ApplyOrderBy(Expression<Func<TSource, object>> expression)
        {
            OrderBy = expression;
            return this;
        }
        public Specification<TSource> ApplyOrderByDescending(Expression<Func<TSource, object>> expression)
        {
            OrderByDescending = expression;
            return this;
        }
        public Specification<TSource> ApplyFilter(Expression<Func<TSource, bool>> expression)
        {
            Filter = expression;
            return this;
        }
    }
}
