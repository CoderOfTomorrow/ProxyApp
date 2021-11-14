using System;

namespace Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}
