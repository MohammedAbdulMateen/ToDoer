namespace ToDoer.ViewModels
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Views;
    using PropertyChanged;
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
    /// The view model for Todo.xaml
    /// </summary>
    public class NewTaskViewModel : ViewModelBase, INavigable
    {
        #region Fields

        /// <summary>
        /// The _navigation service
        /// </summary>
        private INavigationService _navigationService;

        /// <summary>
        /// The _task repository
        /// </summary>
        private ITaskRepository _taskRepository;

        /// <summary>
        /// The _is todo name focused
        /// </summary>
        private bool _isTodoNameFocused;

        /// <summary>
        /// The _task
        /// </summary>
        private TaskModel _task;

        /// <summary>
        /// The _loaded
        /// </summary>
        private ICommand _loaded;

        /// <summary>
        /// The _save task
        /// </summary>
        private ICommand _saveTask;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NewTaskViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="taskRepository">The task repository.</param>
        public NewTaskViewModel(INavigationService navigationService, ITaskRepository taskRepository)
        {
            this._navigationService = navigationService;
            this._taskRepository = taskRepository;
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
                return this._isTodoNameFocused;
            }
            set
            {
                this.Set(ref this._isTodoNameFocused, value);
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
                return this._task;
            }
            set
            {
                this.Set(ref this._task, value);
            }
        }

        /// <summary>
        /// Gets or sets the loaded.
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

        /// <summary>
        /// Gets the save task.
        /// </summary>
        /// <value>
        /// The save task.
        /// </value>
        [DoNotNotify]
        public ICommand SaveTask
        {
            get
            {
                if (this._saveTask == null)
                {
                    this._saveTask = new SimpleRelayCommand(this._onSaveTask);
                }

                return this._saveTask;
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
            if (contextModel != null && contextModel.Id != Constants.DefaultContextId)
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
            this._navigationService.GoBack();
        }
#endif

        #endregion

        #region Private Methods

        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void _onLoaded(object parameter)
        {
            this.IsTodoNameFocused = true;
        }

        /// <summary>
        /// Called when [save task].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>An instance of Task <see cref="System.Threading.Tasks.Task.cs"/></returns>
        private async void _onSaveTask(object parameter)
        {
            if (this.Task.IsValid)
            {
                TaskModel task = null;
                if (this.Task.Id == 0)
                {
                    task = await this._taskRepository.AddTaskAsync(this.Task);
                }
                else
                {
                    task = await this._taskRepository.UpdateTaskAsync(this.Task);
                }

                this._resetTask();
                this._navigationService.NavigateTo(Constants.Task, task);
            }
        }

        private void _resetTask()
        {
            this.Task.Todo = null;
            this.Task.DueDate = null;
            this.Task.DueTime = null;
            this.Task.ReminderDate = null;
            this._task.ReminderTime = null;
        }

        #endregion
    }
}
