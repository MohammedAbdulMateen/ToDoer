namespace ToDoer.Data
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ToDoer.Common;
    using ToDoer.Models;

    /// <summary>
    /// The context repository manages data in and out for contexts.
    /// </summary>
    public class ContextRepository : IContextRepository
    {
        #region Public Methods

        /// <summary>
        /// Gets the contexts asynchronous.
        /// </summary>
        /// <returns>
        /// A list with the element type of ContextModel <see cref="ContextModel.cs" />
        /// </returns>
        public async Task<List<ContextModel>> GetContextsAsync()
        {
            var contexts = await _getContextsAsync();

            return contexts;
        }

        /// <summary>
        /// Gets the context asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// An instance of ContextModel <see cref="ContextModel.cs" />
        /// </returns>
        public async Task<ContextModel> GetContextAsync(int id)
        {
            var contexts = await _getContextsAsync();
            var context = contexts.Single(x => x.Id == id);

            return context;
        }

        /// <summary>
        /// Adds the context asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task<ContextModel> AddContextAsync(ContextModel context)
        {
            var contexts = await _getContextsAsync();
            int id;
            var last = contexts.LastOrDefault();
            if (last == null)
            {
                id = 1;
            }
            else
            {
                id = last.Id + 1;
            }

            context.Id = id;
            contexts.Add(context);
            await _setContextsAsync(contexts);

            return context;
        }

        /// <summary>
        /// Updates the context asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task<ContextModel> UpdateContextAsync(ContextModel context)
        {
            var contexts = await _getContextsAsync();
            var sourceContext = contexts.Single(x => x.Id == context.Id);
            sourceContext.Name = context.Name;
            await _setContextsAsync(contexts);

            return sourceContext;
        }

        /// <summary>
        /// Deletes the context asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>An instance of Task <see cref="System.Threading.Tasks.Task.cs"/></returns>
        public async Task DeleteContextAsync(int id)
        {
            var contexts = await _getContextsAsync();
            var context = contexts.Single(x => x.Id == id);
            contexts.Remove(context);
            await _setContextsAsync(contexts);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// _gets the contexts asynchronous.
        /// </summary>
        /// <returns>
        /// A list with the element type of ContextModel <see cref="ContextModel.cs" />
        /// </returns>
        private async Task<List<ContextModel>> _getContextsAsync()
        {
            List<ContextModel> contexts = null;
            var contents = await FileUtility.ReadFileAsTextAsync(Constants.ContextDataSource);
            contexts = JsonConvert.DeserializeObject<List<ContextModel>>(contents);
            if (contexts == null)
            {
                contexts = new List<ContextModel>();
            }

            return contexts;
        }

        /// <summary>
        /// _sets the contexts asynchronous.
        /// </summary>
        /// <param name="contexts">The contexts.</param>
        /// <returns>An instance of Task <see cref="System.Threading.Tasks.Task.cs"/></returns>
        private async Task _setContextsAsync(List<ContextModel> contexts)
        {
            var contents = JsonConvert.SerializeObject(contexts);
            await FileUtility.WriteFileAsTextAsync(Constants.ContextDataSource, contents);
        }

        #endregion
    }
}
