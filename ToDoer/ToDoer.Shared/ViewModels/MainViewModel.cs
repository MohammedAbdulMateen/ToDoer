namespace ToDoer.ViewModels
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Views;
    using PropertyChanged;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using ToDoer.Commands;
    using ToDoer.Common;
    using ToDoer.Data;
    using ToDoer.Interfaces;
    using ToDoer.Models;
#if WINDOWS_PHONE_APP
    using Windows.Phone.UI.Input;
#endif

    /// <summary>
    /// The view model for MainPage.xaml.
    /// </summary>
    public class MainViewModel : ViewModelBase, INavigable
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
        /// The _context selection changed
        /// </summary>
        private ICommand _contextSelectionChanged;

        /// <summary>
        /// The _add context
        /// </summary>
        private ICommand _addContext;

        /// <summary>
        /// The _selected context
        /// </summary>
        private ContextModel _selectedContext;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="contextRepository">The context repository.</param>
        public MainViewModel(INavigationService navigationService, IContextRepository contextRepository)
        {
            this._navigationService = navigationService;
            this._contextRepository = contextRepository;
            this.Contexts = new ObservableCollection<ContextModel>();
            this._initContexts();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the contexts.
        /// </summary>
        /// <value>
        /// The contexts.
        /// </value>
        public ObservableCollection<ContextModel> Contexts { get; set; }

        /// <summary>
        /// Gets or sets the selected context.
        /// </summary>
        /// <value>
        /// The selected context.
        /// </value>
        public ContextModel SelectedContext
        {
            get
            {
                return this._selectedContext;
            }
            set
            {
                this.Set(ref this._selectedContext, value);
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the context selection changed.
        /// </summary>
        /// <value>
        /// The context selection changed.
        /// </value>
        [DoNotNotify]
        public ICommand ContextSelectionChanged
        {
            get
            {
                if (this._contextSelectionChanged == null)
                {
                    this._contextSelectionChanged = new SimpleRelayCommand(this._onContextSelectionChanged);
                }

                return this._contextSelectionChanged;
            }
        }

        /// <summary>
        /// Gets the add context.
        /// </summary>
        /// <value>
        /// The add context.
        /// </value>
        [DoNotNotify]
        public ICommand AddContext
        {
            get
            {
                if (this._addContext == null)
                {
                    this._addContext = new SimpleRelayCommand(this._onAddContext);
                }

                return this._addContext;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Activates the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void Activate(object parameter)
        {
            var context = parameter as ContextModel;
            if (context == null)
            {
                return;
            }

            var item = this.Contexts.SingleOrDefault(x => x.Id == context.Id);
            if (item == null)
            {
                this.Contexts.Add(context);
            }
            else
            {
                item.Name = context.Name;
            }
        }

        /// <summary>
        /// Deactivates the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void Deactivate(object parameter)
        {
        }

#if WINDOWS_PHONE_APP
        /// <summary>
        /// Backs the button pressed.
        /// </summary>
        /// <param name="e">The <see cref="Windows.Phone.UI.Input.BackPressedEventArgs"/> instance containing the event data.</param>
        public void BackButtonPressed(BackPressedEventArgs e)
        {
            // e.Handled = true;
        }
#endif

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the contexts.
        /// </summary>
        private async void _initContexts()
        {
            var defaultContexts = this._initDefaultContexts();
            var contexts = await this._contextRepository.GetContextsAsync();
            defaultContexts.AddRange(contexts);
            for (int i = 0; i < defaultContexts.Count; i++)
            {
                this.Contexts.Add(defaultContexts[i]);
            }
        }

        /// <summary>
        /// Initializes the default contexts.
        /// </summary>
        /// <returns></returns>
        private List<ContextModel> _initDefaultContexts()
        {
            var inbox = LocalizationService.GetLocalizedMessage(Constants.Inbox);
            var today = LocalizationService.GetLocalizedMessage(Constants.Today);
            var tomorrow = LocalizationService.GetLocalizedMessage(Constants.Tomorrow);
            var week = LocalizationService.GetLocalizedMessage(Constants.Week);
            var contexts = new List<ContextModel>
            {
                new ContextModel { Id = Constants.DefaultContextId, Name = inbox },
                new ContextModel { Id = Constants.DefaultContextId, Name = today },
                new ContextModel { Id = Constants.DefaultContextId, Name = tomorrow },
                new ContextModel { Id = Constants.DefaultContextId, Name = week }
            };

            return contexts;
        }

        /// <summary>
        /// Called when [context selection changed].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void _onContextSelectionChanged(object parameter)
        {
            if (this.SelectedContext == null)
            {
                return;
            }

            this._navigationService.NavigateTo(Constants.Task, this.SelectedContext);
            this.SelectedContext = null;
        }

        /// <summary>
        /// Called when [add context].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void _onAddContext(object parameter)
        {
            this._navigationService.NavigateTo(Constants.AddContext);
        }

        #endregion
    }
}
