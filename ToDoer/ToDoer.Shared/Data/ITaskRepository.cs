namespace ToDoer.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ToDoer.Models;

    /// <summary>
    /// Provides the contract for manipulating the tasks data.
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Gets the tasks asynchronous.
        /// </summary>
        /// <param name="contextId">The context identifier.</param>
        /// <returns>
        /// A list with the element type of TaskModel <see cref="TaskModel.cs" />
        /// </returns>
        Task<List<TaskModel>> GetTasksAsync(int contextId);

        /// <summary>
        /// Gets the tasks asynchronous between the specified dates.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>A list with the element type of TaskModel <see cref="TaskModel.cs" /></returns>
        Task<List<TaskModel>> GetTasksAsync(DateTimeOffset? startDate = null, DateTimeOffset? endDate = null);

        /// <summary>
        /// Gets the task asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// An instance of TaskModel <see cref="TaskModel.cs" />
        /// </returns>
        Task<TaskModel> GetTaskAsync(int id);

        /// <summary>
        /// Adds the task asynchronous.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>
        /// An instance TaskModel <see cref="TaskModel.cs" />
        /// </returns>
        Task<TaskModel> AddTaskAsync(TaskModel task);

        /// <summary>
        /// Updates the task asynchronous.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>
        /// An instance of TaskModel <see cref="TaskModel.cs" />
        /// </returns>
        Task<TaskModel> UpdateTaskAsync(TaskModel task);

        /// <summary>
        /// Deletes the task asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>An instance of Task <see cref="System.Threading.Tasks.Task.cs"/></returns>
        Task DeleteTaskAsync(int id);

        /// <summary>
        /// Deletes the tasks asynchronous belonging to a particular Context.
        /// </summary>
        /// <param name="contextId">The context identifier.</param>
        /// <returns>An instance of Task <see cref="System.Threading.Tasks.Task.cs"/></returns>
        Task DeleteTasksAsync(int contextId);
    }
}
