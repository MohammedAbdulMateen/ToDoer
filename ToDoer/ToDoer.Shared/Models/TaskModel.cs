namespace ToDoer.Models
{
    using System;
    using ToDoer.Common;

    /// <summary>
    /// The task model
    /// </summary>
    public class TaskModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskModel"/> class.
        /// </summary>
        public TaskModel()
        {
            this.Context = Constants.DefaultContext;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the context identifier.
        /// </summary>
        /// <value>
        /// The context identifier.
        /// </value>
        public int ContextId { get; set; }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public string Context { get; set; }

        /// <summary>
        /// Gets or sets the todo.
        /// </summary>
        /// <value>
        /// The todo.
        /// </value>
        public string Todo { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the due time.
        /// </summary>
        /// <value>
        /// The due time.
        /// </value>
        public TimeSpan? DueTime { get; set; }

        /// <summary>
        /// Gets or sets the reminder date.
        /// </summary>
        /// <value>
        /// The reminder date.
        /// </value>
        public DateTime? ReminderDate { get; set; }

        /// <summary>
        /// Gets or sets the reminder time.
        /// </summary>
        /// <value>
        /// The reminder time.
        /// </value>
        public TimeSpan? ReminderTime { get; set; }
    }
}
