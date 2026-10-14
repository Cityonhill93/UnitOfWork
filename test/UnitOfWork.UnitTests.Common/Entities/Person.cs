using System;
using UnitOfWork.Abstractions;

namespace UnitOfWork.UnitTests.Common.Entities
{
    public class Person : IEntity
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Email { get; set; }
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
