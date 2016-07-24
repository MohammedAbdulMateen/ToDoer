namespace ToDoer.Models
{
    using PropertyChanged;
    using System;
    using ToDoer.Common;

    /// <summary>
    /// The task model
    /// </summary>
    [ImplementPropertyChanged]
    public class TaskModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskModel"/> class.
        /// </summary>
        public TaskModel()
        {
            this.ContextId = Constants.DefaultContextId;
            this.Context = LocalizationService.GetLocalizedMessage(Constants.Inbox);
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DoNotNotify]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the context identifier.
        /// </summary>
        /// <value>
        /// The context identifier.
        /// </value>
        [DoNotNotify]
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
        public DateTimeOffset? DueDate { get; set; }

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
        public DateTimeOffset? ReminderDate { get; set; }

        /// <summary>
        /// Gets or sets the reminder time.
        /// </summary>
        /// <value>
        /// The reminder time.
        /// </value>
        public TimeSpan? ReminderTime { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        [DoNotNotify]
        public bool IsValid
        {
            get
            {
                return (this.ContextId > 0 || this.ContextId == Constants.DefaultContextId)
                    && !string.IsNullOrWhiteSpace(this.Context)
                    && !string.IsNullOrWhiteSpace(this.Todo);
            }
        }
    }
}
