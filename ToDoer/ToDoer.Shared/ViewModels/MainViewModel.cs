namespace ToDoer.ViewModels
{
    using GalaSoft.MvvmLight.Views;
    using System.Collections.Generic;
    using System.Windows.Input;
    using ToDoer.Commands;
    using ToDoer.Common;
    using ToDoer.Interfaces;
    using ToDoer.Models;
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
        /// The contexts
        /// </summary>
        private List<ContextModel> contexts;

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
        public MainViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.Contexts = new List<ContextModel>();
            var defaultContexts = this.InitDefaultContexts();
            this.Contexts.AddRange(defaultContexts);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the contexts.
        /// </summary>
        /// <value>
        /// The contexts.
        /// </value>
        public List<ContextModel> Contexts
        {
            get
            {
                return this.contexts;
            }
            set
            {
                if (value == this.contexts)
                {
                    return;
                }

                this.contexts = value;
                this.NotifyPropertyChanged();
            }
        }

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

        #region Methods

        /// <summary>
        /// Activates the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void Activate(object parameter)
        {
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

        /// <summary>
        /// Initializes the default contexts.
        /// </summary>
        /// <returns></returns>
        private List<ContextModel> InitDefaultContexts()
        {
            var contexts = new List<ContextModel>
            {
                new ContextModel { Id = 1, Name = Constants.Inbox },
                new ContextModel { Id = 2, Name = Constants.Today },
                new ContextModel { Id = 3, Name = Constants.Tomorrow },
                new ContextModel { Id = 4, Name = Constants.Week }
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
