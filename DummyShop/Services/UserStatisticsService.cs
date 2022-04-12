using DummyShop.Models;
using DummyShop.Persistence.Repository;
using System;
using System.Threading.Tasks;

namespace DummyShop.Services;

public sealed class UserStatisticsService
{
    private readonly IHasUserOrderStatistics _statistics;
    private readonly Func<DateTimeOffset> _thresholdProvider;

    public UserStatisticsService(
        IHasUserOrderStatistics statistics,
        Func<DateTimeOffset> thresholdProvider)
    {
        _statistics = statistics;
        _thresholdProvider = thresholdProvider;
    }

    public async Task<OrderStatistics> GetSumOfRelevantOrders(Email user)
    {
        var threshold = _thresholdProvider();
        var sumOfOrders = await _statistics.GetSumOfOrdersInRelatedPeriod(user, threshold);
        return new OrderStatistics(sumOfOrders, threshold);
    }
}