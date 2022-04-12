using DummyShop.Models;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyShop.Persistence.Repository;

public sealed class CustomerRepository
{
    private readonly ShopContext _context;

    public CustomerRepository(ShopContext context) => _context = context;

    public Task<CustomerEntity?> Get(Email user) =>
    (
        from customer in _context.Customers
        where customer.Email == user
        select customer
    ).FirstOrDefaultAsync();

    #region List
    
    public Task<IEnumerable<CustomerEntity>> List() =>
    (
        from customer in _context.Customers
        select customer
    )
    .ToArrayAsync()
    .Map(Enumerable.AsEnumerable);
    
    #endregion
}