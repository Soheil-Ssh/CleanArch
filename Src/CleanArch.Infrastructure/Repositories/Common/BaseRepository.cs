using CleanArch.Core.Entities.Base;
using CleanArch.Core.IRepositories.Common;
using CleanArch.Core.Specifications.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArch.Infrastructure.Repositories.Common;

public class BaseRepository<TKey, TEntity> : IBaseRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
{
    protected readonly ApplicationDbContext Context;
    protected readonly DbSet<TEntity> Db;

    protected BaseRepository(ApplicationDbContext context)
    {
        Context = context;
        Db = context.Set<TEntity>();
    }

    protected virtual IQueryable<TEntity> BuildQuery(Expression<Func<TEntity, bool>>? expression = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = null,
        bool asNoTracking = true,
        bool asSplitQuery = false,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = Db;

        if (expression is not null)
            query = query.Where(expression);

        if (includes.Length > 0)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (order is not null)
            query = order(query);

        if (asNoTracking)
            query = query.AsNoTracking();

        if (asSplitQuery && includes.Length > 0)
            query = query.AsSplitQuery();

        return query;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = BuildQuery(expression, order, true, false, includes);
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? expression = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = BuildQuery(expression, order, true, false, includes);
        return await query.Select(selector).ToListAsync();
    }

    public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(int take,
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? expression = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        take = take <= 0 ? 10 : take;
        var query = BuildQuery(expression, order, true, false, includes);
        return await query.Select(selector).Take(take).ToListAsync();
    }

    public async Task<Pagination<TResult>> GetAllAsync<TResult>(int current,
        int take,
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? expression = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = BuildQuery(expression, order, true, false, includes);

        take = take <= 0 ? 10 : take;
        current = current <= 0 ? 1 : current;

        int count = await query.CountAsync();
        if (count == 0)
            return new Pagination<TResult>([], current, 0, take);

        int skip = (current - 1) * take;

        if (skip >= count)
        {
            current = (int)Math.Ceiling(count / (double)take);
            skip = (current - 1) * take;
        }

        var items = await query.Select(selector).Skip(skip).Take(take).ToListAsync();
        return new Pagination<TResult>(items, current, count, take);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = BuildQuery(expression, null, true, false, includes);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<TResult?> GetAsync<TResult>(Expression<Func<TEntity, bool>> expression,
        Expression<Func<TEntity, TResult>> selector,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = BuildQuery(expression, null, true, false, includes);
        return await query.Select(selector).FirstOrDefaultAsync();
    }

    public async Task<TKey> AddAsync(TEntity entity)
    {
        entity.CreateDate = DateTime.UtcNow;
        await Db.AddAsync(entity);
        return entity.Id;
    }

    public async Task<IEnumerable<TKey>> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        var entityList = entities.ToList();
        var now = DateTime.UtcNow;
        foreach (var entity in entityList)
        {
            entity.CreateDate = now;
        }
        await Db.AddRangeAsync(entityList);
        return entityList.Select(e => e.Id);
    }

    public TKey Update(TEntity entity)
    {
        entity.UpdateDate = DateTime.UtcNow;
        Db.Update(entity);
        return entity.Id;
    }

    public void Delete(TEntity entity)
    {
        entity.IsDeleted = true;
        Update(entity);
    }

    public void DeletePermanently(TEntity entity)
    {
        Db.Remove(entity);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? expression = null)
    {
        var query = BuildQuery(expression);
        return await query.CountAsync();
    }

    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>>? expression = null)
    {
        var query = BuildQuery(expression);
        return await query.AnyAsync();
    }
}