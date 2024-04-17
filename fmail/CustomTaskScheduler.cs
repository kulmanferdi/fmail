using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace fmail
{
    /// <summary>
    /// Represents a custom task scheduler that executes tasks on a specific <see cref="SynchronizationContext"/>.
    /// </summary>
    public class CustomTaskScheduler : TaskScheduler
    {
        readonly ConcurrentQueue<Task> tasks = new ConcurrentQueue<Task>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTaskScheduler"/> class using the current <see cref="SynchronizationContext"/>.
        /// </summary>
        public CustomTaskScheduler() : this(SynchronizationContext.Current)
        {
        }

        // <summary>
        /// Initializes a new instance of the <see cref="CustomTaskScheduler"/> class with the specified <see cref="SynchronizationContext"/>.
        /// </summary>
        /// <param name="context">The synchronization context on which to execute tasks.</param>
        public CustomTaskScheduler(SynchronizationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets the synchronization context associated with this task scheduler.
        /// </summary>
        public SynchronizationContext Context { get; private set; }

        /// <summary>
        /// Gets the maximum concurrency level supported by this task scheduler.
        /// </summary>
        public override int MaximumConcurrencyLevel { get { return 1; } }

        void TryDequeueAndExecuteTask(object state)
        {
            if (tasks.TryDequeue(out var toExecute))
                TryExecuteTask(toExecute);
        }

        /// <summary>
        /// Queues a task to the scheduler for execution.
        /// </summary>
        /// <param name="task">The task to be queued.</param>
        protected override void QueueTask(Task task)
        {
            // Add the task to the collection
            tasks.Enqueue(task);

            // Queue up a delegate that will dequeue and execute a task
            Context.Post(TryDequeueAndExecuteTask, null);
        }

        /// <summary>
        /// Determines whether the provided task can be executed synchronously in this call.
        /// </summary>
        /// <param name="task">The task to be executed.</param>
        /// <param name="taskWasPreviouslyQueued">true if the task was previously queued; otherwise, false.</param>
        /// <returns>true if the task was executed synchronously; otherwise, false.</returns>
        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return SynchronizationContext.Current == Context && TryExecuteTask(task);
        }

        /// <summary>
        /// Gets an enumerable of the tasks currently scheduled on this scheduler.
        /// </summary>
        /// <returns>An enumerable of the tasks currently scheduled on this scheduler.</returns>
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return tasks.ToArray();
        }
    }
}
