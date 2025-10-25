using System.Linq.Expressions;
using angularSkinet.Core.Interfaces;

namespace angularSkinet.Core.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification() {}

    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>>? Criteria { get; }

    public Expression<Func<T, object>>? OrderBy { get; private set; }

    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public bool IsDistinct { get;  private set; }

    public int Take { get;  private set; }
    public int Skip { get;  private set; }
    public bool IsPagingEnableb { get;  private set; }

    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
        if (Criteria is not null)
        {
            query = query.Where(Criteria);
        }

        return query;
    }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }

    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnableb = true;
    }
}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria)
    : BaseSpecification<T>(criteria!), ISpecification<T, TResult>
{
    protected BaseSpecification() : this(null) {}
    
    public Expression<Func<T, TResult>>? Select { get; private set; }

    protected void AddSelect(Expression<Func<T, TResult>> selectExpresion)
    {
        Select = selectExpresion;
    }
}