namespace ToDoer.Data
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ToDoer.Common;
    using ToDoer.Models;

    /// <summary>
    /// The task repository manages data in and out for tasks.
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        #region Public Methods

        /// <summary>
        /// Gets the tasks asynchronous.
        /// </summary>
        /// <param name="contextId">The context identifier.</param>
        /// <returns>
        /// A list with the element type of TaskModel <see cref="TaskModel.cs" />
        /// </returns>
        public async Task<List<TaskModel>> GetTasksAsync(int contextId)
        {
            var tasks = await _getTasksAsync(contextId);

            return tasks;
        }

        /// <summary>
        /// Gets the tasks asynchronous between the specified dates.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>
        /// A list with the element type of TaskModel <see cref="TaskModel.cs" />
        /// </returns>
        public async Task<List<TaskModel>> GetTasksAsync(DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
        {
            var tasks = await _getTasksAsync();
            if (startDate != null && endDate == null)
            {
                tasks = tasks.Where(x => x.DueDate == startDate).ToList();
            }
            else if (startDate != null && endDate != null)
            {
                tasks = tasks.Where(x => x.DueDate >= startDate && x.DueDate <= endDate).ToList();
            }

            return tasks;
        }

        /// <summary>
        /// Gets the task asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// An instance TaskModel <see cref="TaskModel.cs" />
        /// </returns>
        public async Task<TaskModel> GetTaskAsync(int id)
        {
            var tasks = await _getTasksAsync();
            var context = tasks.Single(x => x.Id == id);

            return context;
        }

        /// <summary>
        /// Adds the task asynchronous.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>
        /// An instance TaskModel <see cref="TaskModel.cs" />
        /// </returns>
        public async Task<TaskModel> AddTaskAsync(TaskModel task)
        {
            int id;
            var tasks = await _getTasksAsync();
            var lastTask = tasks.LastOrDefault();
            if (lastTask == null)
            {
                id = 1;
            }
            else
            {
                id = lastTask.Id + 1;
            }

            task.Id = id;
            tasks.Add(task);
            await _setTasksAsync(tasks);

            return task;
        }

        /// <summary>
        /// Updates the task asynchronous.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>
        /// An instance TaskModel <see cref="TaskModel.cs" />
        /// </returns>
        public async Task<TaskModel> UpdateTaskAsync(TaskModel task)
        {
            var tasks = await _getTasksAsync();
            var sourceTask = tasks.Single(x => x.Id == task.Id);
            sourceTask.Todo = task.Todo;
            sourceTask.DueDate = task.DueDate;
            sourceTask.DueTime = task.DueTime;
            sourceTask.ReminderDate = task.ReminderDate;
            sourceTask.ReminderTime = task.ReminderTime;
            await _setTasksAsync(tasks);

            return sourceTask;
        }

        /// <summary>
        /// Deletes the task asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>An instance of Task <see cref="System.Threading.Tasks.Task.cs"/></returns>
        public async Task DeleteTaskAsync(int id)
        {
            var tasks = await _getTasksAsync();
            var task = tasks.Single(x => x.Id == id);
            tasks.Remove(task);
            await _setTasksAsync(tasks);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// _gets the tasks asynchronous.
        /// </summary>
        /// <param name="contextId">
        /// The context identifier.
        /// <remarks>The default value is -1 which gets the tasks for all the contexts.</remarks>
        /// </param>
        /// <returns>
        /// A list with the element type of TaskModel <see cref="TaskModel.cs" />
        /// </returns>
        private async Task<List<TaskModel>> _getTasksAsync(int contextId = Constants.DefaultContextId)
        {
            List<TaskModel> tasks = null;
            var contents = await FileUtility.ReadFileAsTextAsync(Constants.TaskDataSource);
            tasks = JsonConvert.DeserializeObject<List<TaskModel>>(contents);
            if (tasks == null)
            {
                tasks = new List<TaskModel>();
            }
            else if (contextId != Constants.DefaultContextId)
            {
                tasks = tasks.Where(x => x.ContextId == contextId).ToList();
            }

            return tasks;
        }

        /// <summary>
        /// _sets the tasks asynchronous.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        /// <returns>An instance of Task <see cref="System.Threading.Tasks.Task.cs"/></returns>
        private async Task _setTasksAsync(List<TaskModel> tasks)
        {
            var contents = JsonConvert.SerializeObject(tasks);
            await FileUtility.WriteFileAsTextAsync(Constants.TaskDataSource, contents);
        }

        #endregion
    }
}
