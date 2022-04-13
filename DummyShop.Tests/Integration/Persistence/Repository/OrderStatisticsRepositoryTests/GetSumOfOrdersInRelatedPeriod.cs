using AutoFixture;
using AutoFixture.Xunit2;
using DummyShop.Models;
using DummyShop.Persistence;
using DummyShop.Persistence.Repository;
using DummyShop.Tests.Customizations;
using DummyShop.Tests.EntityFrameworkOverrides;
using DummyShop.Tests.Extensions;
using DummyShop.Tests.Misc.SpecimenBuilders;
using System.Linq;

namespace DummyShop.Tests.Integration.Persistence.Repository.OrderStatisticsRepositoryTests;

public sealed class GetSumOfOrdersInRelatedPeriod : ShopContextSqLiteInMemory
{
    public GetSumOfOrdersInRelatedPeriod(ITestOutputHelper output) : base(output) { }

    [Theory, CustomerWithOrders]
    public async Task Returns_SumOfAllTheOrders([Frozen] Email email, CustomerEntity customer)
    {
        var expected = (
            from order in customer.Orders
            from line in order.OrderLines
            select line.Quantity * line.Product!.Price
        ).Sum();
        
        await using (var context = CreateContext())
        {
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
        }

        await using (var context = CreateContext())
        {
            var sut = new OrderStatisticsRepository(context);
            var actual = await sut.GetSumOfOrdersInRelatedPeriod(
                email,
                DateTimeOffset.Now.AddYears(-2));
            Assert.Equal(expected, actual);
            WriteLine($"The sum was {actual}");
        }
    }
    
    private sealed class CustomerWithOrdersAttribute : AutoDataAttribute
    {
        public CustomerWithOrdersAttribute() : base(FixtureFactory) { }

        private static IFixture FixtureFactory()
        {
            var fixture = new Fixture()
                .WithCircularReference()
                .Customize(new ValidEmail());

            fixture.Customizations.Add(
                new RandomDateTimeOffsetSequenceGenerator(
                    DateTimeOffset.Now.AddYears(-1),
                    DateTimeOffset.Now));

            fixture.Customize<OrderEntity>(composer => composer
                .Do(entity => fixture.CreateMany<OrderLineEntity>().Iter(entity.OrderLines.Add)));
            
            fixture.Customize<CustomerEntity>(composer => composer
                .With(entity => entity.Email, (Email email) => email.Value)
                .Do(entity => fixture.CreateMany<OrderEntity>().Iter(entity.Orders.Add)));

            return fixture;
        }
    }
}