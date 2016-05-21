namespace ToDoer.ViewModels
{
    using GalaSoft.MvvmLight.Views;
    using System.Collections.Generic;
    using ToDoer.Common;
    using ToDoer.Commands;
    using ToDoer.Interfaces;
    using ToDoer.Models;
    using System.Windows.Input;
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
        /// The tasks
        /// </summary>
        private List<TaskModel> tasks;

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
        /// Initializes a new instance of the <see cref="TaskViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        public TaskViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the tasks.
        /// </summary>
        /// <value>
        /// The tasks.
        /// </value>
        public List<TaskModel> Tasks
        {
            get
            {
                return this.tasks;
            }
            set
            {
                if (value == this.tasks)
                {
                    return;
                }

                this.tasks = value;
                this.NotifyPropertyChanged();
            }
        }

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

        #region Methods

        /// <summary>
        /// Activates the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void Activate(object parameter)
        {
            this.currentContext = parameter as ContextModel;
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
            this.navigationService.GoBack();
        }
#endif

        /// <summary>
        /// Called when [add task].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void OnAddTask(object parameter)
        {
            this.navigationService.NavigateTo(Constants.AddTask, this.currentContext);
        }

        #endregion
    }
}
