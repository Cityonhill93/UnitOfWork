using Microsoft.EntityFrameworkCore;
using UnitOfWork.UnitTests.Common.Entities;

namespace UnitOfWork.EfCore.UnitTests
{
    internal class PersonTestDbContext : DbContext
    {
        public PersonTestDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Person> People { get; set; } = null!;
    }
}
