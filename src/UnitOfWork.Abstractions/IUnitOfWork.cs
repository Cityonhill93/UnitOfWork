using System;
using System.Collections.Generic;
using System.Text;

namespace UnitOfWork.Abstractions
{
    /// <summary>
    /// Represents a unit of work that manages the repositories for different entity types.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the repository for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        Task<IRepository<TEntity>> GetRepositoryAsync<TEntity>() where TEntity : IEntity;
    }
}
