using System.Linq.Expressions;
using CleanArch.Core.Entities.Common.Enums;
using CleanArch.Infrastructure.Data;
using CleanArch.Infrastructure.IRepositories.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infrastructure.Repositories.Common
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Ctor

        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _db;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _db = _context.Set<TEntity>();
        }

        #endregion


        #region Get all

        public async Task<Result<IEnumerable<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null,
            string includeProps = null)
        {
            Result<IEnumerable<TEntity>> result = new Result<IEnumerable<TEntity>>();

            try
            {
                IQueryable<TEntity> query = _db;

                if (expression != null)
                {
                    query = query.Where(expression);
                }

                if (order != null)
                {
                    query = order(query);
                }

                if (!string.IsNullOrEmpty(includeProps))
                {
                    foreach (string includeProp in includeProps.Split(','))
                    {
                        query = query.Include(includeProp);
                    }
                }

                result.Data = await query.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return result;
        }

        #endregion

        #region Get all (with selector)

        public async Task<Result<IEnumerable<TResult>>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null,
            string includeProps = null)
        {
            Result<IEnumerable<TResult>> result = new Result<IEnumerable<TResult>>();

            try
            {
                IQueryable<TEntity> query = _db;

                if (expression != null)
                {
                    query = query.Where(expression);
                }

                if (order != null)
                {
                    query = order(query);
                }

                if (!string.IsNullOrEmpty(includeProps))
                {
                    foreach (string includeProp in includeProps.Split(','))
                    {
                        query = query.Include(includeProp);
                    }
                }

                result.Data = await query.AsNoTracking().Select(selector).ToListAsync();
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return result;
        }

        #endregion
        
        #region Get all (with take)

        public async Task<Result<IEnumerable<TEntity>>> GetAllAsync(int take,
            Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null,
            string includeProps = null)
        {
            Result<IEnumerable<TEntity>> result = new Result<IEnumerable<TEntity>>();

            try
            {
                IQueryable<TEntity> query = _db;

                if (expression != null)
                {
                    query = query.Where(expression);
                }

                if (order != null)
                {
                    query = order(query);
                }

                if (!string.IsNullOrEmpty(includeProps))
                {
                    foreach (string includeProp in includeProps.Split(','))
                    {
                        query = query.Include(includeProp);
                    }
                }

                result.Data = await query.AsNoTracking().Take(take).ToListAsync();
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return result;
        }

        #endregion

        #region Get all (with take, with selector)

        public async Task<Result<IEnumerable<TResult>>> GetAllAsync<TResult>(int take,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null,
            string includeProps = null)
        {
            Result<IEnumerable<TResult>> result = new Result<IEnumerable<TResult>>();

            try
            {
                IQueryable<TEntity> query = _db;

                if (expression != null)
                {
                    query = query.Where(expression);
                }

                if (order != null)
                {
                    query = order(query);
                }

                if (!string.IsNullOrEmpty(includeProps))
                {
                    foreach (string includeProp in includeProps.Split(','))
                    {
                        query = query.Include(includeProp);
                    }
                }

                result.Data = await query.AsNoTracking().Select(selector).Take(take).ToListAsync();
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return result;
        }

        #endregion
        
        #region Get

        public async Task<Result<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression,
            string includeProps = null)
        {
            Result<TEntity> result = new Result<TEntity>();

            try
            {
                IQueryable<TEntity> query = _db;

                if (!string.IsNullOrEmpty(includeProps))
                {
                    foreach (string includeProp in includeProps.Split(','))
                    {
                        query = query.Include(includeProp);
                    }
                }

                result.Data = await query.AsNoTracking().FirstOrDefaultAsync(expression);
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return await Task.FromResult(result);
        }

        #endregion

        #region Get (with selector)

        public async Task<Result<TResult>> GetAsync<TResult>(Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TResult>> selector,
            string includeProps = null)
        {
            Result<TResult> result = new Result<TResult>();

            try
            {
                IQueryable<TEntity> query = _db;

                if (!string.IsNullOrEmpty(includeProps))
                {
                    foreach (string includeProp in includeProps.Split(','))
                    {
                        query = query.Include(includeProp);
                    }
                }

                result.Data = await query.AsNoTracking().Where(expression)
                    .Select(selector)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return await Task.FromResult(result);
        }

        #endregion

        #region Count

        public async Task<Result<int>> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            Result<int> result = new Result<int>();

            try
            {
                IQueryable<TEntity> query = _db;

                if (expression != null)
                {
                    query = query.Where(expression);
                }

                result.Data = await query.AsNoTracking().CountAsync();
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return await Task.FromResult(result);
        }

        #endregion

        #region Is exist

        public async Task<Result<bool>> IsExistAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            Result<bool> result = new Result<bool>();

            try
            {
                IQueryable<TEntity> query = _db;

                if (expression != null)
                {
                    query = query.Where(expression);
                }

                result.Data = await query.AsNoTracking().AnyAsync();
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return await Task.FromResult(result);
        }

        #endregion

        #region Add

        public async Task<Result<Guid>> AddAsync(TEntity entity)
        {
            Result<Guid> result = new Result<Guid>();

            try
            {
                entity.CreateDate = DateTime.Now;

                await _db.AddAsync(entity);

                result.Data = entity.Id;
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return await Task.FromResult(result);
        }

        #endregion

        #region Add range

        public async Task<Result<IEnumerable<Guid>>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            Result<IEnumerable<Guid>> result = new Result<IEnumerable<Guid>>();

            try
            {
                await _db.AddRangeAsync(entities);
                result.Data = entities.Select(e => e.Id);
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return await Task.FromResult(result);
        }

        #endregion

        #region Update

        public Result<Guid> Update(TEntity entity)
        {
            Result<Guid> result = new Result<Guid>();

            try
            {
                entity.LastUpdateDate = DateTime.Now;
                _db.Update(entity);

                result.Data = entity.Id;
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return result;
        }

        #endregion

        #region Delete

        public Result Delete(TEntity entity)
        {
            Result result = new Result();

            try
            {
                entity.IsDelete = true;
                Update(entity);
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return result;
        }

        #endregion

        #region Delete persistent

        public Result DeletePersistent(TEntity entity)
        {
            Result result = new Result();

            try
            {
                _db.Remove(entity);
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return result;
        }

        #endregion

        #region Delete (with id)

        public async Task<Result> DeleteAsync(Guid id)
        {
            Result result = new Result();

            try
            {
                var entity = await GetAsync(e => e.Id == id);
                if (entity.Data == null)
                {
                    result.SetType(ResultType.NotFoundError, $"Data not found by id = {id}");
                    return await Task.FromResult(result);
                }

                Delete(entity.Data);
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return await Task.FromResult(result);
        }

        #endregion

        #region Delete persistent (with id)

        public async Task<Result> DeletePersistentAsync(Guid id)
        {
            Result result = new Result();

            try
            {
                var entity = await GetAsync(e => e.Id == id);
                if (entity.Data == null)
                {
                    result.SetType(ResultType.NotFoundError, $"Data not found by id = {id}");
                    return await Task.FromResult(result);
                }

                DeletePersistent(entity.Data);
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return await Task.FromResult(result);
        }

        #endregion

        #region Delete range

        public Result DeleteRange(IEnumerable<TEntity> entities)
        {
            Result result = new Result();

            try
            {
                foreach (var entity in entities)
                {
                    Delete(entity);
                }
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return result;
        }

        #endregion

        #region Delete persistent range

        public Result DeletePersistentRange(IEnumerable<TEntity> entities)
        {
            Result result = new Result();

            try
            {
                _db.RemoveRange(entities);
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return result;
        }

        #endregion

        #region Delete range (with id)

        public async Task<Result> DeleteRangeAsync(IEnumerable<Guid> ids)
        {
            Result result = new Result();

            try
            {
                var entities = new List<TEntity>();
                foreach (var id in ids)
                {
                    var entity = await GetAsync(e => e.Id == id);
                    if (entity.Data == null)
                    {
                        result.SetType(ResultType.NotFoundError, $"Data not found by id = {id}");
                        return await Task.FromResult(result);
                    }

                    Delete(entity.Data);
                }
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return await Task.FromResult(result);
        }

        #endregion

        #region Delete persistent range (with id)

        public async Task<Result> DeletePersistentRangeAsync(IEnumerable<Guid> ids)
        {
            Result result = new Result();

            try
            {
                var entities = new List<TEntity>();
                foreach (var id in ids)
                {
                    var entity = await GetAsync(e => e.Id == id);
                    if (entity.Data == null)
                    {
                        result.SetType(ResultType.NotFoundError, $"Data not found by id = {id}");
                        return await Task.FromResult(result);
                    }
                }

                DeletePersistentRange(entities);
            }
            catch (Exception ex)
            {
                result.SetType(ResultType.InternalServerError, ex.GetType().ToString() + " - " + ex.Message);
            }

            return await Task.FromResult(result);
        }

        #endregion

    }
}
