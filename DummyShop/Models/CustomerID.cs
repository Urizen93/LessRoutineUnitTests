using DummyShop.Helpers;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using static LanguageExt.Prelude;

namespace DummyShop.Models;

public sealed class CustomerID : IEquatable<CustomerID>, IComparable<CustomerID>
{
    private const string TestCustomerID = "5555555551";

    public CustomerID(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Customer number cannot be empty", nameof(value));

        var noLeadingZeroes = value.TrimStart('0');

        if (noLeadingZeroes.Any(IsNotDigit))
            throw new ArgumentException("Must contain digits only!", nameof(value));

        if (noLeadingZeroes.Length != 10)
            throw new ArgumentException("Must contain strictly 10 characters!", nameof(value));

        if (not (IsLuhnCompliant(value)))
            throw new ArgumentException("Must pass LUHN check!");

        Value = noLeadingZeroes;
    }

    public string Value { get; }

    public long Numeric => long.Parse(Value);

    public bool Equals(CustomerID? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return Value == other.Value;
    }

    public override bool Equals(object? obj) =>
        obj is CustomerID other
        && Equals(other);

    public override string ToString() => Value;

    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string?(CustomerID? number) => number?.Value;

    public static bool operator ==(CustomerID? left, CustomerID? right) =>
        ReferenceEquals(left, right)
        || left is {} && left.Equals(right);

    public static bool operator !=(CustomerID? left, CustomerID? right) =>
        !(left == right);

    public int CompareTo(CustomerID? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return string.Compare(Value, other.Value, StringComparison.Ordinal);
    }

    [Pure]
    private static bool IsLuhnCompliant(string value) =>
        value == TestCustomerID || Luhn.IsCompliant(value);

    [Pure]
    private static bool IsNotDigit(char symbol) => !char.IsDigit(symbol);
}