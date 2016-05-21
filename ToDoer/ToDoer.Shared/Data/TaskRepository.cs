namespace ToDoer.Data
{
    using Newtonsoft.Json;
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
        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <returns>A list with the element type of TaskModel <see cref="TaskModel.cs"/></returns>
        public async Task<List<TaskModel>> GetTasks()
        {
            var tasks = await _getTasks();

            return tasks;
        }

        /// <summary>
        /// Gets the task.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>An instance TaskModel <see cref="TaskModel.cs"/></returns>
        public async Task<TaskModel> GetTask(int id)
        {
            var tasks = await _getTasks();
            var context = tasks.Single(x => x.Id == id);

            return context;
        }

        /// <summary>
        /// Adds the task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>An instance TaskModel <see cref="TaskModel.cs"/></returns>
        public async Task<TaskModel> AddTask(TaskModel task)
        {
            var tasks = await _getTasks();
            int id;
            var last = tasks.LastOrDefault();
            if (last == null)
            {
                id = 1;
            }
            else
            {
                id = last.Id + 1;
            }

            task.Id = id;
            tasks.Add(task);
            _setTasks(tasks);

            return task;
        }

        /// <summary>
        /// Updates the task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>An instance TaskModel <see cref="TaskModel.cs"/></returns>
        public async Task<TaskModel> UpdateTask(TaskModel task)
        {
            var tasks = await _getTasks();
            var sourceTask = tasks.Single(x => x.Id == task.Id);
            sourceTask.Todo = task.Todo;
            sourceTask.DueDate = task.DueDate;
            sourceTask.DueTime = task.DueTime;
            sourceTask.ReminderDate = task.ReminderDate;
            sourceTask.ReminderTime = task.ReminderTime;
            _setTasks(tasks);

            return sourceTask;
        }

        /// <summary>
        /// Deletes the task.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async void DeleteTask(int id)
        {
            var tasks = await _getTasks();
            var task = tasks.Single(x => x.Id == id);
            tasks.Remove(task);
            _setTasks(tasks);
        }

        #region Private Methods

        /// <summary>
        /// _gets the tasks.
        /// </summary>
        /// <returns>A list with the element type of TaskModel <see cref="TaskModel.cs"/></returns>
        private async Task<List<TaskModel>> _getTasks()
        {
            List<TaskModel> tasks = null;
            var contents = await FileUtility.ReadFileAsTextAsync(Constants.TaskDataSource);
            tasks = JsonConvert.DeserializeObject<List<TaskModel>>(contents);
            if (tasks == null)
            {
                tasks = new List<TaskModel>();
            }

            return tasks;
        }

        /// <summary>
        /// _sets the tasks.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        private async void _setTasks(List<TaskModel> tasks)
        {
            var contents = JsonConvert.SerializeObject(tasks);
            await FileUtility.WriteFileAsTextAsync(Constants.TaskDataSource, contents);
        }

        #endregion
    }
}
