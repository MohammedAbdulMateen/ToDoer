namespace ToDoer.Commands
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// The command that can be bound to the view using the MVVM pattern.
    /// Implements the ICommand interface.
    /// </summary>
    public class SimpleRelayCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the handler.
        /// </summary>
        /// <value>
        /// The handler.
        /// </value>
        private Action<object> handler { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleRelayCommand"/> class.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public SimpleRelayCommand(Action<object> handler)
        {
            this.handler = handler;
        }

        /// <summary>
        /// Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Occurs when [can execute changed].
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Executes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void Execute(object parameter)
        {
            this.handler(parameter);
        }

        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
