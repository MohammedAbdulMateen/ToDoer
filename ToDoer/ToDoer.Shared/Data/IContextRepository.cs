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
        /// Gets the contexts.
        /// </summary>
        /// <returns>A list with the element type of ContextModel <see cref="ContextModel.cs"/></returns>
        Task<List<ContextModel>> GetContexts();

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>An instance of ContextModel <see cref="ContextModel.cs"/></returns>
        Task<ContextModel> GetContext(int id);

        /// <summary>
        /// Adds the context.
        /// </summary>
        /// <param name="context">The context.</param>
        Task<ContextModel> AddContext(ContextModel context);

        /// <summary>
        /// Updates the context.
        /// </summary>
        /// <param name="context">The context.</param>
        Task<ContextModel> UpdateContext(ContextModel context);

        /// <summary>
        /// Deletes the context.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteContext(int id);
    }
}
