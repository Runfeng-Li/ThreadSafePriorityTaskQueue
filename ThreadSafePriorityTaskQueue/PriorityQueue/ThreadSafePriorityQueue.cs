using C5;
using System;
using System.Collections.Generic;

namespace ThreadSafePriorityTaskQueue.PriorityQueue
{
    public class ThreadSafePriorityQueue<T> where T : IQueueItem
    {
        private readonly object _lock = new object();
        private readonly IntervalHeap<T> _intervalHeap;
        private readonly Dictionary<Guid, IPriorityQueueHandle<T>> _dictionary;

        public ThreadSafePriorityQueue(IComparer<T> comparer)
        {
            _intervalHeap = new IntervalHeap<T>(comparer);
            _dictionary = new Dictionary<Guid, IPriorityQueueHandle<T>>();
        }

        public bool IsEmpty
        {
            get
            {
                return _intervalHeap.IsEmpty;
            }
        }

        public int Count
        {
            get
            {
                return _intervalHeap.Count;
            }
        }

        public IQueueItem Peek()
        {
            lock (_lock)
            {
                try
                {
                    return _intervalHeap.FindMax();
                }
                catch (NoSuchItemException)
                {
                    return null;
                }
            }
        }

        public bool Exist(Guid id)
        {
            lock (_lock)
            {
                return _dictionary.ContainsKey(id);
            }
        }

        public void Enqueue(T queueItem)
        {
            lock (_lock)
            {
                IPriorityQueueHandle<T> handle = null;

                _intervalHeap.Add(ref handle, queueItem);
                _dictionary.Add(queueItem.Id, handle);
            }
        }

        public IQueueItem Dequeue()
        {
            lock (_lock)
            {
                IQueueItem queueItem;

                try
                {
                    queueItem = _intervalHeap.FindMax();
                }
                catch (NoSuchItemException)
                {
                    return null;
                }

                _dictionary.Remove(queueItem.Id);
                _intervalHeap.DeleteMax();

                return queueItem;
            }
        }

        public IQueueItem Remove(Guid id)
        {
            lock (_lock)
            {
                if (_dictionary.TryGetValue(id, out IPriorityQueueHandle<T> handle)
                    && _intervalHeap.Find(handle, out T queueItem))
                {
                    _dictionary.Remove(queueItem.Id);
                    _intervalHeap.Delete(handle);

                    return queueItem;
                }

                return null;
            }
        }
    }
}