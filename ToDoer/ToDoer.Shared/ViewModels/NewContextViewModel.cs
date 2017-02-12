namespace ToDoer.ViewModels
{
    using GalaSoft.MvvmLight.Views;
    using System.Windows.Input;
    using ToDoer.Commands;
    using ToDoer.Common;
    using ToDoer.Interfaces;
    using ToDoer.Models;
    using ToDoer.Data;
    using GalaSoft.MvvmLight;
    using PropertyChanged;
#if WINDOWS_PHONE_APP
    using Windows.Phone.UI.Input;
#endif

    /// <summary>
    /// The view model for AddContext.xaml
    /// </summary>
    public class NewContextViewModel : ViewModelBase, INavigable
    {
        #region Fields

        /// <summary>
        /// The _navigation service
        /// </summary>
        private INavigationService _navigationService;

        /// <summary>
        /// The _context repository
        /// </summary>
        private IContextRepository _contextRepository;

        /// <summary>
        /// The _context
        /// </summary>
        private ContextModel _context;

        /// <summary>
        /// The _is context name focused
        /// </summary>
        private bool _isContextNameFocused;

        /// <summary>
        /// The _save context
        /// </summary>
        private ICommand _saveContext;

        /// <summary>
        /// The _loaded
        /// </summary>
        private ICommand _loaded;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NewContextViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="contextRepository">The context repository.</param>
        public NewContextViewModel(INavigationService navigationService, IContextRepository contextRepository)
        {
            this._navigationService = navigationService;
            this._contextRepository = contextRepository;
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
                return this._context;
            }
            set
            {
                this.Set(ref this._context, value);
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
                return this._isContextNameFocused;
            }
            set
            {
                this.Set(ref this._isContextNameFocused, value);
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the save context.
        /// </summary>
        /// <value>
        /// The save context.
        /// </value>
        [DoNotNotify]
        public ICommand SaveContext
        {
            get
            {
                if (this._saveContext == null)
                {
                    this._saveContext = new SimpleRelayCommand(this.OnSaveContext);
                }

                return this._saveContext;
            }
        }

        /// <summary>
        /// Gets the loaded.
        /// </summary>
        /// <value>
        /// The loaded.
        /// </value>
        [DoNotNotify]
        public ICommand Loaded
        {
            get
            {
                if (this._loaded == null)
                {
                    this._loaded = new SimpleRelayCommand(this._onLoaded);
                }

                return this._loaded;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Activates the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Activate(object parameter)
        {
            var context = parameter as ContextModel;
            if (context == null)
            {
                this.Context = new ContextModel();
            }
            else
            {
                this.Context = context;
            }

            this.IsContextNameFocused = false;
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
            this._navigationService.GoBack();
        }
#endif

        /// <summary>
        /// Called when [save context].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public async void OnSaveContext(object parameter)
        {
            this.Context.Validate();
            if (!this.Context.IsValid)
            {
                return;
            }

            ContextModel context = null;
            if (this.Context.Id == 0)
            {
                context = await this._contextRepository.AddContextAsync(this.Context);
            }
            else
            {
                context = await this._contextRepository.UpdateContextAsync(this.Context);
            }

            this._navigationService.NavigateTo(Constants.MainPage, context);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void _onLoaded(object parameter)
        {
            this.IsContextNameFocused = true;
        }

        #endregion
    }
}
