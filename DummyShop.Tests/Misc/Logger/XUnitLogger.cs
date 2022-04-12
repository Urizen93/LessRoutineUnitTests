using Microsoft.Extensions.Logging;

namespace DummyShop.Tests.Misc.Logger;

public sealed class XUnitLogger : ILogger
{
    private const string Name = nameof(XUnitLogger);
    private readonly IWriter _writer;

    public XUnitLogger(IWriter writer) => _writer = writer;

    public void Log<TState>(LogLevel logLevel, EventId _, TState state,
        Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        var message = formatter(state, exception);

        if (string.IsNullOrWhiteSpace(message) && exception is null) return;

        _writer.WriteLine($"{logLevel}: {Name}: {message}");

        if (exception != null) _writer.WriteLine(exception.ToString());
    }

    public bool IsEnabled(LogLevel _) => true;

    public IDisposable BeginScope<TState>(TState _) => new XUnitScope();
}