using AutoFixture;
using AutoFixture.Xunit2;
using DummyShop.Persistence;
using DummyShop.Tests.EntityFrameworkOverrides;
using DummyShop.Tests.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DummyShop.Tests.Integration.Persistence.ShopContextTests;

public sealed class WhenChangesAreSaved : ShopContextSqLiteInMemory
{
    public WhenChangesAreSaved(ITestOutputHelper output) : base(output) { }

    [Theory, OrderWithoutCreatedAtProperty]
    public async Task OrderEntity_CreatedAt_GetsGeneratedValue(OrderEntity order)
    {
        await using (var context = CreateContext())
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        await using (var context = CreateContext())
        {
            var actual = await context.Orders.SingleAsync();
            
            Assert.NotEqual(default, actual.CreatedAt);
            WriteLine($"Order was created at {actual.CreatedAt}");
        } 
    }
    
    private sealed class OrderWithoutCreatedAtPropertyAttribute : AutoDataAttribute
    {
        public OrderWithoutCreatedAtPropertyAttribute() : base(FixtureFactory) { }

        private static IFixture FixtureFactory()
        {
            var fixture = new Fixture().WithCircularReference();
            
            fixture.Customize<OrderEntity>(composer => composer
                .With(entity => entity.CreatedAt, () => default));
            
            return fixture;
        }
    }
}