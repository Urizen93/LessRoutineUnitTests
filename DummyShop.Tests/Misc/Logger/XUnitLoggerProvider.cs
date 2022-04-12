using Microsoft.Extensions.Logging;

namespace DummyShop.Tests.Misc.Logger;

public sealed class XUnitLoggerProvider : ILoggerProvider
{
    private readonly IWriter _writer;

    public XUnitLoggerProvider(IWriter writer) => _writer = writer;

    public ILogger CreateLogger(string _) => new XUnitLogger(_writer);

    public void Dispose() { }
}