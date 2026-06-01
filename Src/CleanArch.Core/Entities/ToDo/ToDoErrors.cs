using CleanArch.Core.Abstractions.Error;

namespace CleanArch.Core.Entities.ToDo;

public static class ToDoErrors
{
    /// <summary>
    /// Not found to do by id error
    /// </summary>
    public static readonly Error NotFound = new("ToDo.NotFound", "To Do not found by id.", ErrorType.NotFound);
}