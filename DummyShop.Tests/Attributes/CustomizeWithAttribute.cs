using AutoFixture;
using AutoFixture.Xunit2;
using System.Reflection;

namespace DummyShop.Tests.Attributes;

public sealed class CustomizeWithAttribute : CustomizeAttribute
{
    private readonly Type _type;

    public CustomizeWithAttribute(Type customizationType)
    {
        if (!typeof(ICustomization).IsAssignableFrom(customizationType))
            throw new ArgumentException( "Type needs to implement ICustomization");
            
        _type = customizationType;
    }

    public override ICustomization GetCustomization(ParameterInfo parameter) =>
        Activator.CreateInstance(_type) as ICustomization
        ?? throw new InvalidOperationException();
}