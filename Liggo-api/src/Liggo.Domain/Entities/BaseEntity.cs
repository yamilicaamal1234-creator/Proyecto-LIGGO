using System;

namespace Liggo.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public Guid AdminId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
