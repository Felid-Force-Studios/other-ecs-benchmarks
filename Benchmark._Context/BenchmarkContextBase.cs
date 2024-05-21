using MorpehComponent = Scellecs.Morpeh.IComponent;

namespace Benchmark._Context;

public abstract class BenchmarkContextBase : IDisposable
{
    /// <summary>
    /// Initialize world for test.
    /// </summary>
    /// <param name="entityCount"> Amount of entities will be used in benchmark. </param>
    public abstract void Setup(int entityCount);
    
    /// <summary>
    /// Place where you can set up your stashes, caches, etc.
    /// </summary>
    /// <param name="poolId"> Id of cache - will be passed to <see cref="CreateEntities{T1}"/>, <see cref="AddComponent{T1}"/>, <see cref="RemoveComponent{T1}"/>. </param>
    public abstract void Warmup<T1>(int poolId) where T1 : struct, MorpehComponent;

    /// <summary>
    /// Place where you can set up your stashes, caches, etc.
    /// </summary>
    /// <param name="poolId"> Id of cache - will be passed to <see cref="CreateEntities{T1,T2}"/>, <see cref="AddComponent{T1, T2}"/>, <see cref="RemoveComponent{T1, T2}"/>. </param>
    public abstract void Warmup<T1, T2>(int poolId)
        where T1 : struct, MorpehComponent where T2 : struct, MorpehComponent;

    /// <summary>
    /// Place where you can set up your stashes, caches, etc.
    /// </summary>
    /// <param name="poolId"> Id of cache - will be passed to <see cref="CreateEntities{T1,T2,T3}"/>, <see cref="AddComponent{T1, T2, T3}"/>, <see cref="RemoveComponent{T1, T2, T3}"/>. </param>
    public abstract void Warmup<T1, T2, T3>(int poolId) where T1 : struct, MorpehComponent
        where T2 : struct, MorpehComponent
        where T3 : struct, MorpehComponent;

    /// <summary>
    /// Place where you can set up your stashes, caches, etc.
    /// </summary>
    /// <param name="poolId"> Id of cache - will be passed to <see cref="CreateEntities{T1,T2,T3,T4}"/>, <see cref="AddComponent{T1, T2, T3, T4}"/>, <see cref="RemoveComponent{T1, T2, T3, T4}"/>. </param>
    public abstract void Warmup<T1, T2, T3, T4>(int poolId) where T1 : struct, MorpehComponent
        where T2 : struct, MorpehComponent
        where T3 : struct, MorpehComponent
        where T4 : struct, MorpehComponent;

    /// <summary>
    /// Clean up the world.
    /// </summary>
    public abstract void Cleanup();

    /// <summary>
    /// Disposition of context.
    /// </summary>
    public void Dispose()
    {
    }

    /// <summary>
    /// Acquire lock for deferred structural changes/
    /// </summary>
    public abstract void Lock();

    /// <summary>
    /// Release lock and apply structural changes.
    /// </summary>
    public abstract void Commit();

    /// <summary>
    /// Create empty entity.
    /// </summary>
    /// <param name="entitySet"> Entity set object returned by <see cref="CreateEntities"/> </param>
    public abstract void CreateEntities(in object entitySet);

    /// <summary>
    /// Create entity with components.
    /// </summary>
    /// <param name="entitySet"> Entity set object returned by <see cref="CreateEntities"/> </param>
    /// <param name="poolId"> ID of pool from <see cref="Warmup{T1}"/></param>
    /// <returns> Entity set identifier. Literally it can be anything, filter, query ids array, whatever. Will be used as argument in <see cref="AddComponent{T1}"/>, <see cref="RemoveComponent{T1}"/>, <see cref="DeleteEntities{T1}"/> </returns>
    public abstract void CreateEntities<T1>(in object entitySet, in int poolId = -1) where T1 : struct, MorpehComponent;

    /// <summary>
    /// Create entity with components.
    /// </summary>
    /// <param name="entitySet"> Entity set object returned by <see cref="CreateEntities"/> </param>
    /// <param name="poolId"> ID of pool from <see cref="Warmup{T1, T2}"/></param>
    /// <returns> Entity set identifier. Literally it can be anything, filter, query ids array, whatever. Will be used as argument in <see cref="AddComponent{T1}"/>, <see cref="RemoveComponent{T1}"/>, <see cref="DeleteEntities{T1}"/> </returns>
    public abstract void CreateEntities<T1, T2>(in object entitySet, in int poolId = -1) where T1 : struct, MorpehComponent
        where T2 : struct, MorpehComponent;

    /// <summary>
    /// Create entity with components.
    /// </summary>
    /// <param name="entitySet"> Entity set object returned by <see cref="CreateEntities"/> </param>
    /// <param name="poolId"> ID of pool from <see cref="Warmup{T1, T2, T3}"/></param>
    /// <returns> Entity set identifier. Literally it can be anything, filter, query ids array, whatever. Will be used as argument in <see cref="AddComponent{T1}"/>, <see cref="RemoveComponent{T1}"/>, <see cref="DeleteEntities{T1}"/> </returns>
    public abstract void CreateEntities<T1, T2, T3>(in object entitySet, in int poolId = -1) where T1 : struct, MorpehComponent
        where T2 : struct, MorpehComponent
        where T3 : struct, MorpehComponent;

