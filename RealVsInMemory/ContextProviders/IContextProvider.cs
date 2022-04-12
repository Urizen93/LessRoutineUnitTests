using DummyShop.Persistence;

namespace RealVsInMemory.ContextProviders;

public interface IContextProvider
{
    Task Init();

    ShopContext CreateContext();
}