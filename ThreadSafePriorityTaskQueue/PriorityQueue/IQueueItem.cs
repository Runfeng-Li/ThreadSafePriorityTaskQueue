using System;

namespace ThreadSafePriorityTaskQueue.PriorityQueue
{
    public interface IQueueItem
    {
        Guid Id { get; }
    }
}