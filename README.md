# other-ecs-benchmarks

alternative to https://github.com/Doraku/Ecs.CSharp.Benchmark

# Latest run

updated with actions and can be found [here](https://gist.github.com/blackbone/6d254a684cf580441bf58690ad9485c3)

# What is all about?

General idea is to hide implementation of each ECS under context abstraction and work with it from benchmark implementations.

Benchmarks design follow 2 rules which I try to balance with:
* **Strict usage** - to ensure all benchmarks are running with same flow to avoid cheating.
* **Features utilization** - to allow implementations to run in perfomant way.

General flow of any benchmark execution is divided into 3 steps:
* Preparation
  * Creating world
  * Creating initial entities if needed
  * Initialize filters and queries or other stuff which used to gain perfomance
* Benchmark call
  * Aquiring lock of world
  * Run main logic
  * Commiting changes
* Cleanup - mostly omited

# Implemented contexts

|      ECS | Version                                                              | Implemented | Verified |
|---------:|:---------------------------------------------------------------------|-------------|----------|
|     Arch | [1.2.8](https://www.nuget.org/packages/Arch/1.2.8)                   | ✅           | ❌        |
|  fennecs | [0.3.5-beta](https://www.nuget.org/packages/fennecs/0.3.5-beta)      | ✅           | ❌        |
|   Morpeh | [2023.1.0](https://www.nuget.org/packages/Scellecs.Morpeh/2023.1.0)  | ✅           | ❌        |

# Implemented benchmarks

| Benchmark                       | Description                                       |
|---------------------------------|---------------------------------------------------|
| Create Empty Entity             | Creates [EntityCount] empty entities              |
| Create Entity With N Components | Creates [EntityCount] entitites with N components |
| Add N Components                | Adds N components to [EntityCount] entities       |
| Remove N Components             | Adds N components to [EntityCount] entities       |

# Contribution

- Fork
- Implement
- Create PR