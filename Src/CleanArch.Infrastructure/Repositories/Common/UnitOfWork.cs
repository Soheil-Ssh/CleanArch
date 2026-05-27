using CleanArch.Core.IRepositories.Common;
using CleanArch.Core.IRepositories.ToDo;
using CleanArch.Infrastructure.Repositories.ToDo;

namespace CleanArch.Infrastructure.Repositories.Common;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private IToDoRepository? _toDoRepository;
    public IToDoRepository ToDoRepository
        => _toDoRepository ??= new ToDoRepository(context);

    public async Task<int> SaveAsync()
        => await context.SaveChangesAsync();
}