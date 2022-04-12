using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using DummyShop.Models;
using DummyShop.Persistence.Repository;
using DummyShop.Services;
using DummyShop.Tests.Attributes;
using DummyShop.Tests.Customizations;
using NSubstitute;

namespace DummyShop.Tests.Unit.Services.UserStatisticsServiceTest;

public sealed class GetSumOfRelevantOrders
{
    [Theory, UserWithSum]
    public async Task Returns_OrderStatistics_ImplicitDependencies(
        Email user,
        UserStatisticsService sut,
        OrderStatistics expected)
    {
        var actual = await sut.GetSumOfRelevantOrders(user);
        
        Assert.Equal(expected, actual);
    }
    
    private sealed class UserWithSumAttribute : AutoDataAttribute
    {
        public UserWithSumAttribute() : base(FixtureFactory) { }

        private static IFixture FixtureFactory()
        {
            var fixture = new Fixture()
                .Customize(new ValidEmail());

            var email = fixture.Freeze<Email>();
            var (sumOfOrders, threshold) = fixture.Freeze<OrderStatistics>();

            #region Dependencies

            var statistics = fixture.Freeze<IHasUserOrderStatistics>();
            statistics.GetSumOfOrdersInRelatedPeriod(email, threshold).Returns(sumOfOrders);
            
            fixture.Inject(() => threshold);

            #endregion
            
            return fixture;
        }
    }
    
    #region Explicit
    
    [Theory, AutoNSubData]
    public async Task Returns_OrdersStatistics_ExplicitDependencies(
        [ValidEmail] Email user,
        IHasUserOrderStatistics statistics,
        [Substitute] Func<DateTimeOffset> thresholdProvider,
        OrderStatistics expected)
    {
        statistics.GetSumOfOrdersInRelatedPeriod(user, expected.Since).Returns(expected.SumOfOrders);
        thresholdProvider.Invoke().Returns(expected.Since);
        
        var sut = new UserStatisticsService(statistics, thresholdProvider);

        var actual = await sut.GetSumOfRelevantOrders(user);
        
        Assert.Equal(expected, actual);
    }
    
    #endregion

    #region Combined
    
    [Theory, AutoNSubData]
    public async Task Returns_OrdersStatistics_TheWorstOfBothWorlds(
        [ValidEmail] Email user,
        [Frozen] IHasUserOrderStatistics statistics,
        [Frozen, Substitute] Func<DateTimeOffset> thresholdProvider,
        UserStatisticsService sut,
        OrderStatistics expected)
    {
        statistics.GetSumOfOrdersInRelatedPeriod(user, expected.Since).Returns(expected.SumOfOrders);
        thresholdProvider.Invoke().Returns(expected.Since);

        var actual = await sut.GetSumOfRelevantOrders(user);
        
        Assert.Equal(expected, actual);
    }

    #endregion

    #region Verify

    [Theory, AutoNSubData]
    public async Task Calls_GetSumOfOrdersInRelatedPeriod(
        [ValidEmail] Email user,
        [Frozen] IHasUserOrderStatistics statistics,
        UserStatisticsService sut)
    {
        _ = await sut.GetSumOfRelevantOrders(user);

        await statistics.Received().GetSumOfOrdersInRelatedPeriod(user, Arg.Any<DateTimeOffset>());
    }

    #endregion
}