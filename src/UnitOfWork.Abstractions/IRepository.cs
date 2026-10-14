using System;
using System.Collections.Generic;
using System.Text;

namespace UnitOfWork.Abstractions
{
    /// <summary>
    /// A class that allows CRUD operations on a specific <see cref="IEntity"/> type.
    /// </summary>
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Writes a new <see cref="IEntity"/> to the data store.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all <see cref="IEntity"/>records that match the given <see cref="ISpecification{TEntity}"/>.
        /// </summary>
        /// <param name="specification">The specification to match entities.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a single <see cref="IEntity"/> that matches the given <see cref="ISpecification{TEntity}"/> or null if no match is found.
        /// </summary>
        /// <param name="specification">The specification to match entities.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<TEntity?> GetFirstOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all <see cref="IEntity"/> records that match the given <see cref="ISpecification{TEntity}"/>.
        /// </summary>
        /// <param name="specification">The specification to match entities.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates all <see cref="IEntity"/> records by executing <paramref name="updateAction"/>to all records that match the given <see cref="ISpecification{TEntity}"/>.
        /// </summary>
        /// <param name="specification">The specification to match entities.</param>
        /// <param name="updateAction">The action to execute on each matching entity.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(ISpecification<TEntity> specification, Action<TEntity> updateAction, CancellationToken cancellationToken = default);

    }
}
