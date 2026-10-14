using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using UnitOfWork.Abstractions;

namespace UnitOfWork.EfCore
{
    /// <summary>
    /// Defines a specification that can be used to read <see cref="IEntity"/> instances or identify them for update or deletion, with support for Entity Framework Core navigation property includes.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEfCoreSpecification<TEntity> : ISpecification<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Returns the expressions that are used to include navigation properties when querying entities that match the specification.
        /// </summary>
        /// <typeparam name="TNavProperty"></typeparam>
        /// <returns></returns>
        IEnumerable<Expression<Func<TEntity, TNavProperty>>> GetIncludes<TNavProperty>();
    }
}
