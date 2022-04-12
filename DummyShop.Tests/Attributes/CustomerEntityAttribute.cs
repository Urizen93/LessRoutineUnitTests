using AutoFixture;
using AutoFixture.Xunit2;
using DummyShop.Models;
using DummyShop.Persistence;
using DummyShop.Tests.Customizations;

namespace DummyShop.Tests.Attributes;

public sealed class CustomerEntityAttribute : AutoDataAttribute
{
    public CustomerEntityAttribute() : base(FixtureFactory) { }

    private static IFixture FixtureFactory()
    {
        var fixture = new Fixture()
            .Customize(new ValidCustomerID())
            .Customize(new ValidEmail());
                
        fixture.Customize<CustomerEntity>(composer => composer
            .With(entity => entity.ID, (CustomerID id) => id.Numeric)
            .With(entity => entity.Email, (Email email) => email.Value));
                
        return fixture;
    }
}