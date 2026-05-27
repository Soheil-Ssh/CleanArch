using CleanArch.Core.IRepositories.ToDo;
using CleanArch.Core.Specifications.Common;
using CleanArch.Core.Specifications.ToDo;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArch.Infrastructure.Repositories.ToDo;

public class ToDoRepository(ApplicationDbContext context)
    : BaseRepository<long, Core.Entities.ToDo.ToDo>(context), IToDoRepository
{
    public async Task<Pagination<TResult>> GetAll<TResult>(ToDoFilterSpecification filter,
        Expression<Func<Core.Entities.ToDo.ToDo, TResult>> selector)
    {
        IQueryable<Core.Entities.ToDo.ToDo> query = Db;

        if (filter.Criteria != null)
            query = query.Where(filter.Criteria);

        int count = await query.CountAsync();
        if (count == 0)
            return new Pagination<TResult>(new List<TResult>(), filter.CurrentPage, 0, filter.Take);

        if (filter.OrderBy != null)
            query = filter.OrderBy(query);

        var items = await query.AsNoTracking()
            .Skip(filter.Skip)
            .Take(filter.Take)
            .Select(selector)
            .ToListAsync();

        return new Pagination<TResult>(items, filter.CurrentPage, count, filter.Take);
    }
}