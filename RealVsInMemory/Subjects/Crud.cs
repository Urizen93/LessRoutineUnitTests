using DummyShop.Persistence;
using Microsoft.EntityFrameworkCore;
using RealVsInMemory.ContextProviders;
using SemanticComparison.Fluent;

namespace RealVsInMemory.Subjects;

public sealed class Crud : ICrud
{
    private readonly Func<ShopContext> _contextProvider;
    private readonly Task _init;

    public Crud(IContextProvider contextProvider)
    {
        _init = contextProvider.Init();
        _contextProvider = contextProvider.CreateContext;
    }

    public async Task<CustomerEntity> Create()
    {
        await _init;
        
        var customer = new CustomerEntity { ID = 1, Email = "test@fastdev.se" };
        await using (var context = _contextProvider())
        {
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
        }
        
        await using (var context = _contextProvider())
        {
            var actual = await context.Customers.AnyAsync(existing =>
                existing.ID == customer.ID
                && existing.Email == customer.Email);
            
            Assert.True(actual);
        }
        
        return customer;
    }
    
    public async Task<CustomerEntity> Read()
    {
        await _init;
        
        var customer = new CustomerEntity { ID = 1, Email = "test@fastdev.se" };
        await using (var context = _contextProvider())
        {
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
        }

        await using (var context = _contextProvider())
        {
            var actual = await context.Customers.FirstOrDefaultAsync(existing =>
                existing.ID == customer.ID);
            
            customer.AsSource()
                .OfLikeness<CustomerEntity>()
                .Without(entity => entity.Orders)
                .ShouldEqual(actual!);
        }

        return customer;
    }
    
    public async Task<CustomerEntity> Update()
    {
        await _init;
        
        var customer = new CustomerEntity { ID = 1, Email = "test@fastdev.se" };
        await using (var context = _contextProvider())
        {
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
        }

        await using (var context = _contextProvider())
        {
            customer = await context.Customers.FirstOrDefaultAsync(existing =>
                existing.ID == customer.ID);
            customer!.Email = "other@fastdev.se";
            await context.SaveChangesAsync();
        }
        
        await using (var context = _contextProvider())
        {
            var actual = await context.Customers.AnyAsync(existing =>
                existing.ID == customer.ID
                && existing.Email == customer.Email);
            
            Assert.True(actual);
        }

        return customer;
    }
    
    public async Task<CustomerEntity> Delete()
    {
        await _init;
        
        var customer = new CustomerEntity { ID = 1, Email = "test@fastdev.se" };
        await using (var context = _contextProvider())
        {
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
        }
        
        await using (var context = _contextProvider())
        {
            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
        }
        
        await using (var context = _contextProvider())
        {
            var actual = await context.Customers.AnyAsync(existing =>
                existing.ID == customer.ID);
            
            Assert.False(actual);
        }
        
        return customer;
    }
}