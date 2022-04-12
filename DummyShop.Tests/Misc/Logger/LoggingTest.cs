using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DummyShop.Tests.Misc.Logger;

public abstract class LoggingTest : IWriter, IDisposable
{
    protected LoggingTest(ITestOutputHelper output)
    {
        Output = output;

        LoggerFactory = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider()
            .GetRequiredService<ILoggerFactory>();

        LoggerFactory.AddProvider(new XUnitLoggerProvider(this));
    }

    private ITestOutputHelper Output { get; }

    protected ILoggerFactory LoggerFactory { get; }
    
    public void WriteLine(string? str = null) => Output.WriteLine(str ?? string.Empty);

    public virtual void Dispose() => LoggerFactory.Dispose();
}