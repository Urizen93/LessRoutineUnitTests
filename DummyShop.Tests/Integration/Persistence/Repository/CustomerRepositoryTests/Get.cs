using AutoFixture.Xunit2;
using DummyShop.Models;
using DummyShop.Persistence;
using DummyShop.Persistence.Repository;
using DummyShop.Tests.Attributes;
using DummyShop.Tests.EntityFrameworkOverrides;
using SemanticComparison.Fluent;

namespace DummyShop.Tests.Integration.Persistence.Repository.CustomerRepositoryTests;

public sealed class Get : ShopContextSqLiteInMemory
{
    public Get(ITestOutputHelper output) : base(output) { }
    
    [Theory, CustomerEntity]
    public async Task Returns_Customer(
        [Frozen] Email user,
        CustomerEntity expected)
    {
        await Arrange(expected);

        await using var context = CreateContext();
        var sut = new CustomerRepository(context);
        var actual = await sut.Get(user);

        expected.AsSource().OfLikeness<CustomerEntity>()
            .Without(entity => entity.Orders)
            .ShouldEqual(actual!);
    }

    [Theory, CustomerEntity]
    public async Task Returns_Null_When_ThereIsNoSuchCustomer(Email user, CustomerEntity existing)
    {
        await Arrange(existing);

        await using var context = CreateContext();
        var sut = new CustomerRepository(context);
        var actual = await sut.Get(user);

        Assert.Null(actual);
    }

    private async Task Arrange(CustomerEntity customer)
    {
        await using var context = CreateContext();
        context.Customers.Add(customer);
        await context.SaveChangesAsync();
    }
}