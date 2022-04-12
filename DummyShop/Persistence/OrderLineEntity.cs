using System;

namespace DummyShop.Persistence;

public sealed class OrderLineEntity
{
    public Guid ID { get; set; }
        
    public int Quantity { get; set; }
        
    public Guid ProductID { get; set; }
        
    public ProductEntity? Product { get; set; }
        
    public Guid OrderID { get; set; }
        
    public OrderEntity? Order { get; set; }
}