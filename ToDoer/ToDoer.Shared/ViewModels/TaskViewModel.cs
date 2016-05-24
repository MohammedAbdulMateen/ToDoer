namespace ToDoer.ViewModels
{
    using GalaSoft.MvvmLight.Views;
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

    /// <summary>
    /// The view model for Task.xaml user control.
    /// </summary>
    public class TaskViewModel : VMBase, INavigable
    {
        #region Fields

        /// <summary>
        /// The navigation service
        /// </summary>
        private INavigationService navigationService;

        /// <summary>
        /// The task repository
        /// </summary>
        private ITaskRepository taskRepository;

        /// <summary>
        /// The current context
        /// </summary>
        private ContextModel currentContext;

        /// <summary>
        /// The add task
        /// </summary>
        private ICommand addTask;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="taskRepository">The task repository.</param>
        public TaskViewModel(INavigationService navigationService, ITaskRepository taskRepository)
        {
            this.navigationService = navigationService;
            this.taskRepository = taskRepository;
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

        public ICommand AddTask
        {
            get
            {
                if (this.addTask == null)
                {
                    this.addTask = new SimpleRelayCommand(this.OnAddTask);
                }

                return this.addTask;
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
            this.currentContext = parameter as ContextModel;
            if (this.currentContext == null)
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
            this.navigationService.NavigateTo(Constants.MainPage);
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
            if (this.currentContext.Id == Constants.DefaultContextId)
            {
                tasks = await this._getTasksAsync(this.currentContext.Name);
            }
            else
            {
                tasks = await this._getTasksAsync(this.currentContext.Id);
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
        private void OnAddTask(object parameter)
        {
            this.navigationService.NavigateTo(Constants.AddTask, this.currentContext);
        }

        /// <summary>
        /// _gets the tasks asynchronous.
        /// </summary>
        /// <param name="contextId">The context identifier.</param>
        /// <returns>
        /// A list with the element type of TaskModel <see cref="TaskModel.cs" />
        /// </returns>
        private async Task<List<TaskModel>> _getTasksAsync(int contextId)
        {
            var tasks = await this.taskRepository.GetTasksAsync(contextId);

            return tasks;
        }

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

            var tasks = await this.taskRepository.GetTasksAsync(startDate, endDate);

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
            else
            {
                item.Todo = task.Todo;
                item.DueDate = task.DueDate;
                item.DueTime = task.DueTime;
                item.ReminderDate = task.ReminderDate;
                item.ReminderTime = task.ReminderTime;
            }
        }

        #endregion
    }
}
