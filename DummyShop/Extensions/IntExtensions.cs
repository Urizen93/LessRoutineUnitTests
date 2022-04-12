namespace DummyShop.Extensions;

public static class IntExtensions
{
    public static int LastDigit(this int value) =>
        value % 10;
}