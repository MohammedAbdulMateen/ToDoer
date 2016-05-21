namespace ToDoer.ViewModels
{
    using GalaSoft.MvvmLight.Views;
    using System.Windows.Input;
    using ToDoer.Commands;
    using ToDoer.Common;
    using ToDoer.Interfaces;
    using ToDoer.Models;
    using ToDoer.Data;
#if WINDOWS_PHONE_APP
    using Windows.Phone.UI.Input;
#endif

    /// <summary>
    /// The view model for AddContext.xaml
    /// </summary>
    public class NewContextViewModel : VMBase, INavigable
    {
        #region Fields

        /// <summary>
        /// The navigation service
        /// </summary>
        private INavigationService navigationService;

        /// <summary>
        /// The context repository
        /// </summary>
        private IContextRepository contextRepository;

        /// <summary>
        /// The context
        /// </summary>
        private ContextModel context;

        /// <summary>
        /// The is context name focused
        /// </summary>
        private bool isContextNameFocused;

        /// <summary>
        /// The save context
        /// </summary>
        private ICommand saveContext;

        /// <summary>
        /// The loaded
        /// </summary>
        private ICommand loaded;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NewContextViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="contextRepository">The context repository.</param>
        public NewContextViewModel(INavigationService navigationService, IContextRepository contextRepository)
        {
            this.navigationService = navigationService;
            this.contextRepository = contextRepository;
            this.Context = new ContextModel();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public ContextModel Context
        {
            get
            {
                return this.context;
            }
            set
            {
                if (value == this.context)
                {
                    return;
                }

                this.context = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is context name focused.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is context name focused; otherwise, <c>false</c>.
        /// </value>
        public bool IsContextNameFocused
        {
            get
            {
                return this.isContextNameFocused;
            }
            set
            {
                if (value == this.isContextNameFocused)
                {
                    return;
                }

                this.isContextNameFocused = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the save context.
        /// </summary>
        /// <value>
        /// The save context.
        /// </value>
        public ICommand SaveContext
        {
            get
            {
                if (this.saveContext == null)
                {
                    this.saveContext = new SimpleRelayCommand(this.OnSaveContext);
                }

                return this.saveContext;
            }
        }

        /// <summary>
        /// Gets the loaded.
        /// </summary>
        /// <value>
        /// The loaded.
        /// </value>
        public ICommand Loaded
        {
            get
            {
                if (this.loaded == null)
                {
                    this.loaded = new SimpleRelayCommand(this.OnLoaded);
                }

                return this.loaded;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Activates the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Activate(object parameter)
        {
        }

        /// <summary>
        /// Deactivates the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Deactivate(object parameter)
        {
        }

#if WINDOWS_PHONE_APP
        /// <summary>
        /// Backs the button pressed.
        /// </summary>
        /// <param name="e">The <see cref="Windows.Phone.UI.Input.BackPressedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void BackButtonPressed(BackPressedEventArgs e)
        {
            e.Handled = true;
            this.navigationService.GoBack();
        }
#endif

        /// <summary>
        /// Called when [save context].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public async void OnSaveContext(object parameter)
        {
            ContextModel context = null;
            if (this.Context.Id == 0)
            {
                context = await this.contextRepository.AddContext(this.Context);
            }
            else
            {
                context = await this.contextRepository.UpdateContext(this.Context);
            }

            this.navigationService.NavigateTo(Constants.MainPage, context);
        }

        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void OnLoaded(object parameter)
        {
            this.IsContextNameFocused = true;
        }

        #endregion
    }
}
