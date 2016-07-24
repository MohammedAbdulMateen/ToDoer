namespace ToDoer.Models
{
    using PropertyChanged;
    using ToDoer.Common;
    using ToDoer.Interfaces;

    /// <summary>
    /// The context model.
    /// </summary>
    [ImplementPropertyChanged]
    public class ContextModel : ValidatableBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextModel"/> class.
        /// </summary>
        public ContextModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextModel"/> class.
        /// </summary>
        /// <param name="forceValidation">if set to <c>true</c> [force validation].</param>
        public ContextModel(bool forceValidation)
        {
            // TODO: Correct the approach of using dictionary for validation.
            // Temporary Fix: Setting empty error message instance to avoid key not found exception when a new instance is created.
            base.AddValidationMessage(new ValidationErrorMessage(), Constants.ContextNameProperty);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DoNotNotify]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public override void Validate()
        {
            this.ValidateName(Constants.ContextNameProperty);

            // Passing in an empty string will cause the ValidatableBase indexer to be hit.
            // This will let the UI refresh it's error bindings.
            base.RaisePropertyChanged(string.Empty);
        }

        /// <summary>
        /// Validates the name.
        /// </summary>
        /// <param name="property">The property.</param>
        public void ValidateName(string property)
        {
            base.RemoveValidationMessages(property);
            if (string.IsNullOrEmpty(this.Name))
            {
                base.AddValidationMessage(new ValidationErrorMessage(Constants.ContextNameEmptyError), property);
            }
            else
            {
                base.AddValidationMessage(new ValidationErrorMessage(), property);
            }
        }

        #endregion
    }
}
