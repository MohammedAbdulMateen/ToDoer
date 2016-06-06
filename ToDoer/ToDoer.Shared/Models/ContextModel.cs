namespace ToDoer.Models
{
    using PropertyChanged;

    /// <summary>
    /// The context model.
    /// </summary>
    [ImplementPropertyChanged]
    public class ContextModel
    {
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
                return !string.IsNullOrWhiteSpace(this.Name);
            }
        }
    }
}
