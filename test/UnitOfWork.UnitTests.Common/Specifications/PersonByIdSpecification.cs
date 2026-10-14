using System;
using System.Linq.Expressions;
using UnitOfWork.Abstractions;
using UnitOfWork.UnitTests.Common.Entities;

namespace UnitOfWork.UnitTests.Common.Specifications
{
    public class PersonByIdSpecification : ISpecification<Person>
    {
        private readonly int _id;

        public PersonByIdSpecification(int id)
        {
            _id = id;
        }

        public Expression<Func<Person, bool>> GetWhereExpression()
        {
            return p => p.Id == _id;
        }
    }
}
