using System;
using System.Collections.Generic;
using ThreadSafePriorityTaskQueue.PriorityQueue;
using ThreadSafePriorityTaskQueue.PriorityQueue.Comparers;
using ThreadSafePriorityTaskQueue.PriorityQueue.Tasks;

namespace ThreadSafePriorityTaskQueue
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var queue = new ThreadSafePriorityQueue<SimpleTask>(new ImpactBasedPriorityComparer());

            var listOfTasks = new List<SimpleTask>(6)
            {
                new SimpleTask()
                {
                    Priority = new TaskPriority()
                    {
                        ImpactLevel = ImpactLevel.Low,
                        DesignatedBy = UserRole.QA,
                        Deadline = DateTime.UtcNow
                    }
                },
                new SimpleTask()
                {
                    Priority = new TaskPriority()
                    {
                        ImpactLevel = ImpactLevel.Critical,
                        DesignatedBy = UserRole.Developer,
                        Deadline = DateTime.UtcNow
                    }
                },
                new SimpleTask()
                {
                    Priority = new TaskPriority()
                    {
                        ImpactLevel = ImpactLevel.High,
                        DesignatedBy = UserRole.Developer,
                        Deadline = DateTime.MaxValue
                    }
                },
                new SimpleTask()
                {
                    Priority = new TaskPriority()
                    {
                        ImpactLevel = ImpactLevel.High,
                        DesignatedBy = UserRole.QA,
                        Deadline = DateTime.MaxValue
                    }
                }
                ,new SimpleTask()
                {
                    Priority = new TaskPriority()
                    {
                        ImpactLevel = ImpactLevel.Trivial,
                        DesignatedBy = UserRole.Developer,
                        Deadline = DateTime.UtcNow
                    }
                }
                ,new SimpleTask()
                {
                    Priority = new TaskPriority()
                    {
                        ImpactLevel = ImpactLevel.Medium,
                        DesignatedBy = UserRole.QA,
                        Deadline = DateTime.UtcNow
                    }
                }
            };

            listOfTasks.ForEach(task =>
            {
                Console.WriteLine($"* Item to be enqueued [{task.Id}] :\n\tImpactLevel = {task.Priority.ImpactLevel}\n\tDesignatedBy = {task.Priority.DesignatedBy}\n\tDeadline = {task.Priority.Deadline}");
                queue.Enqueue(task);
                Console.WriteLine($"Enqueued [{task.Id}].\t# of items in queue: {queue.Count}\n");
            });

            var highestPriorityItem = queue.Peek();
            if (highestPriorityItem != null)
            {
                Console.WriteLine($"\nThe highest priority item {highestPriorityItem.Id} found via Peek().");
            }

            var removedItem = queue.Remove(highestPriorityItem.Id);
            Console.WriteLine($"Removed item [{removedItem.Id}] from the queue.\n");

            while (!queue.IsEmpty)
            {
                Console.WriteLine($"Dequeued [{queue.Dequeue().Id}].\t# of items in queue: {queue.Count}");
            }

            Console.WriteLine("\nDONE");
            Console.ReadLine();
        }
    }
}