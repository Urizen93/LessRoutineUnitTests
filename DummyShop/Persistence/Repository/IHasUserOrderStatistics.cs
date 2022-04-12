using DummyShop.Models;
using System;
using System.Threading.Tasks;

namespace DummyShop.Persistence.Repository;

public interface IHasUserOrderStatistics
{
    Task<decimal> GetSumOfOrdersInRelatedPeriod(Email user, DateTimeOffset threshold);
}