    /// <summary>
    /// Create entity with components.
    /// </summary>
    /// <param name="entitySet"> Entity set object returned by <see cref="CreateEntities"/> </param>
    /// <param name="poolId"> ID of pool from <see cref="Warmup{T1, T2, T3, T4}"/></param>
    /// <returns> Entity set identifier. Literally it can be anything, filter, query ids array, whatever. Will be used as argument in <see cref="AddComponent{T1}"/>, <see cref="RemoveComponent{T1}"/>, <see cref="DeleteEntities{T1}"/> </returns>
    public abstract void CreateEntities<T1, T2, T3, T4>(in object entitySet, in int poolId = -1) where T1 : struct, MorpehComponent
        where T2 : struct, MorpehComponent
        where T3 : struct, MorpehComponent
        where T4 : struct, MorpehComponent;

    /// <summary>
    /// Add component to entity.
    /// </summary>
    /// <param name="entitySet"> Entity set object returned by <see cref="CreateEntities"/> </param>
    /// <param name="poolId"> ID of pool from <see cref="Warmup{T1}"/></param>
    public abstract void AddComponent<T1>(in object entitySet, in int poolId = -1) where T1 : struct, MorpehComponent;

    /// <summary>
    /// Add components to entity.
    /// </summary>
    /// <param name="entitySet"> Entity set object returned by <see cref="CreateEntities"/> </param>
    /// <param name="poolId"> ID of pool from <see cref="Warmup{T1, T2}"/></param>
    public abstract void AddComponent<T1, T2>(in object entitySet, in int poolId = -1) where T1 : struct, MorpehComponent
        where T2 : struct, MorpehComponent;

    /// <summary>
    /// Add components to entity.
    /// </summary>
    /// <param name="entitySet"> Entity set object returned by <see cref="CreateEntities"/> </param>
    /// <param name="poolId"> ID of pool from <see cref="Warmup{T1, T2, T3}"/></param>
    public abstract void AddComponent<T1, T2, T3>(in object entitySet, in int poolId = -1)
        where T1 : struct, MorpehComponent where T2 : struct, MorpehComponent where T3 : struct, MorpehComponent;

    /// <summary>
    /// Add components to entity.
    /// </summary>
    /// <param name="entitySet"> Entity set object returned by <see cref="CreateEntities"/> </param>
    /// <param name="poolId"> ID of pool from <see cref="Warmup{T1, T2, T3, T4}"/></param>
    public abstract void AddComponent<T1, T2, T3, T4>(in object entitySet, in int poolId = -1)
        where T1 : struct, MorpehComponent
        where T2 : struct, MorpehComponent
        where T3 : struct, MorpehComponent
        where T4 : struct, MorpehComponent;

    /// <summary>
    /// Removes component to entity.
    /// </summary>
    /// <param name="entitySet"> Entity set object returned by <see cref="CreateEntities"/> </param>
    /// <param name="poolId"> ID of pool from <see cref="Warmup{T1}"/></param>
    public abstract void RemoveComponent<T1>(in object entitySet, in int poolId = -1) where T1 : struct, MorpehComponent;

    /// <summary>
    /// Removes component to entity.
    /// </summary>
    /// <param name="entitySet"> Entity set object returned by <see cref="CreateEntities"/> </param>
    /// <param name="poolId"> ID of pool from <see cref="Warmup{T1, T2}"/></param>
    public abstract void RemoveComponent<T1, T2>(in object entitySet, in int poolId = -1)
        where T1 : struct, MorpehComponent where T2 : struct, MorpehComponent;

    /// <summary>
    /// Removes component to entity.
    /// </summary>
    /// <param name="entitySet"> Entity set object returned by <see cref="CreateEntities"/> </param>
    /// <param name="poolId"> ID of pool from <see cref="Warmup{T1, T2, T3}"/></param>
    public abstract void RemoveComponent<T1, T2, T3>(in object entitySet, in int poolId = -1)
        where T1 : struct, MorpehComponent where T2 : struct, MorpehComponent where T3 : struct, MorpehComponent;

    /// <summary>
    /// Removes component to entity.
    /// </summary>
    /// <param name="entitySet"> Entity set object returned by <see cref="CreateEntities"/> </param>
    /// <param name="poolId"> ID of pool from <see cref="Warmup{T1, T2, T3, T4}"/></param>
    public abstract void RemoveComponent<T1, T2, T3, T4>(in object entitySet, in int poolId = -1)
        where T1 : struct, MorpehComponent
        where T2 : struct, MorpehComponent
        where T3 : struct, MorpehComponent
        where T4 : struct, MorpehComponent;

    /// <summary>
    /// Shuffles entities in set.
    /// </summary>
    /// <param name="entitySet"> Entity set object returned by <see cref="CreateEntities"/> </param>
    /// <returns> New or existing shuffled set. </returns>
    public abstract object Shuffle(in object entitySet);

    /// <summary>
    /// Prepares entity set.
    /// </summary>
    /// <param name="count"> Count of entities to be stored. </param>
    /// <returns> New or existing shuffled set. </returns>
    public abstract object PrepareSet(in int count);
}