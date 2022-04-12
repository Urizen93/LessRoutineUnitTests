using AutoFixture;
using AutoFixture.Xunit2;
using DummyShop.Tests.Extensions;

namespace DummyShop.Tests.Attributes;

public sealed class AutoNSubDataAttribute : AutoDataAttribute
{
    public AutoNSubDataAttribute()
        : base(() => new Fixture().WithNSubstitute())
    {
    }
}