using System.Collections.Generic;
using ThreadSafePriorityTaskQueue.PriorityQueue.Tasks;

namespace ThreadSafePriorityTaskQueue.PriorityQueue.Comparers
{
    public class ImpactBasedPriorityComparer : IComparer<SimpleTask>
    {
        public int Compare(SimpleTask x, SimpleTask y)
        {
            if (x.Priority.ImpactLevel.CompareTo(y.Priority.ImpactLevel) != 0)
            {
                return x.Priority.ImpactLevel.CompareTo(y.Priority.ImpactLevel);
            }
            else if (x.Priority.Deadline.CompareTo(y.Priority.Deadline) != 0)
            {
                return x.Priority.Deadline.CompareTo(y.Priority.Deadline) * -1;
            }

            return x.Priority.DesignatedBy.CompareTo(y.Priority.DesignatedBy);
        }
    }
}