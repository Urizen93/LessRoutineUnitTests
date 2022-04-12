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
        #region Hidden so far
        // connection.CreateFunction(@"SYSUTCDATETIME", () => DateTimeOffset.UtcNow);
        #endregion

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
        context.Customers.RemoveRange(await context.Customers.ToArrayAsync());
        context.Products.RemoveRange(await context.Products.ToArrayAsync());
        await context.SaveChangesAsync();
    }

    public Task DisposeAsync() => Task.CompletedTask;
    
    protected ShopContext CreateContext() => new ShopContextForTests(_contextOptions);
}