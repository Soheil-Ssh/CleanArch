using System.Linq.Expressions;

namespace CleanArch.Infrastructure.IRepositories.Common
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<Result<IEnumerable<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null,
            string includeProps = null);

        Task<Result<IEnumerable<TResult>>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null,
            string includeProps = null);

        Task<Result<IEnumerable<TEntity>>> GetAllAsync(int take,
            Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null,
            string includeProps = null);

        Task<Result<IEnumerable<TResult>>> GetAllAsync<TResult>(int take,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null,
            string includeProps = null);

        Task<Result<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression, string includeProps = null);

        Task<Result<TResult>> GetAsync<TResult>(Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TResult>> selector,
            string includeProps = null);

        Task<Result<int>> CountAsync(Expression<Func<TEntity, bool>> expression = null);

        Task<Result<bool>> IsExistAsync(Expression<Func<TEntity, bool>> expression = null);

        Task<Result<Guid>> AddAsync(TEntity entity);

        Task<Result<IEnumerable<Guid>>> AddRangeAsync(IEnumerable<TEntity> entities);

        Result<Guid> Update(TEntity entity);

        Result Delete(TEntity entity);

        Result DeletePersistent(TEntity entity);

        Task<Result> DeleteAsync(Guid id);

        Task<Result> DeletePersistentAsync(Guid id);

        Result DeleteRange(IEnumerable<TEntity> entities);

        Result DeletePersistentRange(IEnumerable<TEntity> entities);

        Task<Result> DeleteRangeAsync(IEnumerable<Guid> ids);

        Task<Result> DeletePersistentRangeAsync(IEnumerable<Guid> ids);
    }
}
