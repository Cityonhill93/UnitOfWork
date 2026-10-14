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
        /// Deletes an existing <see cref="IEntity"/> from the data store.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing <see cref="IEntity"/> in the data store.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);


    }
}
