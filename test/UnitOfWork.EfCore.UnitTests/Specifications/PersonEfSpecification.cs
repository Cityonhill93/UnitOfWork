using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnitOfWork.Abstractions;
using UnitOfWork.UnitTests.Common.Entities;

namespace UnitOfWork.EfCore.UnitTests.Specifications
{
    internal class PersonEfSpecification : UnitOfWork.EfCore.IEfCoreSpecification<Person>
    {
        private readonly Expression<Func<Person, bool>> _expr;

        public PersonEfSpecification(Expression<Func<Person, bool>> expr)
        {
            _expr = expr;
        }

        public IEnumerable<Expression<Func<Person, IEntity>>> GetIncludes()
        {
            return Array.Empty<Expression<Func<Person, IEntity>>>();
        }

        public Expression<Func<Person, bool>> GetWhereExpression() => _expr;
    }
}
