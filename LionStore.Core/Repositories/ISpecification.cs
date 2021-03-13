using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace LionStore.Core.Repositories
{
    public interface ISpecification<TSource>
    {
        Expression<Func<TSource, bool>> BaseFilter { get; }
        Expression<Func<TSource, bool>> Filter { get; }
        List<Expression<Func<TSource, object>>> Includes { get; }
        Expression<Func<TSource, object>> OrderBy { get; }
        Expression<Func<TSource, object>> OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
    }
}
