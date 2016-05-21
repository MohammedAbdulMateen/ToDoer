namespace ToDoer.Pages
{
    using GalaSoft.MvvmLight.Ioc;
    using ToDoer.Interfaces;
    using ToDoer.ViewModels;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;
#if WINDOWS_PHONE_APP
    using Windows.Phone.UI.Input;
#endif

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddTask : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddTask"/> class.
        /// </summary>
        public AddTask()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.DataContext = SimpleIoc.Default.GetInstance<NewTaskViewModel>();
        }

        /// <summary>
        /// Raises the <see cref="E:NavigatedTo" /> event.
        /// </summary>
        /// <param name="e">The <see cref="NavigationEventArgs"/> instance containing the event data.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var navigableViewModel = this.DataContext as INavigable;
            if (navigableViewModel != null)
            {
                navigableViewModel.Activate(e.Parameter);
            }

#if WINDOWS_PHONE_APP
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
#endif
        }

        /// <summary>
        /// Raises the <see cref="E:NavigatedFrom" /> event.
        /// </summary>
        /// <param name="e">The <see cref="NavigationEventArgs"/> instance containing the event data.</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            var navigableViewModel = this.DataContext as INavigable;
            if (navigableViewModel != null)
            {
                navigableViewModel.Deactivate(e.Parameter);
            }

#if WINDOWS_PHONE_APP
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
#endif
        }

#if WINDOWS_PHONE_APP
        /// <summary>
        /// Handles the BackPressed event of the HardwareButtons control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BackPressedEventArgs"/> instance containing the event data.</param>
        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            var navigableViewModel = this.DataContext as INavigable;
            if (navigableViewModel != null)
            {
                navigableViewModel.BackButtonPressed(e);
            }
        }
#endif
    }
}
