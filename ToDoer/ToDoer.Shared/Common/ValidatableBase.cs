namespace ToDoer.Common
{
    using GalaSoft.MvvmLight;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ToDoer.Interfaces;

    /// <summary>
    /// The validation base implementing the IValidatable
    /// </summary>
    public abstract class ValidatableBase : ObservableObject, IValidatable
    {
        /// <summary>
        /// The validation messages
        /// </summary>
        private Dictionary<string, List<IValidationMessage>> validationMessages = new Dictionary<string, List<IValidationMessage>>();

        /// <summary>
        /// Gets the validation messages.
        /// </summary>
        /// <value>
        /// The validation messages.
        /// </value>
        public Dictionary<string, List<IValidationMessage>> ValidationMessages
        {
            get
            {
                return this.validationMessages;
            }
            private set
            {
                base.Set(ref validationMessages, value);
            }
        }

        /// <summary>
        /// Determines whether [has validation message type] [the specified property].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">The property.</param>
        /// <returns>
        ///   <c>true</c> if [has validation message type] [the specified property]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasValidationMessageType<T>(string property = "")
        {
            if (string.IsNullOrEmpty(property))
            {
                bool result = this.validationMessages.Values.Any(collection => collection.Any(msg => msg is T));

                return result;
            }

            return this.validationMessages.ContainsKey(property);
        }

        /// <summary>
        /// Adds the validation message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="property">The property.</param>
        public void AddValidationMessage(IValidationMessage message, string property = "")
        {
            if (string.IsNullOrEmpty(property))
            {
                return;
            }

            // If the key does not exist, then we create one.
            if (!this.validationMessages.ContainsKey(property))
            {
                this.validationMessages[property] = new List<IValidationMessage>();
            }

            if (this.validationMessages[property].Any(msg => msg.Message.Equals(message.Message) || msg == message))
            {
                return;
            }

            this.validationMessages[property].Add(message);
        }

        /// <summary>
        /// Removes the validation message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="property">The property.</param>
        public void RemoveValidationMessage(string message, string property = "")
        {
            if (string.IsNullOrEmpty(property))
            {
                return;
            }

            if (!this.validationMessages.ContainsKey(property))
            {
                return;
            }

            if (this.validationMessages[property].Any(msg => msg.Message.Equals(message)))
            {
                // Remove the error from the key's collection.
                this.validationMessages[property].Remove(this.validationMessages[property].FirstOrDefault(msg => msg.Message.Equals(message)));
            }
        }

        /// <summary>
        /// Removes the validation messages.
        /// </summary>
        /// <param name="property">The property.</param>
        public void RemoveValidationMessages(string property = "")
        {
            if (string.IsNullOrEmpty(property))
            {
                return;
            }

            if (!this.validationMessages.ContainsKey(property))
            {
                return;
            }

            this.validationMessages[property].Clear();
            this.validationMessages.Remove(property);
        }

        /// <summary>
        /// Validates the property.
        /// </summary>
        /// <param name="validationDelegate">The validation delegate.</param>
        /// <param name="failureMessage">The failure message.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>An instance of IValidationMessage</returns>
        public IValidationMessage ValidateProperty(Func<string, IValidationMessage> validationDelegate, string failureMessage, string propertyName = "")
        {
            IValidationMessage result = validationDelegate(failureMessage);
            if (result != null)
            {
                this.AddValidationMessage(result, propertyName);
            }
            else
            {
                this.RemoveValidationMessage(failureMessage, propertyName);
            }

            return result;
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public abstract void Validate();
    }
}
