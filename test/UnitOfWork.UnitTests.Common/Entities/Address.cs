using UnitOfWork.Abstractions;

namespace UnitOfWork.UnitTests.Common.Entities
{
    public class Address : IEntity
    {
        public int Id { get; set; }
        public string? Street1 { get; set; }
        public string? Street2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public int PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
