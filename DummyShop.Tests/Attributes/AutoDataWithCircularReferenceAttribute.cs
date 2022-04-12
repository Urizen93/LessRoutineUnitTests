using AutoFixture;
using AutoFixture.Xunit2;
using DummyShop.Tests.Extensions;

namespace DummyShop.Tests.Attributes;

public sealed class AutoDataWithCircularReferenceAttribute : AutoDataAttribute
{
    public AutoDataWithCircularReferenceAttribute()
        : base(() => new Fixture().WithCircularReference()) { }
}