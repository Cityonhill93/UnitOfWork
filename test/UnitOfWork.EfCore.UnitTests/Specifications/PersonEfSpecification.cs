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
        private readonly bool _includeAddresses;

        public PersonEfSpecification(Expression<Func<Person, bool>> expr, bool includeAddresses = false)
        {
            _expr = expr;
            _includeAddresses = includeAddresses;
        }

        public IEnumerable<Expression<Func<Person, object>>> GetIncludes()
        {
            if (_includeAddresses)
            {
                return [
                    p => p.Addresses
                ];
            }

            return System.Array.Empty<Expression<Func<Person, object>>>();
        }

        public Expression<Func<Person, bool>> GetWhereExpression() => _expr;
    }
}
