using Microsoft.EntityFrameworkCore;
using System;

namespace DummyShop.Persistence;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder builder)
    {
        var john = new CustomerEntity { ID = 8693276019L, Email = "john.smith@fastdev.se" };
        var jane = new CustomerEntity { ID = 3496343678L, Email = "jane.doe@fastdev.se" };

        var niceX = new ProductEntity {
            ID = new Guid("515EAFBB-5BAD-43B0-A6D4-277D49C31192"),
            Name = "Nice X",
            Price = 100 };
        var awesomeY = new ProductEntity {
            ID = new Guid("A4F35C60-3AF1-458B-B887-AE06A8482FB1"),
            Name = "Awesome Y",
            Price = 300 };

        var year2021 = new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var year2020 = new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero);

        var johnOrder = new OrderEntity
        {
            ID = new Guid("A1EA5AA0-10AB-49CE-8D54-0C15CB4F07D5"),
            CreatedAt = year2021,
            CustomerID = john.ID
        };
        var janeOrder = new OrderEntity
        {
            ID = new Guid("0494A9C2-EB6C-430C-BFC5-6F88DD084ADD"),
            CreatedAt = year2020,
            CustomerID = jane.ID
        };

        var orderLines = new[]
        {
            new OrderLineEntity
            {
                ID = new Guid("CF80A62F-04E8-46B4-A8F0-1076BB1C0518"),
                Quantity = 1,
                ProductID = niceX.ID,
                OrderID = johnOrder.ID
            },
            new OrderLineEntity
            {
                ID = new Guid("6AB72CDA-E366-41E8-B607-046E91362B5D"),
                Quantity = 3,
                ProductID = awesomeY.ID,
                OrderID = johnOrder.ID
            },
            new OrderLineEntity
            {
                ID = new Guid("48325234-9311-45DA-9747-D35049CFB7CB"),
                Quantity = 2,
                ProductID = niceX.ID,
                OrderID = janeOrder.ID
            }
        };

        builder.Entity<CustomerEntity>().HasData(john, jane);
        builder.Entity<ProductEntity>().HasData(niceX, awesomeY);
        builder.Entity<OrderEntity>().HasData(johnOrder, janeOrder);
        builder.Entity<OrderLineEntity>().HasData(orderLines);
    }
}