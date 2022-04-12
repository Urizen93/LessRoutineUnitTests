using System;
using System.Collections.Generic;

namespace DummyShop.Persistence;

public sealed class ProductEntity
{
    public Guid ID { get; set; }
        
    public string? Name { get; set; }
        
    public decimal Price { get; set; }

    public ICollection<OrderLineEntity> OrderLines { get; } = new HashSet<OrderLineEntity>();
}