using System.Linq.Expressions;

namespace CleanArch.Core.Specifications.Common;

/// <summary>
/// Provides a base class for building query specifications that encapsulate filtering, ordering, and pagination logic.
/// </summary>
/// <typeparam name="T">The entity type this specification applies to.</typeparam>
public abstract class BaseSpecification<T>
{
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// The filter expression used to restrict the query results.
    /// </summary>
    public Expression<Func<T, bool>>? Criteria { get; protected set; }
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// A function that applies ordering to the query and returns an ordered queryable.
    /// </summary>
    public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; protected set; }
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// The number of elements to skip for pagination.
    /// </summary>
    public int Skip { get; protected set; }
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// The number of elements to take for pagination.
    /// </summary>
    public int Take { get; protected set; }
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// The current page number (1-based) after pagination has been applied.
    /// </summary>
    public int CurrentPage { get; protected set; }

    /// <summary>
    /// Sets the pagination parameters based on the requested page number and page size.
    /// Ensures page is at least 1 and page size is at least 10.
    /// </summary>
    /// <param name="page">The requested page number (1-based). Values less than 1 are treated as 1.</param>
    /// <param name="pageSize">The number of items per page. Values less than or equal to 0 are treated as 10.</param>
    protected virtual void ApplyPaging(int page, int pageSize)
    {
        CurrentPage = page <= 0 ? 1 : page;
        Skip = (page - 1) * pageSize;
        Take = pageSize <= 0 ? 10 : pageSize;
    }

    /// <summary>
    /// Sets the ordering function that will be applied to the query.
    /// </summary>
    /// <param name="orderBy">A function that takes an <see cref="IQueryable{T}"/> and returns an <see cref="IOrderedQueryable{T}"/>.</param>
    protected void ApplyOrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
    {
        OrderBy = orderBy;
    }

    /// <summary>
    /// Sets the filter expression that will be applied to the query.
    /// </summary>
    /// <param name="criteria">The expression defining the filter condition.</param>
    protected void ApplyCriteria(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }
}