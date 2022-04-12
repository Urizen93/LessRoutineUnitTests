using AutoFixture;
using AutoFixture.Xunit2;
using DummyShop.Tests.Customizations;
using System.Reflection;

namespace DummyShop.Tests.Attributes;

public sealed class ValidEmailAttribute : CustomizeAttribute
{
    public override ICustomization GetCustomization(ParameterInfo parameter) => new ValidEmail();
}