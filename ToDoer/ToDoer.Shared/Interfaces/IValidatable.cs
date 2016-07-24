namespace ToDoer.Interfaces
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An interface containing all the validation required to be implemented by a model.
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// Gets the validation messages.
        /// </summary>
        /// <value>
        /// The validation messages.
        /// </value>
        Dictionary<string, List<IValidationMessage>> ValidationMessages { get; }

        /// <summary>
        /// Adds the validation message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="property">The property.</param>
        void AddValidationMessage(IValidationMessage message, string property = "");

        /// <summary>
        /// Removes the validation message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="property">The property.</param>
        void RemoveValidationMessage(string message, string property = "");

        /// <summary>
        /// Removes the validation messages.
        /// </summary>
        /// <param name="property">The property.</param>
        void RemoveValidationMessages(string property = "");

        /// <summary>
        /// Determines whether [has validation message type] [the specified property].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">The property.</param>
        /// <returns>
        ///   <c>true</c> if [has validation message type] [the specified property]; otherwise, <c>false</c>.
        /// </returns>
        bool HasValidationMessageType<T>(string property = "");

        /// <summary>
        /// Validates the property.
        /// </summary>
        /// <param name="validationDelegate">The validation delegate.</param>
        /// <param name="failureMessage">The failure message.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>An instance of IValidationMessage</returns>
        IValidationMessage ValidateProperty(Func<string, IValidationMessage> validationDelegate, string failureMessage, string propertyName = "");
    }
}
