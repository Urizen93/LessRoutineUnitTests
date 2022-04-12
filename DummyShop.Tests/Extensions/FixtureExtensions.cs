using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Kernel;

namespace DummyShop.Tests.Extensions;

public static class FixtureExtensions
{
    public static IFixture WithNSubstitute(this IFixture fixture) =>
        fixture.Customize(new AutoNSubstituteCustomization());
        
    public static IFixture WithCircularReference(this IFixture fixture)
    {
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        return fixture;
    }

    public static IFixture With(this IFixture fixture, ISpecimenBuilder builder)
    {
        fixture.Customizations.Add(builder);
        return fixture;
    }
}