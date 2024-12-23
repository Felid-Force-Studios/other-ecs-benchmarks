using Benchmark._Context;
using BenchmarkDotNet.Attributes;

namespace Benchmark.Benchmarks.Systems;

[ArtifactsPath(".benchmark_results/" + nameof(SystemWith2ComponentsMultipleComposition<T>))]
[MemoryDiagnoser]
#if CHECK_CACHE_MISSES
[HardwareCounters(BenchmarkDotNet.Diagnosers.HardwareCounter.CacheMisses)]
#endif
public abstract class SystemWith2ComponentsMultipleComposition<T> : IBenchmark<T> where T : IBenchmarkContext
{
    [Params(Constants.SystemEntityCount)] public int EntityCount { get; set; }
    [Params(0, 10)] public int Padding { get; set; }

    public T Context { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        Context = BenchmarkContext.Create<T>(EntityCount);
        Context.Setup();

        Context.Warmup<Component1>(0);
        Context.Warmup<Component2>(1);
        Context.Warmup<Component1, Component2>(2);
        Context.Warmup<Padding1>(3);
        Context.Warmup<Padding2>(4);
        Context.Warmup<Padding3>(5);
        Context.Warmup<Padding4>(6);

        var set = Context.PrepareSet(1);

        // set up entities
        for (var i = 0; i < EntityCount; ++i)
        {
            for (var j = 0; j < Padding; ++j)
                switch (j % 2)
                {
                    case 0:
                        Context.CreateEntities<Component1>(set, 0);
                        break;
                    case 1:
                        Context.CreateEntities<Component2>(set, 1);
                        break;
                }

            Context.CreateEntities(set, 2, default(Component1), new Component2 { Value = 1 });

            switch (i % 4)
            {
                case 0:
                    Context.AddComponent(set, 3, default(Padding1));
                    break;
                case 2:
                    Context.AddComponent(set, 4, default(Padding2));
                    break;
                case 3:
                    Context.AddComponent(set, 5, default(Padding3));
                    break;
                case 4:
                    Context.AddComponent(set, 6, default(Padding4));
                    break;
            }
        }


        unsafe
        {
            // set up systems
            Context.AddSystem<Component1, Component2>(&Update, 2);
        }

        Context.FinishSetup();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        Context.Cleanup();
        Context.Dispose();
        Context = default;
    }

    [Benchmark]
    public void Run()
    {
        Context.Tick(0.1f);
    }

    private static void Update(ref Component1 c1, ref Component2 c2)
    {
        c1.Value += c2.Value;
    }
}