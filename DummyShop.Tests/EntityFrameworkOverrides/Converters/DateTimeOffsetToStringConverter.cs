using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DummyShop.Tests.EntityFrameworkOverrides.Converters;

public sealed class DateTimeOffsetToStringConverter : ValueConverter<DateTimeOffset, string>
{
    private const string Format = "o";
    
    public DateTimeOffsetToStringConverter(ConverterMappingHints? mappingHints = null) : base(
        dateTimeOffset => dateTimeOffset.ToString(Format),
        dateTimeOffsetString => DateTimeOffset.ParseExact(dateTimeOffsetString, Format, null),
        mappingHints)
    {
    }
}