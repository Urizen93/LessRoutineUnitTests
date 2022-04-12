using System;

namespace DummyShop.Models;

public sealed class Email : IEquatable<Email>
{
    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty", nameof(value));

        if (value.Length > 254)
            throw new ArgumentException("Email is too long", nameof(value));

        if (!value.Contains('@'))
            throw new ArgumentException("Invalid email", nameof(value));

        Value = value;
    }

    public string Value { get; }

    public override bool Equals(object? obj) =>
        obj is Email other
        && Equals(other);

    public bool Equals(Email? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return Value == other.Value;
    }

    public override string ToString() => Value;

    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string?(Email? email) => email?.Value;
}