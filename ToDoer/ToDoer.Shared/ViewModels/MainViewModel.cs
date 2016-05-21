namespace ToDoer.ViewModels
{
    using System.Linq;
    using GalaSoft.MvvmLight.Views;
    using System.Collections.Generic;
    using System.Windows.Input;
    using ToDoer.Commands;
    using ToDoer.Common;
    using ToDoer.Interfaces;
    using ToDoer.Models;
    using ToDoer.Data;
    using System.Collections.ObjectModel;
#if WINDOWS_PHONE_APP
    using Windows.Phone.UI.Input;
#endif

    /// <summary>
    /// The view model for MainPage.xaml.
    /// </summary>
    public class MainViewModel : VMBase, INavigable
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
        /// The context selection changed
        /// </summary>
        private ICommand contextSelectionChanged;

        /// <summary>
        /// The add context
        /// </summary>
        private ICommand addContext;

        /// <summary>
        /// The selected context
        /// </summary>
        private ContextModel selectedContext;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="contextRepository">The context repository.</param>
        public MainViewModel(INavigationService navigationService, IContextRepository contextRepository)
        {
            this.navigationService = navigationService;
            this.contextRepository = contextRepository;
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
        /// Gets the context selection changed.
        /// </summary>
        /// <value>
        /// The context selection changed.
        /// </value>
        public ICommand ContextSelectionChanged
        {
            get
            {
                if (this.contextSelectionChanged == null)
                {
                    this.contextSelectionChanged = new SimpleRelayCommand(this.OnContextSelectionChanged);
                }

                return this.contextSelectionChanged;
            }
        }

        /// <summary>
        /// Gets the add context.
        /// </summary>
        /// <value>
        /// The add context.
        /// </value>
        public ICommand AddContext
        {
            get
            {
                if (this.addContext == null)
                {
                    this.addContext = new SimpleRelayCommand(this.OnAddContext);
                }

                return this.addContext;
            }
        }

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
                return this.selectedContext;
            }
            set
            {
                if (value == this.selectedContext)
                {
                    return;
                }

                this.selectedContext = value;
                this.NotifyPropertyChanged();
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
            if (context != null)
            {
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

        private async void _initContexts()
        {
            var defaultContexts = this.InitDefaultContexts();
            var contexts = await this.contextRepository.GetContexts();
            // this.Contexts.AddRange(defaultContexts);
            // this.Contexts.AddRange(contexts);
            for (int i = 0; i < defaultContexts.Count; i++)
            {
                this.Contexts.Add(defaultContexts[i]);
            }

            for (int i = 0; i < contexts.Count; i++)
            {
                this.Contexts.Add(contexts[i]);
            }
        }

        /// <summary>
        /// Initializes the default contexts.
        /// </summary>
        /// <returns></returns>
        private List<ContextModel> InitDefaultContexts()
        {
            var contexts = new List<ContextModel>
            {
                new ContextModel { Id = Constants.DefaultContextId, Name = Constants.Inbox },
                new ContextModel { Id = Constants.DefaultContextId, Name = Constants.Today },
                new ContextModel { Id = Constants.DefaultContextId, Name = Constants.Tomorrow },
                new ContextModel { Id = Constants.DefaultContextId, Name = Constants.Week }
            };

            return contexts;
        }

        /// <summary>
        /// Called when [context selection changed].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void OnContextSelectionChanged(object parameter)
        {
            if (this.SelectedContext == null)
            {
                return;
            }

            this.navigationService.NavigateTo(Constants.Task, this.SelectedContext);
            this.SelectedContext = null;
        }

        /// <summary>
        /// Called when [add context].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void OnAddContext(object parameter)
        {
            this.navigationService.NavigateTo(Constants.AddContext);
        }

        #endregion
    }
}
