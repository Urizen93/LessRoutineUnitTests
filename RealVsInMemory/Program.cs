using BenchmarkDotNet.Running;
using RealVsInMemory;

BenchmarkRunner.Run<RealVsInMemoryBenchmark>();
Console.ReadLine();