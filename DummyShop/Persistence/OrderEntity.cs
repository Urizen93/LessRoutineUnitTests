using System;
using System.Collections.Generic;

namespace DummyShop.Persistence;

public sealed class OrderEntity
{
    public Guid ID { get; set; }
        
    public DateTimeOffset CreatedAt { get; set; }
        
    public long CustomerID { get; set; }
        
    public CustomerEntity? Customer { get; set; }

    public ICollection<OrderLineEntity> OrderLines { get; } = new HashSet<OrderLineEntity>();
}