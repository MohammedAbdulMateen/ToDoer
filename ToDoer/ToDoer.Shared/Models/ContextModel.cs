namespace ToDoer.Models
{
    using PropertyChanged;
    using ToDoer.Common;

    /// <summary>
    /// The context model.
    /// </summary>
    [ImplementPropertyChanged]
    public class ContextModel : ValidationBase
    {
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
        /// Validates the self.
        /// </summary>
        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                this.ValidationErrors[Constants.ContextNameProperty] = Constants.ContextNameEmptyError;
            }
        }

        #endregion
    }
}
