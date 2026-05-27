using System.Linq.Expressions;

namespace CleanArch.Core.Specifications.ToDo;

/// <summary>
/// Defines a specification for filtering and paginating <see cref="ToDo"/> entities
/// by title search, completion status, and creation date.
/// </summary>
public class ToDoFilterSpecification : BaseSpecification<Entities.ToDo.ToDo>
{
    /// <summary>
    /// Initializes a new instance with the given filter, pagination, and ordering parameters.
    /// </summary>
    /// <param name="searchQuery">Optional text to search for in the title. If null or empty, all titles match.</param>
    /// <param name="isCompleted">Optional completion status to filter by. If null, tasks of any completion state are included.</param>
    /// <param name="page">The 1-based page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    public ToDoFilterSpecification(string? searchQuery,
        bool? isCompleted,
        int page,
        int pageSize)
    {
        ApplyCriteria(BuildCriteria(searchQuery, isCompleted));
        ApplyPaging(page, pageSize);
        ApplyOrderBy(q => q.OrderByDescending(t => t.CreateDate));
    }

    /// <summary>
    /// Builds the filter expression that matches tasks based on the provided criteria.
    /// </summary>
    /// <param name="searchQuery">The text that must appear in the title (if specified).</param>
    /// <param name="isCompleted">If set, only tasks with the exact completion state are matched.</param>
    /// <returns>A combined filter expression.</returns>
    private static Expression<Func<Entities.ToDo.ToDo, bool>> BuildCriteria(string? searchQuery,
        bool? isCompleted)
    {
        return t =>
            (string.IsNullOrEmpty(searchQuery) || t.Title.Contains(searchQuery)) &&
            (!isCompleted.HasValue || t.IsCompleted == isCompleted);
    }
}