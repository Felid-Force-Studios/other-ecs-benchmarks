using System;
using Benchmark._Context;
using BenchmarkDotNet.Attributes;

namespace Benchmark.Benchmarks.Entities.StructuralChanges;

[ArtifactsPath(".benchmark_results/" + nameof(OneAddThreeComponents<T, TE>))]
[MemoryDiagnoser]
#if CHECK_CACHE_MISSES
[HardwareCounters(BenchmarkDotNet.Diagnosers.HardwareCounter.CacheMisses)]
#endif
public abstract class OneAddThreeComponents<T, TE> : IBenchmark<T, TE> where T : IBenchmarkContext<TE>
{
    [Params(Constants.EntityCount)] public int EntityCount { get; set; }

    public T Context { get; set; }
    private TE[] _entitySet;

    [GlobalSetup]
    public void GlobalSetup()
    {
        Context = BenchmarkContext.Create<T>(EntityCount);
        Context.Setup();
        _entitySet =  Context.PrepareSet(EntityCount);
        Context.Warmup<Component1>(0);
        Context.Warmup<Component2, Component3, Component4>(1);
        Context.FinishSetup();
    }

    [IterationSetup]
    public void IterationSetup()
    {
        Context.CreateEntities<Component1>(_entitySet, 0, default(Component1));
    }

    [Benchmark]
    public void Run()
    {
        Context.AddComponent<Component2, Component3, Component4>(_entitySet, 1, default(Component2), default(Component3), default(Component4));
    }

    [IterationCleanup]
    public void IterationCleanup()
    {
        Context.DeleteEntities(_entitySet);
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        Context.Cleanup();
        Context.Dispose();
        Context = default;
    }
}
