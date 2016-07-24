namespace ToDoer.Common
{
    using ToDoer.Interfaces;

    /// <summary>
    /// The error validation message
    /// </summary>
    public class ValidationErrorMessage : IValidationMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationErrorMessage"/> class.
        /// </summary>
        public ValidationErrorMessage()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationErrorMessage"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ValidationErrorMessage(string message)
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
