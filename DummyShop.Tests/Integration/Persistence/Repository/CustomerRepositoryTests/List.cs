using DummyShop.Persistence;
using DummyShop.Persistence.Repository;
using DummyShop.Tests.Attributes;
using DummyShop.Tests.EntityFrameworkOverrides;
using System.Collections.Generic;

namespace DummyShop.Tests.Integration.Persistence.Repository.CustomerRepositoryTests;

public sealed class List : ShopContextSqLiteInMemory
{
    public List(ITestOutputHelper output) : base(output) { }

    [Theory, CustomerEntity]
    public async Task Returns_AllCustomers(IReadOnlyList<CustomerEntity> expected)
    {
        await using (var context = CreateContext())
        {
            context.Customers.AddRange(expected);
            await context.SaveChangesAsync();
        }

        await using (var context = CreateContext())
        {
            var sut = new CustomerRepository(context);
            var actual = await sut.List();

            Assert.Equivalent(expected, actual);

            #region Hidden so far

            // Assert.Collection(
            //     expected.OrderBy(entity => entity.ID),
            //     actual.AsSequenceSource().OfEqualityChecks(
            //         likeness => likeness
            //             .Without(entity => entity.Orders)));

            #endregion

            #region Hidden so far

            // Assert.Collection(
            //     expected.OrderBy(entity => entity.ID),
            //     actual.Select(item => item.AsSource().OfLikeness<Customer>()
            //             .With(customer => customer.ID)
            //             .EqualsWhen((entity, customer) => customer.ID.Numeric == entity.ID)
            //             .With(customer => customer.Email)
            //             .EqualsWhen((entity, customer) => customer.Email == entity.Email))
            //         .Select(likeness => (Action<Customer>) likeness.ShouldEqual)
            //         .ToArray());

            #endregion

            #region Hidden so far

            // Assert.True(
            //     expected
            //         .OrderBy(entity => entity.ID)
            //         .SequenceLike(actual, likeness => likeness
            //             .With(entity => entity.ID)
            //             .EqualsWhen((customer, entity) => customer.ID.Numeric == entity.ID)
            //             .With(entity => entity.Email)
            //             .EqualsWhen((customer, entity) => customer.Email == entity.Email)
            //             .Without(entity => entity.Orders)));

            #endregion
        }
    }
}