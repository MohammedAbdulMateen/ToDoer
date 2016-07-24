namespace ToDoer.ViewModels
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Views;
    using PropertyChanged;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using ToDoer.Commands;
    using ToDoer.Common;
    using ToDoer.Data;
    using ToDoer.Interfaces;
    using ToDoer.Models;
#if WINDOWS_PHONE_APP
    using Windows.Phone.UI.Input;
#endif
    using Windows.UI.Popups;

    /// <summary>
    /// The view model for Task.xaml user control.
    /// </summary>
    public class TaskViewModel : ViewModelBase, INavigable
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
        /// The _current context
        /// </summary>
        private ContextModel _currentContext;

        /// <summary>
        /// The _selected task
        /// </summary>
        private TaskModel _selectedTask;

        /// <summary>
        /// The _add task
        /// </summary>
        private ICommand _addTask;

        /// <summary>
        /// The _task selection changed
        /// </summary>
        private ICommand _taskSelectionChanged;

        /// <summary>
        /// The _delete task
        /// </summary>
        private ICommand _deleteTask;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="taskRepository">The task repository.</param>
        public TaskViewModel(INavigationService navigationService, ITaskRepository taskRepository)
        {
            this._navigationService = navigationService;
            this._taskRepository = taskRepository;
            this.Tasks = new ObservableCollection<TaskModel>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the tasks.
        /// </summary>
        /// <value>
        /// The tasks.
        /// </value>
        public ObservableCollection<TaskModel> Tasks { get; set; }

        /// <summary>
        /// Gets or sets the selected task.
        /// </summary>
        /// <value>
        /// The selected task.
        /// </value>
        public TaskModel SelectedTask
        {
            get
            {
                return this._selectedTask;
            }
            set
            {
                this.Set(ref this._selectedTask, value);
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the add task.
        /// </summary>
        /// <value>
        /// The add task.
        /// </value>
        [DoNotNotify]
        public ICommand AddTask
        {
            get
            {
                if (this._addTask == null)
                {
                    this._addTask = new SimpleRelayCommand(this._onAddTask);
                }

                return this._addTask;
            }
        }

        /// <summary>
        /// Gets the task selection changed.
        /// </summary>
        /// <value>
        /// The task selection changed.
        /// </value>
        [DoNotNotify]
        public ICommand TaskSelectionChanged
        {
            get
            {
                if (this._taskSelectionChanged == null)
                {
                    this._taskSelectionChanged = new SimpleRelayCommand(this._onTaskSelectionChanged);
                }

                return this._taskSelectionChanged;
            }
        }

        /// <summary>
        /// Gets the delete task.
        /// </summary>
        /// <value>
        /// The delete task.
        /// </value>
        [DoNotNotify]
        public ICommand DeleteTask
        {
            get
            {
                if (this._deleteTask == null)
                {
                    this._deleteTask = new SimpleRelayCommand(this._onDeleteTask);
                }

                return this._deleteTask;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Activates the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public async void Activate(object parameter)
        {
            this._currentContext = parameter as ContextModel;
            if (this._currentContext == null)
            {
                var task = parameter as TaskModel;
                if (task != null)
                {
                    this._addOrUpdateTask(task);
                }

                return;
            }

            await this._initTasks();
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
            e.Handled = true;
            // this.navigationService.GoBack();
            this._navigationService.NavigateTo(Constants.MainPage);
        }
#endif

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the tasks.
        /// </summary>
        /// <returns></returns>
        private async Task _initTasks()
        {
            List<TaskModel> tasks = null;
            this.Tasks.Clear();
            if (this._currentContext.Id == Constants.DefaultContextId)
            {
                tasks = await this._getTasksAsync(this._currentContext.Name);
            }
            else
            {
                tasks = await this._getTasksAsync(this._currentContext.Id);
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                this.Tasks.Add(tasks[i]);
            }
        }

        /// <summary>
        /// Called when [add task].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void _onAddTask(object parameter)
        {
            this._navigationService.NavigateTo(Constants.AddTask, this._currentContext);
        }

        /// <summary>
        /// The _get tasks asynchronous.
        /// </summary>
        /// <param name="contextId">The context identifier.</param>
        /// <returns>
        /// A list with the element type of TaskModel <see cref="TaskModel.cs" />
        /// </returns>
        private async Task<List<TaskModel>> _getTasksAsync(int contextId)
        {
            var tasks = await this._taskRepository.GetTasksAsync(contextId);

            return tasks;
        }

        /// <summary>
        /// The _get tasks asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A list with the element type of TaskModel <see cref="TaskModel.cs" /></returns>
        private async Task<List<TaskModel>> _getTasksAsync(string context)
        {
            DateTimeOffset? startDate = null, endDate = null;
            switch (context)
            {
                case Constants.Today:
                    startDate = DateTimeOffset.Now.Date;
                    break;
                case Constants.Tomorrow:
                    startDate = DateTimeOffset.Now.Date.AddDays(1);
                    break;
                case Constants.Week:
                    startDate = DateTimeOffset.Now.Date;
                    endDate = DateTimeOffset.Now.Date.AddDays(7);
                    break;
            }

            var tasks = await this._taskRepository.GetTasksAsync(startDate, endDate);

            return tasks;
        }

        /// <summary>
        /// Add or update task.
        /// </summary>
        /// <param name="task">The task.</param>
        private void _addOrUpdateTask(TaskModel task)
        {
            var item = this.Tasks.SingleOrDefault(x => x.Id == task.Id);
            if (item == null)
            {
                this.Tasks.Add(task);
            }

            /*
             * Reason: The item is synchronized by the WinRT (through usage of Caching as well)
            else
            {
                item.Todo = task.Todo;
                item.DueDate = task.DueDate;
                item.DueTime = task.DueTime;
                item.ReminderDate = task.ReminderDate;
                item.ReminderTime = task.ReminderTime;
            }
            */
        }

        /// <summary>
        /// The _on task selection changed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void _onTaskSelectionChanged(object parameter)
        {
            if (this.SelectedTask == null)
            {
                return;
            }

            this._navigationService.NavigateTo(Constants.AddTask, this.SelectedTask);
            this.SelectedTask = null;
        }

        /// <summary>
        /// The _on delete task.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private async void _onDeleteTask(object parameter)
        {
            var task = parameter as TaskModel;
            if (task == null)
            {
                return;
            }

            var dialog = new MessageDialog(LocalizationService.GetLocalizedMessage(Constants.DeletePrompt), task.Todo);

            var delete = new UICommand
            {
                Id = task.Id,
                Invoked = _onDeleteTaskConfirmed,
                Label = LocalizationService.GetLocalizedMessage(Constants.Okay)
            };

            var cancel = new UICommand
            {
                Id = task.Id,
                Invoked = _onDeleteTaskAborted,
                Label = LocalizationService.GetLocalizedMessage(Constants.Cancel)
            };

            dialog.Commands.Add(delete);
            dialog.Commands.Add(cancel);

            await dialog.ShowAsync();
        }

        /// <summary>
        /// The _on delete task aborted.
        /// </summary>
        /// <param name="command">The command.</param>
        private void _onDeleteTaskAborted(IUICommand command)
        {
        }

        /// <summary>
        /// The _on delete task confirmed.
        /// </summary>
        /// <param name="command">The command.</param>
        private async void _onDeleteTaskConfirmed(IUICommand command)
        {
            var taskId = Convert.ToInt32(command.Id);
            await this._taskRepository.DeleteTaskAsync(taskId);
            var item = this.Tasks.SingleOrDefault(x => x.Id == taskId);
            this.Tasks.Remove(item);
        }

        #endregion
    }
}
