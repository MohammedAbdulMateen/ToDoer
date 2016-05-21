namespace ToDoer.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ToDoer.Models;

    /// <summary>
    /// Provides the contract for manipulating the tasks data.
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <returns>A list with the element type of TaskModel <see cref="TaskModel.cs"/></returns>
        Task<List<TaskModel>> GetTasks();

        /// <summary>
        /// Gets the task.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>An instance of TaskModel <see cref="TaskModel.cs"/></returns>
        Task<TaskModel> GetTask(int id);

        /// <summary>
        /// Adds the task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>An instance TaskModel <see cref="TaskModel.cs"/></returns>
        Task<TaskModel> AddTask(TaskModel task);

        /// <summary>
        /// Updates the task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>An instance of TaskModel <see cref="TaskModel.cs"/></returns>
        Task<TaskModel> UpdateTask(TaskModel task);

        /// <summary>
        /// Deletes the task.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteTask(int id);
    }
}
