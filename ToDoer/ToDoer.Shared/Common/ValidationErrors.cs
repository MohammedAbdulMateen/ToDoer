namespace ToDoer.Common
{
    using PropertyChanged;
    using System.Collections.Generic;

    /// <summary>
    /// The Validation Errors
    /// </summary>
    [ImplementPropertyChanged]
    public class ValidationErrors
    {
        /// <summary>
        /// The validation errors
        /// </summary>
        private readonly Dictionary<string, string> validationErrors = new Dictionary<string, string>();

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get
            {
                return this.validationErrors.Count < 1;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="System.String"/> with the specified field name.
        /// </summary>
        /// <value>
        /// The <see cref="System.String"/>.
        /// </value>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>The error message for the fieldName if any otherwise an empty string.</returns>
        public string this[string fieldName]
        {
            get
            {
                return this.validationErrors.ContainsKey(fieldName) ? this.validationErrors[fieldName] : string.Empty;
            }
            set
            {
                if (this.validationErrors.ContainsKey(fieldName))
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        this.validationErrors.Remove(fieldName);
                    }
                    else
                    {
                        this.validationErrors[fieldName] = value;
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        this.validationErrors.Add(fieldName, value);
                    }
                }
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this.validationErrors.Clear();
        }
    }
}
