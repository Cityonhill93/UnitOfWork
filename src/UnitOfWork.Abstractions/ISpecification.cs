using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace UnitOfWork.Abstractions
{
    /// <summary>
    /// Defines a specification that can be used to read <see cref="IEntity"/> instances or identify them for update or deletion.
    /// </summary>
    public interface ISpecification<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Returns the expression that is used to filter the entities that match the specification.
        /// </summary>
        /// <returns></returns>
        Expression<Func<TEntity, bool>> ToExpression();
    }
}
