using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestApp.DAL.Repository
{
    public class EfRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        private readonly ApplicationContext _dbContext;

        public EfRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<TEntity>> CreateManyAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
            await _dbContext.SaveChangesAsync();

            return entities;
        }

        public async Task RemoveManyAsync(IEnumerable<TEntity> entity)
        {
            _dbContext.Set<TEntity>().RemoveRange(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity data)
        {
            _dbContext.Set<TEntity>().Remove(data);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TId id)
        {
            _dbContext.Set<TEntity>().Remove(await GetByIdAsync(id));

            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _dbContext.FindAsync<TEntity>(id);
        }

        public virtual IQueryable<TEntity> GetList(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = default(int?),
            int? take = default(int?),
            bool asNoTracking = true,
            bool notDeleted = true)
        {
            int count;
            var query = GetQueryable(out count, filter, orderBy, includeProperties, skip, take, asNoTracking, notDeleted, true);

            return query;
        }

        public IQueryable<TEntity> GetQueryable(
            out int count,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = default(int?),
            int? take = default(int?),
            bool asNoTracking = true,
            bool notDeleted = true,
            bool calculateCount = false)
        {
            var query = CreateQuery(filter, includeProperties);

            //if (notDeleted)
            //	query = query.Where(x => !x.Deleted);

            if (calculateCount)
                count = query.Count();
            else
                count = 0;

            if (orderBy != null)
                query = orderBy(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (asNoTracking)
                query = query.AsNoTracking();

            return query;
        }

        public virtual IQueryable<TEntity> GetQuery(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null)
        {
            return CreateQuery(filter, includeProperties);
        }

        protected IQueryable<TEntity> CreateQuery(
            Expression<Func<TEntity, bool>> filter = null,
            string Includes = null)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if (!String.IsNullOrEmpty(Includes))
                foreach (var property in Includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(property);

            if (filter != null)
                query = query.Where(filter);

            return query;
        }
    }
}
