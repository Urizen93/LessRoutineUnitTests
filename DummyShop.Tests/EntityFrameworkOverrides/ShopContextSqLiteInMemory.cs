using DummyShop.Persistence;
using DummyShop.Tests.Misc.Logger;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DummyShop.Tests.EntityFrameworkOverrides;

public abstract class ShopContextSqLiteInMemory : LoggingTest, IAsyncLifetime
{
    private readonly DbContextOptions<ShopContext> _contextOptions;

    protected ShopContextSqLiteInMemory(ITestOutputHelper output) : base(output)
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        connection.CreateFunction(@"SYSUTCDATETIME", () => DateTimeOffset.UtcNow);

        _contextOptions = new DbContextOptionsBuilder<ShopContext>()
            .UseSqlite(connection)
            .UseLoggerFactory(LoggerFactory)
            .EnableSensitiveDataLogging()
            .Options;
    }

    public async Task InitializeAsync()
    {
        await using var context = CreateContext();
        await context.Database.EnsureCreatedAsync();
    }

    public Task DisposeAsync() => Task.CompletedTask;
    
    protected ShopContext CreateContext() => new ShopContextForTests(_contextOptions);
}