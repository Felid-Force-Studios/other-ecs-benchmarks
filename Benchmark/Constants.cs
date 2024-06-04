namespace Benchmark;

public static class Constants
{
    public const int Seed = 0x7ADE7455;
    public const int SmallEntityCount = 500;
    public const int MidEntityCount = 50_000;
    public const int LargeEntityCount = 500_000;
    public const int SmallIterationCount = 1_000;
    public const int MidIterationCount = 10_000;
    public const int LargeIterationCount = 100_000;

#if SHORT_RUN
    public const int EntityCount = SmallEntityCount;
    public const int SystemEntityCount = SmallEntityCount;
    public const int IterationCount = SmallIterationCount;
#else
    public const int EntityCount = MidEntityCount;
    public const int SystemEntityCount = LargeEntityCount;
    public const int IterationCount = LargeIterationCount;
#endif
}