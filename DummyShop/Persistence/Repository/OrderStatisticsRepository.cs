using DummyShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DummyShop.Persistence.Repository;

public sealed class OrderStatisticsRepository
{
    private readonly ShopContext _context;

    public OrderStatisticsRepository(ShopContext context) => _context = context;

    public Task<decimal> GetSumOfOrdersInRelatedPeriod(Email user) =>
    (
        from customer in _context.Customers
            
        join order in _context.Orders
            on customer.ID equals order.CustomerID
                
        join orderLine in _context.OrderLines
            on order.ID equals orderLine.OrderID
                
        join product in _context.Products
            on orderLine.ProductID equals product.ID
                
        where customer.Email == user
            #region A bit more complicated stuff
            // && order.CreatedAt >= DateTimeOffset.Now.AddYears(-3)
            #endregion
                  
        select orderLine.Quantity * product.Price
    ).SumAsync();
}