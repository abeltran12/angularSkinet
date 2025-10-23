using angularSkinet.Core.Entities;
using angularSkinet.Core.Interfaces;

namespace angularSkinet.Infrastructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        if (spec.Criteria is not null)
        {
            query = query.Where(spec.Criteria); // este seria el where
        }

        if (spec.OrderBy is not null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDescending is not null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        if (spec.IsDistinct)
        {
            query = query.Distinct();
        }

        return query;
    }

    public static IQueryable<TResult> GetQuery<TSpec, TResult>
        (IQueryable<T> query, ISpecification<T, TResult> spec)
    {
        if (spec.Criteria is not null)
        {
            query = query.Where(spec.Criteria); // este seria el where
        }

        if (spec.OrderBy is not null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDescending is not null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        var selectQuery = query as IQueryable<TResult>;

        if (spec.Select is not null)
        {
            selectQuery = query.Select(spec.Select);
        }

        if (spec.IsDistinct)
        {
            selectQuery = selectQuery?.Distinct();
        }

        return selectQuery ?? query.Cast<TResult>();
    }
}
