namespace ToDoer.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ToDoer.Models;

    /// <summary>
    /// Provides the contract for manipulating the contexts data.
    /// </summary>
    public interface IContextRepository
    {
        /// <summary>
        /// Gets the contexts asynchronous.
        /// </summary>
        /// <returns>
        /// A list with the element type of ContextModel <see cref="ContextModel.cs" />
        /// </returns>
        Task<List<ContextModel>> GetContextsAsync();

        /// <summary>
        /// Gets the context asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// An instance of ContextModel <see cref="ContextModel.cs" />
        /// </returns>
        Task<ContextModel> GetContextAsync(int id);

        /// <summary>
        /// Adds the context asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        Task<ContextModel> AddContextAsync(ContextModel context);

        /// <summary>
        /// Updates the context asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        Task<ContextModel> UpdateContextAsync(ContextModel context);

        /// <summary>
        /// Deletes the context asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>An instance of Task <see cref="System.Threading.Tasks.Task.cs"/></returns>
        Task DeleteContextAsync(int id);
    }
}
