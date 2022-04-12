using DummyShop.Models;
using DummyShop.Persistence;
using DummyShop.Persistence.Repository;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

const string connectionString =
    "Server=(LocalDb)\\MSSQLLocalDB;"
    + "Initial Catalog=DummyShop;"
    + "Integrated Security=true;";

var options = new DbContextOptionsBuilder<ShopContext>()
    .UseSqlServer(connectionString)
    .Options;
var context = new ShopContext(options);
context.Database.EnsureCreated();
var statistics = new OrderStatisticsRepository(context);

var john = new Email("john.smith@fastdev.se");
var jane = new Email("jane.doe@fastdev.se");

Console.WriteLine(await PrintStatistics(statistics, john));
Console.WriteLine(await PrintStatistics(statistics, jane));

static Task<string> PrintStatistics(OrderStatisticsRepository statistics, Email user) =>
    from sumOfOrders in statistics.GetSumOfOrdersInRelatedPeriod(user)
    select Print(user, sumOfOrders);

static string Print(Email user, decimal sumOfOrders) =>
    $"In relevant period {user} ordered for the amount of ${sumOfOrders:#0.00};";