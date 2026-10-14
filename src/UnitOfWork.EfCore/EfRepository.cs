using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Text;
using UnitOfWork.Abstractions;

namespace UnitOfWork.EfCore
{
    public abstract class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly Func<CancellationToken, Task> _saveChangesAsync;

        internal EfRepository(DbSet<TEntity> dbSet, Func<CancellationToken, Task> saveChangesAsync)
        {
            _dbSet = dbSet;
            _saveChangesAsync = saveChangesAsync;
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _saveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            _ = await _dbSet
                .Where(specification.GetWhereExpression())
                .ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = _dbSet;

            if(specification is IEfCoreSpecification<TEntity> efCoreSpecification)
            {
                foreach (var include in efCoreSpecification.GetIncludes())
                {
                    queryable = queryable.Include(include);
                }
            }

            return await queryable
                .Where(specification.GetWhereExpression())
                .ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = _dbSet;

            if (specification is IEfCoreSpecification<TEntity> efCoreSpecification)
            {
                foreach (var include in efCoreSpecification.GetIncludes())
                {
                    queryable = queryable.Include(include);
                }
            }

            return await queryable
                .Where(specification.GetWhereExpression())
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateAsync(ISpecification<TEntity> specification, Action<TEntity> updateAction, CancellationToken cancellationToken = default)
        {
            var recordsToUpdate = await _dbSet.Where(specification.GetWhereExpression()).ToListAsync(cancellationToken);
            foreach(var record in recordsToUpdate)
            {
                updateAction(record);
            }

            await _saveChangesAsync(cancellationToken);
        }
    }
}
