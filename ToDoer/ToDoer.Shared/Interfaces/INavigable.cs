namespace ToDoer.Interfaces
{
#if WINDOWS_PHONE_APP
    using Windows.Phone.UI.Input;
#endif

    /// <summary>
    /// Defines the methods that every ViewModel have to handle the load and unload events.
    /// </summary>
    public interface INavigable
    {
        /// <summary>
        /// Activates the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        void Activate(object parameter);

        /// <summary>
        /// Deactivates the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        void Deactivate(object parameter);

#if WINDOWS_PHONE_APP
        /// <summary>
        /// Backs the button pressed.
        /// </summary>
        /// <param name="e">The <see cref="BackPressedEventArgs"/> instance containing the event data.</param>
        void BackButtonPressed(BackPressedEventArgs e);
#endif
    }
}
