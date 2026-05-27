using CleanArch.Core.Specifications.ToDo;
using System.Linq.Expressions;

namespace CleanArch.Core.IRepositories.ToDo;

/// <summary>
/// Defines repository operations specific to the <see cref="ToDo"/> entity, extending the base CRUD and query capabilities.
/// </summary>
public interface IToDoRepository : IBaseRepository<long, Entities.ToDo.ToDo>
{
    /// <summary>
    /// Asynchronously retrieves a paginated set of <see cref="ToDo"/> items filtered by the provided specification
    /// and projected into the specified result type.
    /// </summary>
    /// <typeparam name="TResult">The type of the projected result.</typeparam>
    /// <param name="filter">The specification containing filtering, ordering, and pagination parameters.</param>
    /// <param name="selector">The projection expression that defines the shape of each result item.</param>
    /// <returns>A <see cref="Pagination{T}"/> object containing the requested page of projected items and pagination metadata.</returns>
    Task<Pagination<TResult>> GetAll<TResult>(ToDoFilterSpecification filter,
        Expression<Func<Entities.ToDo.ToDo, TResult>> selector);
}