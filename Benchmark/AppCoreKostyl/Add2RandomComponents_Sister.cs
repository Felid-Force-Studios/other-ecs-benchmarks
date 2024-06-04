using System;
using Benchmark.KremAppCore;
using Benchmark.Utils;
using BenchmarkDotNet.Attributes;

namespace Benchmark.AppCoreKostyl
{
    [ArtifactsPath(".benchmark_results/Add2RandomComponents")]
    [BenchmarkCategory(Categories.PerInvocationSetup)]
    [MemoryDiagnoser]
    public class Add2RandomComponents_Sister : IBenchmark
    {
        [Params(Constants.EntityCount)]
        public int EntityCount { get; set; }

        [Params(true, false)]
        public bool RandomOrder { get; set; }

        [Params(1, 32)]
        public int ChunkSize { get; set; }
        public SisterContext Context { get; set; }

        private Array[] _entitySets;
        private Random _rnd;
        [IterationSetup]
        public void Setup()
        {
            Context = new SisterContext(EntityCount);
            Context?.Setup();
            var setsCount = EntityCount / ChunkSize + 1;
            _entitySets = new Array[setsCount];
            for (var i = 0; i < setsCount; i++)
            {
                _entitySets[i] = Context?.PrepareSet(ChunkSize);
                Context?.CreateEntities(_entitySets[i]);
            }

            if (RandomOrder)
                _entitySets.Shuffle();
            Context?.Warmup<Component1, Component2>(0);
            Context?.Warmup<Component2, Component3>(1);
            Context?.Warmup<Component3, Component4>(2);
            Context?.Warmup<Component4, Component1>(3);
            Context?.FinishSetup();
            _rnd = new Random(Constants.Seed);
        }

        [IterationCleanup]
        public void Cleanup()
        {
            var setsCount = EntityCount / ChunkSize + 1;
            for (var i = 0; i < setsCount; i++)
                Context?.DeleteEntities(_entitySets[i]);
            Context?.Cleanup();
            Context?.Dispose();
            Context = default;
        }

        [Benchmark]
        public void Run()
        {
            for (var i = 0; i < _entitySets.Length; i++)
            {
                Context?.Lock();
                switch (_rnd.Next() % 4)
                {
                    case 0:
                        Context?.AddComponent<Component1, Component2>(_entitySets[i], 0);
                        break;
                    case 1:
                        Context?.AddComponent<Component2, Component3>(_entitySets[i], 1);
                        break;
                    case 2:
                        Context?.AddComponent<Component3, Component4>(_entitySets[i], 2);
                        break;
                    case 3:
                        Context?.AddComponent<Component4, Component1>(_entitySets[i], 3);
                        break;
                }

                Context?.Commit();
            }
        }
    }
}