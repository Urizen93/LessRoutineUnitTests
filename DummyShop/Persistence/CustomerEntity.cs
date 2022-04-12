using System.Collections.Generic;

namespace DummyShop.Persistence;

public sealed class CustomerEntity
{
    public long ID { get; set; }
        
    public string? Email { get; set; }

    public ICollection<OrderEntity> Orders { get; } = new HashSet<OrderEntity>();
}