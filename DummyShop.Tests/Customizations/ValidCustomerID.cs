using AutoFixture;
using DummyShop.Models;
using DummyShop.Tests.Misc;

namespace DummyShop.Tests.Customizations;

public sealed class ValidCustomerID : ICustomization
{
    public void Customize(IFixture fixture) =>
        fixture.Register(() => new CustomerID(fixture.LuhnCompliant()));
}