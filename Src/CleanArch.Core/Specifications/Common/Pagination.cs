namespace CleanArch.Core.Specifications.Common;

/// <summary>
/// Represents a page of items resulting from a paginated query, along with pagination metadata.
/// </summary>
/// <typeparam name="T">The type of the items in the page.</typeparam>
public class Pagination<T>(IReadOnlyList<T> items,
    int currentPage,
    int totalCount,
    int pageSize)
{
    /// <summary>
    /// The items on the current page.
    /// </summary>
    public IReadOnlyList<T> Items { get; init; } = items;

    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// The current page number (1-based).
    /// </summary>
    public int CurrentPage { get; init; } = currentPage;

    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// The total number of pages, calculated from the total item count and page size.
    /// </summary>
    public int TotalPages { get; init; } = (int)Math.Ceiling(totalCount / (decimal)pageSize);

    /// <summary>
    /// Indicates whether a next page exists.
    /// </summary>
    public bool IsExistNextPage => (CurrentPage < TotalPages);

    /// <summary>
    /// Indicates whether a previous page exists.
    /// </summary>
    public bool IsExistPrevPage => (CurrentPage > 1);
}