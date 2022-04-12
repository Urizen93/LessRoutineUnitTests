using System;

namespace DummyShop.Models;

public sealed record OrderStatistics(decimal SumOfOrders, DateTimeOffset Since);