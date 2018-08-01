using System;

namespace ThreadSafePriorityTaskQueue.PriorityQueue.Tasks
{
    public enum ImpactLevel
    {
        Critical = 100,
        High = 80,
        Medium = 60,
        Low = 40,
        Trivial = 20
    }

    public enum UserRole
    {
        QA = 100,
        Developer = 50
    }

    public class TaskPriority
    {
        public ImpactLevel ImpactLevel { get; set; }
        public UserRole DesignatedBy { get; set; }
        public DateTime Deadline { get; set; }
    }
}