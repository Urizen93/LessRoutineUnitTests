using System;
using System.Diagnostics.Contracts;

namespace DummyShop.Extensions;

public static class CharExtensions
{
    [Pure]
    public static int AsDigit(this char value) =>
        char.IsDigit(value)
            ? value - '0'
            : throw new ArgumentException("Provided char is not a digit!");
}