using System;

namespace ThreadSafePriorityTaskQueue.PriorityQueue.Tasks
{
    public class SimpleTask : ITask
    {
        public Guid Id { get; } = Guid.NewGuid();
        public TaskPriority Priority { get; set; }
        public object Content { get; set; }
    }
}