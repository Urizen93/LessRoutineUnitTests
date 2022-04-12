using SemanticComparison;
using SemanticComparison.Fluent;
using System.Collections.Generic;
using System.Linq;

namespace DummyShop.Tests.Extensions;

public static class LikenessSequenceExtensions
{
    public static bool SequenceLike<T, TSource>(this IEnumerable<T> that, IEnumerable<TSource> source) =>
        SequenceLike(that, source, identity);

    public static bool SequenceLike<T, TSource>(
        this IEnumerable<T> that,
        IEnumerable<TSource> source,
        Func<Likeness<TSource, T>, IEquatable<T>> customizeLikeness) =>
        source.Select(
                item => customizeLikeness(item.AsSource().OfLikeness<T>()))
            .SequenceEqual(that.Cast<object>());
}