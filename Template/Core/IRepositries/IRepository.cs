using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.IRepositries
{
    public interface IRepository<TEntity> where TEntity : class
    {

        IEnumerable<TEntity> GetAll
        (
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes
        );

        IEnumerable<TEntity> Get
        (
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes
        );

        TEntity GetOne
        (
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes
        );

        TEntity GetFirst
        (
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes
        );

        TEntity GetByID(object ID);

        int? GetCount(Expression<Func<TEntity, bool>> filter = null);

        decimal? GetSum(Expression<Func<TEntity, decimal?>> sumProperty, Expression<Func<TEntity, bool>> filter = null);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
