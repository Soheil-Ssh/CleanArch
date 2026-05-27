using CleanArch.Core.IRepositories.ToDo;

namespace CleanArch.Core.IRepositories.Common;

/// <summary>
/// Defines a contract for a unit of work that coordinates the work of repositories
/// and persists all changes within a single transaction.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Repository for ToDo Entity
    /// </summary>
    public IToDoRepository ToDoRepository { get; }

    /// <summary>
    /// Asynchronously saves all pending changes made within this unit of work.
    /// </summary>
    Task<int> SaveAsync();
}