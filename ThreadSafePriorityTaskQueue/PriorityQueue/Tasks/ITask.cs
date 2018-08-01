namespace ThreadSafePriorityTaskQueue.PriorityQueue.Tasks
{
    public interface ITask : IQueueItem
    {
        TaskPriority Priority { get; set; }
    }
}