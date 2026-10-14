using System;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.UnitTests.Common.Entities;

namespace UnitOfWork.EfCore
{
    internal class PersonRepository : EfRepository<Person>
    {
        internal PersonRepository(DbSet<Person> dbSet, Func<CancellationToken, Task> saveChangesAsync)
            : base(dbSet, saveChangesAsync)
        {
        }
    }
}
