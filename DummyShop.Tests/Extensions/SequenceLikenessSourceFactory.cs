using SemanticComparison;
using SemanticComparison.Fluent;
using System.Collections.Generic;
using System.Linq;

namespace DummyShop.Tests.Extensions;

public static class SequenceLikenessSourceFactory
{
    public static SequenceLikenessSource<T> AsSequenceSource<T>(this IEnumerable<T> value) => new(value);
    
    public sealed class SequenceLikenessSource<T>
    {
        private readonly IEnumerable<T> _value;
    
        public SequenceLikenessSource(IEnumerable<T> value) => _value = value;

        public Action<T>[] OfEqualityChecks() =>
            OfEqualityChecks(identity);

        public Action<T>[] OfEqualityChecks(
            Func<Likeness<T, T>, Likeness<T, T>> customize) =>
            OfEqualityChecks<T>(customize);

        public Action<TDestination>[] OfEqualityChecks<TDestination>() =>
            OfEqualityChecks<TDestination>(identity);
    
        public Action<TDestination>[] OfEqualityChecks<TDestination>(
            Func<Likeness<T, TDestination>, Likeness<T, TDestination>> customize) => _value
            .Select(item => item.AsSource().OfLikeness<TDestination>())
            .Select(customize)
            .Select(likeness => (Action<TDestination>) likeness.ShouldEqual)
            .ToArray();
    }
}