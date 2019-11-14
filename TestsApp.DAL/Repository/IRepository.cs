using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestApp.DAL.Repository
{
    public interface IRepository<TEntity, TId> where TEntity : class
	{		
        Task<TEntity> CreateAsync(TEntity entity);
		
        Task<IEnumerable<TEntity>> CreateManyAsync(IEnumerable<TEntity> entity);
		
        Task RemoveManyAsync(IEnumerable<TEntity> entity);
		
        Task<TEntity> UpdateAsync(TEntity entity);
		
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
		
        Task DeleteAsync(TId id);
	
        Task DeleteAsync(TEntity id);
		
        Task<TEntity> GetByIdAsync(TId id);
		
        IQueryable<TEntity> GetList(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = null,
			int? skip = default,
			int? take = default,
			bool asNoTracking = true,
			bool notDeleted = true);

		IQueryable<TEntity> GetQuery(
			Expression<Func<TEntity, bool>> filter = null,
			string includeProperties = null);

		IQueryable<TEntity> GetQueryable(
			out int count,
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = null,
			int? skip = default,
			int? take = default,
			bool asNoTracking = true,
			bool notDeleted = true,
			bool calculateCount = false);
	}
}
