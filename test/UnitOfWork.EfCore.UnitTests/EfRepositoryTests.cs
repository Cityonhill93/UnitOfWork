using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using UnitOfWork.Abstractions;
using UnitOfWork.UnitTests.Common.Entities;
using UnitOfWork.UnitTests.Common.Specifications;
using UnitOfWork.EfCore.UnitTests.Specifications;
using Xunit;

namespace UnitOfWork.EfCore.UnitTests
{
    public class EfRepositoryTests
    {
        // Use shared Person entity from the test common project and the concrete PersonRepository in the EF Core project.

        // Use PersonTestDbContext defined in PersonTestDbContext.cs

        [Fact]
        public async Task AddAsync_Calls_DbSet_AddAsync_And_Saves()
        {
            var entity = new Person
            {
                Id = 1,
                FirstName = "one",
                LastName = "two",
                Email = "a@b.com",
                DateOfBirth = new System.DateOnly(1990, 1, 1)
            };

            var options = new DbContextOptionsBuilder<PersonTestDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            await using var context = new PersonTestDbContext(options);

            var repo = new UnitOfWork.EfCore.PersonRepository(context.Set<Person>(), ct => context.SaveChangesAsync(ct));

            await repo.AddAsync(entity);

            var saved = await context.People.SingleOrDefaultAsync(p => p.Id == 1);
            Assert.NotNull(saved);
            Assert.Equal("one", saved!.FirstName);
        }

        [Fact]
        public async Task GetAsync_Returns_Filtered_Items()
        {
            var data = new List<Person>
            {
                new Person { Id = 1, FirstName = "one", LastName = "a", Email = "1@x.com", DateOfBirth = new System.DateOnly(1990, 1, 1) },
                new Person { Id = 2, FirstName = "two", LastName = "b", Email = "2@x.com", DateOfBirth = new System.DateOnly(1991, 1, 1) }
            };

            var options = new DbContextOptionsBuilder<PersonTestDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            await using var context = new PersonTestDbContext(options);
            await context.People.AddRangeAsync(data);
            await context.SaveChangesAsync();

            var spec = new PersonByIdSpecification(2);

            var repo = new UnitOfWork.EfCore.PersonRepository(context.Set<Person>(), ct => context.SaveChangesAsync(ct));

            var result = (await repo.GetAsync(spec)).ToList();

            Assert.Single(result);
            Assert.Equal(2, result[0].Id);
        }

        [Fact]
        public async Task GetFirstOrDefaultAsync_Returns_First_Matching_Item()
        {
            var data = new List<Person>
            {
                new Person { Id = 1, FirstName = "one", LastName = "a", Email = "1@x.com", DateOfBirth = new System.DateOnly(1990, 1, 1) },
                new Person { Id = 2, FirstName = "two", LastName = "b", Email = "2@x.com", DateOfBirth = new System.DateOnly(1991, 1, 1) }
            };

            var options = new DbContextOptionsBuilder<PersonTestDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            await using var context = new PersonTestDbContext(options);
            await context.People.AddRangeAsync(data);
            await context.SaveChangesAsync();

            var spec = new PersonByIdSpecification(1);

            var repo = new UnitOfWork.EfCore.PersonRepository(context.Set<Person>(), ct => context.SaveChangesAsync(ct));

            var result = await repo.GetFirstOrDefaultAsync(spec);

            Assert.NotNull(result);
            Assert.Equal(1, result!.Id);
        }

        [Fact]
        public async Task GetFirstOrDefaultAsync_Returns_Null_When_No_Match()
        {
            var data = new List<Person>
            {
                new Person { Id = 1, FirstName = "one", LastName = "a", Email = "1@x.com", DateOfBirth = new System.DateOnly(1990, 1, 1) },
                new Person { Id = 2, FirstName = "two", LastName = "b", Email = "2@x.com", DateOfBirth = new System.DateOnly(1991, 1, 1) }
            };

            var options = new DbContextOptionsBuilder<PersonTestDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            await using var context = new PersonTestDbContext(options);
            await context.People.AddRangeAsync(data);
            await context.SaveChangesAsync();

            var spec = new PersonByIdSpecification(999);

            var repo = new UnitOfWork.EfCore.PersonRepository(context.Set<Person>(), ct => context.SaveChangesAsync(ct));

            var result = await repo.GetFirstOrDefaultAsync(spec);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAsync_Returns_Empty_When_No_Match()
        {
            var data = new List<Person>
            {
                new Person { Id = 1, FirstName = "one", LastName = "a", Email = "1@x.com", DateOfBirth = new System.DateOnly(1990, 1, 1) },
                new Person { Id = 2, FirstName = "two", LastName = "b", Email = "2@x.com", DateOfBirth = new System.DateOnly(1991, 1, 1) }
            };

            var options = new DbContextOptionsBuilder<PersonTestDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            await using var context = new PersonTestDbContext(options);
            await context.People.AddRangeAsync(data);
            await context.SaveChangesAsync();

            var spec = new PersonByIdSpecification(999);
            var efSpec = new PersonEfSpecification(spec.GetWhereExpression());

            var repo = new UnitOfWork.EfCore.PersonRepository(context.Set<Person>(), ct => context.SaveChangesAsync(ct));

            var result = await repo.GetAsync(efSpec);

            Assert.Empty(result);
        }

        [Fact]
        public async Task UpdateAsync_Applies_Action_To_Matching_Records_And_Saves()
        {
            var data = new List<Person>
            {
                new Person { Id = 1, FirstName = "one", LastName = "a", Email = "1@x.com", DateOfBirth = new System.DateOnly(1990, 1, 1) },
                new Person { Id = 2, FirstName = "two", LastName = "b", Email = "2@x.com", DateOfBirth = new System.DateOnly(1991, 1, 1) }
            };

            var options = new DbContextOptionsBuilder<PersonTestDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            await using var context = new PersonTestDbContext(options);
            await context.People.AddRangeAsync(data);
            await context.SaveChangesAsync();

            var spec = new PersonByIdSpecification(1);
            var repo = new UnitOfWork.EfCore.PersonRepository(context.Set<Person>(), ct => context.SaveChangesAsync(ct));

            await repo.UpdateAsync(spec, e => { e.FirstName = "updated"; e.LastName = "updated"; });

            var updated = await context.People.FindAsync(new object[] { 1 }, CancellationToken.None);
            Assert.NotNull(updated);
            Assert.Equal("updated", updated!.FirstName);
        }
    }
}
