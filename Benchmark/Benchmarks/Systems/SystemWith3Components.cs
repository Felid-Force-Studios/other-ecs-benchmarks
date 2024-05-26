using Benchmark._Context;
using BenchmarkDotNet.Attributes;

namespace Benchmark.Benchmarks.Systems;

[ArtifactsPath(".benchmark_results/" + nameof(SystemWith3Components<T>))]
[MemoryDiagnoser]
#if CHECK_CACHE_MISSES
[HardwareCounters(BenchmarkDotNet.Diagnosers.HardwareCounter.CacheMisses)]
#endif
// ReSharper disable once InconsistentNaming
public class SystemWith3Components<T> : SystemBenchmarkBase<T> where T : BenchmarkContextBase, new()
{
    [Params(0, 10)] public int Padding { get; set; }

    protected override void OnSetup()
    {
        base.OnSetup();

        Context.Warmup<Component1>(0);
        Context.Warmup<Component2>(1);
        Context.Warmup<Component3>(2);
        Context.Warmup<Component1, Component2, Component3>(3);
        
        var set = Context.PrepareSet(1);
        Context.Lock();
        // set up entities
        for (var i = 0; i < EntityCount; ++i)
        {
            for (var j = 0; j < Padding; ++j)
            {
                switch (j % 2)
                {
                    case 0: Context.CreateEntities<Component1>(set, 0); break;
                    case 1: Context.CreateEntities<Component2>(set, 1); break;
                    case 2: Context.CreateEntities<Component3>(set, 2); break;
                }
            }
            Context.CreateEntities(set, 3, default(Component1), new Component2 { Value = 1 }, new Component3 { Value = 1 });
        }
        Context.Commit();

        unsafe
        {
            // set up systems
            Context.AddSystem<Component1, Component2, Component3>(&Update, 3);
        }
    }

    private static void Update(ref Component1 c1, ref Component2 c2, ref Component3 c3) => c1.Value += c2.Value + c3.Value;

    [Benchmark]
    public override void Run()
    {
        Context.Tick(0.1f);
    }
}