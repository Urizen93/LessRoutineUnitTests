using DummyShop.Persistence;

namespace RealVsInMemory.Subjects;

public sealed class CrudCleanup : ICrud
{
    private readonly ICrud _crud;
    private readonly Func<ShopContext> _contextFactory;

    public CrudCleanup(ICrud crud, Func<ShopContext> contextFactory)
    {
        _crud = crud;
        _contextFactory = contextFactory;
    }

    public Task<CustomerEntity> Create() => DoAndCleanup(_crud.Create);

    public Task<CustomerEntity> Read() => DoAndCleanup(_crud.Read);

    public Task<CustomerEntity> Update() => DoAndCleanup(_crud.Update);

    public Task<CustomerEntity> Delete() => _crud.Delete();
    
    private Task<CustomerEntity> DoAndCleanup(Func<Task<CustomerEntity>> action) =>
        from customer in action()
        from _ in Cleanup(customer)
        select customer;

    private async Task<Unit> Cleanup(CustomerEntity customer)
    {
        await using var context = _contextFactory();
        context.Remove(customer);
        await context.SaveChangesAsync();
        
        return unit;
    }
}