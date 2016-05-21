namespace ToDoer.ViewModels
{
    using GalaSoft.MvvmLight.Views;
    using ToDoer.Common;
    using ToDoer.Interfaces;
    using ToDoer.Models;
    using System.Windows.Input;
    using ToDoer.Commands;
#if WINDOWS_PHONE_APP
    using Windows.Phone.UI.Input;
#endif

    /// <summary>
    /// The view model for Todo.xaml
    /// </summary>
    public class NewTaskViewModel : VMBase, INavigable
    {
        #region Fields

        /// <summary>
        /// The navigation service
        /// </summary>
        private INavigationService navigationService;

        /// <summary>
        /// The is todo name focused
        /// </summary>
        private bool isTodoNameFocused;

        /// <summary>
        /// The task
        /// </summary>
        private TaskModel task;

        /// <summary>
        /// The loaded
        /// </summary>
        private ICommand loaded;

        /// <summary>
        /// The save task
        /// </summary>
        private ICommand saveTask;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NewTaskViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        public NewTaskViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.Task = new TaskModel();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is todo name focused.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is todo name focused; otherwise, <c>false</c>.
        /// </value>
        public bool IsTodoNameFocused
        {
            get
            {
                return this.isTodoNameFocused;
            }
            set
            {
                if (value == this.isTodoNameFocused)
                {
                    return;
                }

                this.isTodoNameFocused = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the task.
        /// </summary>
        /// <value>
        /// The task.
        /// </value>
        public TaskModel Task
        {
            get
            {
                return this.task;
            }
            set
            {
                if (value == this.task)
                {
                    return;
                }

                this.task = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the loaded.
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

        /// <summary>
        /// Gets the save task.
        /// </summary>
        /// <value>
        /// The save task.
        /// </value>
        public ICommand SaveTask
        {
            get
            {
                if (this.saveTask == null)
                {
                    this.saveTask = new SimpleRelayCommand(this.OnSaveTask);
                }

                return this.saveTask;
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
            var contextModel = parameter as ContextModel;
            if (contextModel != null)
            {
                this.Task.ContextId = contextModel.Id;
                this.Task.Context = contextModel.Name;
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
        /// The back button pressed.
        /// </summary>
        /// <param name="e">The BackPressedEventArgs.</param>
        public void BackButtonPressed(BackPressedEventArgs e)
        {
            e.Handled = true;
            this.navigationService.GoBack();
        }
#endif

        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void OnLoaded(object parameter)
        {
            this.IsTodoNameFocused = true;
        }

        /// <summary>
        /// Called when [save task].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void OnSaveTask(object parameter)
        {
        }

        #endregion
    }
}
