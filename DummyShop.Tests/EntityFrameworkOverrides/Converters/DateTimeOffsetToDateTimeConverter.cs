using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DummyShop.Tests.EntityFrameworkOverrides.Converters;

public sealed class DateTimeOffsetToDateTimeConverter : ValueConverter<DateTimeOffset, DateTime>
{
    public DateTimeOffsetToDateTimeConverter(ConverterMappingHints? mappingHints = null) : base(
        dateTimeOffset => dateTimeOffset.UtcDateTime,
        dateTime => new DateTimeOffset(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)),
        mappingHints)
    {
    }
}