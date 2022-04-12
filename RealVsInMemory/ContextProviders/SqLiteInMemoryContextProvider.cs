using DummyShop.Persistence;
using DummyShop.Tests.EntityFrameworkOverrides;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace RealVsInMemory.ContextProviders;

public sealed class SqLiteInMemoryContextProvider : IContextProvider
{
    private DbContextOptions<ShopContext>? _contextOptions;

    public async Task Init()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();
        connection.CreateFunction(@"SYSUTCDATETIME", () => DateTimeOffset.UtcNow.ToString("o"));

        _contextOptions = new DbContextOptionsBuilder<ShopContext>()
            .UseSqlite(connection)
            .Options;
        
        await using var context = CreateContext();
        await context.Database.EnsureCreatedAsync();
        context.Customers.RemoveRange(await context.Customers.ToArrayAsync());
        context.Products.RemoveRange(await context.Products.ToArrayAsync());
        await context.SaveChangesAsync();
    }
    
    public ShopContext CreateContext() => new ShopContextForTests(_contextOptions!);
}