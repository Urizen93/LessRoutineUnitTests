using DummyShop.Persistence;
using Microsoft.EntityFrameworkCore;

namespace RealVsInMemory.ContextProviders;

public sealed class MsSqlContextProvider : IContextProvider
{
    private const string ConnectionString =
        "Server=(LocalDb)\\MSSQLLocalDB;"
        + "Initial Catalog=DummyShop;"
        + "Integrated Security=true;";
    
    private readonly DbContextOptions<ShopContext> _contextOptions;

    public MsSqlContextProvider()
    {
        _contextOptions = new DbContextOptionsBuilder<ShopContext>()
            .UseSqlServer(ConnectionString)
            .Options;
    }

    public Task Init() => Task.CompletedTask;
    
    public ShopContext CreateContext() => new(_contextOptions);
}