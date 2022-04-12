using AutoFixture;
using AutoFixture.Kernel;
using System.Reflection;

namespace DummyShop.Tests.Misc.SpecimenBuilders;

public sealed class RandomDateTimeOffsetSequenceGenerator : ISpecimenBuilder
{
    private readonly RandomNumericSequenceGenerator _randomizer;

    public RandomDateTimeOffsetSequenceGenerator() : this(
        DateTimeOffset.Now.AddYears(-2),
        DateTimeOffset.Now.AddYears(2))
    {
    }
        
    public RandomDateTimeOffsetSequenceGenerator(DateTimeOffset minDate, DateTimeOffset maxDate)
    {
        if (minDate >= maxDate)
            throw new ArgumentException(
                $"{nameof(minDate)} must be lesser than {nameof(maxDate)}");

        _randomizer = new RandomNumericSequenceGenerator(minDate.Ticks, maxDate.Ticks);
    }

    public object Create(object request, ISpecimenContext context) =>
        IsDateTimeOffsetRequest(request)
            ? CreateRandomDate(context)
            : new NoSpecimen();

    private static bool IsDateTimeOffsetRequest(object request) =>
        typeof(DateTimeOffset).GetTypeInfo().IsAssignableFrom(request as Type);

    private object CreateRandomDate(ISpecimenContext context) =>
        new DateTimeOffset(GetRandomNumberOfTicks(context), TimeSpan.Zero);

    private long GetRandomNumberOfTicks(ISpecimenContext context) =>
        (long) _randomizer.Create(typeof(long), context);
}