using BenchmarkDotNet.Attributes;
using RealVsInMemory.ContextProviders;
using RealVsInMemory.Subjects;

namespace RealVsInMemory;

public class RealVsInMemoryBenchmark
{
    private const int Times = 20;
    
    [Benchmark]
    public async Task SqLiteInMemory()
    {
        for (var i = 0; i < Times; i++)
        {
            await SqLite().Create();
            await SqLite().Read();
            await SqLite().Update();
            await SqLite().Delete();
        }
    }
    
    [Benchmark]
    public async Task MsSqlReal()
    {
        for (var i = 0; i < Times; i++)
        {
            await MsSql().Create();
            await MsSql().Read();
            await MsSql().Update();
            await MsSql().Delete();
        }
    }
    
    private static ICrud SqLite() =>
        new Crud(
            new SqLiteInMemoryContextProvider());
    
    private static ICrud MsSql()
    {
        var contextProvider = new MsSqlContextProvider();
        return new CrudCleanup(
            new Crud(contextProvider),
            contextProvider.CreateContext);
    }
}