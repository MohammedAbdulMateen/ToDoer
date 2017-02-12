namespace ToDoer.Common
{
    using PropertyChanged;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// The Validation Base
    /// </summary>
    [ImplementPropertyChanged]
    public abstract class ValidationBase : VMBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationBase"/> class.
        /// </summary>
        protected ValidationBase()
        {
            this.ValidationErrors = new ValidationErrors();
        }

        /// <summary>
        /// Gets or sets the validation errors.
        /// </summary>
        /// <value>
        /// The validation errors.
        /// </value>
        public ValidationErrors ValidationErrors { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid { get; private set; }

        /// <summary>
        /// The Validates Self to be overriden in the derived class.
        /// </summary>
        protected abstract void ValidateSelf();

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public void Validate()
        {
            this.ValidationErrors.Clear();
            this.ValidateSelf();
            this.IsValid = this.ValidationErrors.IsValid;
            this.NotifyPropertyChanged(Constants.IsValid);
            this.NotifyPropertyChanged(Constants.ValidationErrors);
        }
    }
}
