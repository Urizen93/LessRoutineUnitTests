using AutoFixture;
using AutoFixture.Xunit2;
using DummyShop.Models;
using DummyShop.Persistence;
using DummyShop.Tests.Misc.Logger;
using System.Collections.Generic;
using System.Globalization;
using Array = System.Array;

namespace DummyShop.Tests.Unit;

#pragma warning disable xUnit1026

public sealed class AutoFixtureDemo : LoggingTest
{
    public AutoFixtureDemo(ITestOutputHelper output) : base(output) { }
    
    [Theory, AutoData]
    public void Basic(string text, IEnumerable<int> numbers, DateTimeOffset dateTime) { }

    [Fact]
    public void Fixture()
    {
        var fixture = new Fixture();

        WriteLine(fixture.Create<DateTime>().ToString(CultureInfo.InvariantCulture));
        WriteLine(fixture.CreateMany<byte>().ToArr().ToString());
    }

    #region Constrained constructors

    [Theory, AutoData]
    public void ConstrainedEmail(Email email) { }

    [Theory, AutoData]
    public void ConstrainedID(CustomerID id) { }

    #endregion
    
    #region Classes

    [Theory, AutoData]
    public void Entity(CustomerEntity expected) { }
    
    [Theory, AutoData]
    public void Record(DumbCustomer expected) { }

    #endregion

    #region MemberData

    public static TheoryData<string, IEnumerable<int>, DateTimeOffset> SomeData = new()
    {
        {"edge case 1", new [] {1, 2, 3}, default},
        {"edge case 2", Array.Empty<int>(), default}
    };

    public static TheoryData<string, IEnumerable<int>> SomeIncompleteData = new()
    {
        { "edge case : partially applied", new[] { 5, 6 } }
    };

    #endregion
}

#pragma warning restore xUnit1026