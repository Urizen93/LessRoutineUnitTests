using AutoFixture;
using DummyShop.Extensions;
using DummyShop.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace DummyShop.Tests.Misc;

public static class Generators
{
    #region Some examples

    public static int RelativelySmallNumber(this Generator<byte> generator) =>
        generator.First(number => number < 10);

    public static int NumberWhichIsNotIn(this Generator<int> generator, Seq<int> exclude) =>
        generator.First(number => not (exclude.Contains(number)));

    public static IEnumerable<char> NonZeroDigits(this Generator<char> generator) =>
        generator.Where(symbol => char.IsDigit(symbol) && symbol != '0');

    public static IEnumerable<char> Digits(this Generator<char> generator) =>
        generator.Where(char.IsDigit);
        
    public static IEnumerable<char> NonDigits(this Generator<char> generator) =>
        generator.Where(symbol => not (char.IsDigit(symbol)));

    public static IEnumerable<char> Letters(this Generator<char> generator) =>
        generator.Where(char.IsLetter);

    public static int NumberWithExactNumberOfDigits(this IFixture fixture, int numberOfDigits)
    {
        var generator = new Generator<char>(fixture);
            
        return int.Parse(
            string.Concat(generator
                .NonZeroDigits()
                .Take(1)
                .Concat(generator
                    .Digits()
                    .Take(numberOfDigits - 1))));
    }

    #endregion
    
    public static string LuhnCompliant(this IFixture fixture, int length = 10)
    {
        if (length < 2)
            throw new ArgumentOutOfRangeException(nameof(length), length, "Must be greater than one!");
            
        var generator = new Generator<char>(fixture);
        var nonZeroDigit = generator
            .NonZeroDigits()
            .Take(1);
        var remainingDigits = generator
            .Digits()
            .Take(length - 2);
        
        var generated = string.Concat(nonZeroDigit.Concat(remainingDigits));
        return MakeLuhnCompliant(generated);
    }

    private static string MakeLuhnCompliant(string incomplete)
    {
        var luhnNumber = Luhn.Number(incomplete + '0');
        var complementary = (10 - luhnNumber).LastDigit();

        return incomplete + complementary;
    }
}