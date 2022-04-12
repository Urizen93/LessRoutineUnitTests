using AutoFixture;
using DummyShop.Models;
using System.Net.Mail;

namespace DummyShop.Tests.Customizations;

public sealed class ValidEmail : ICustomization
{
    public void Customize(IFixture fixture) =>
        fixture.Register((MailAddress email) => new Email(email.Address));
}