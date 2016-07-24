namespace ToDoer.Common
{
    using ToDoer.Interfaces;

    /// <summary>
    /// The warning validation message
    /// </summary>
    public class ValidationWarningMessage : IValidationMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationWarningMessage"/> class.
        /// </summary>
        public ValidationWarningMessage()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationWarningMessage"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ValidationWarningMessage(string message)
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; private set; }
    }

}
