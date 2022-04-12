using DummyShop.Extensions;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DummyShop.Helpers;

public static class Luhn
{
    [Pure]
    public static bool IsCompliant(string input) =>
        Number(input) == 0;

    [Pure]
    public static int Number(string input) => input
        .Reverse()
        .Map((index, value) => value.AsDigit() * IndexBasedMultiplier(index))
        .Map(SumOfDigits)
        .Sum()
        .LastDigit();

    [Pure]
    private static int SumOfDigits(int value) =>
        value.LastDigit() + value / 10;

    [Pure]
    private static int IndexBasedMultiplier(int index) =>
        index % 2 + 1;
}