namespace ToDoer.Interfaces
{
    /// <summary>
    /// The interface describing the validation message
    /// </summary>
    public interface IValidationMessage
    {
        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string Message { get; }
    }
}
