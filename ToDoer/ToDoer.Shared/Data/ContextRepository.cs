namespace ToDoer.Data
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ToDoer.Common;
    using ToDoer.Models;
    using Windows.ApplicationModel;
    using Windows.Storage;

    /// <summary>
    /// The context repository manages data in and out for contexts.
    /// </summary>
    public class ContextRepository : IContextRepository
    {
        #region Methods

        /// <summary>
        /// Gets the contexts.
        /// </summary>
        /// <returns>A list with the element type of ContextModel <see cref="ContextModel.cs"/></returns>
        public async Task<List<ContextModel>> GetContexts()
        {
            var contexts = await _getContexts();

            return contexts;
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>An instance of ContextModel <see cref="ContextModel.cs"/></returns>
        public async Task<ContextModel> GetContext(int id)
        {
            var contexts = await _getContexts();
            var context = contexts.Single(x => x.Id == id);

            return context;
        }

        /// <summary>
        /// Adds the context.
        /// </summary>
        /// <param name="context">The context.</param>
        public async Task<ContextModel> AddContext(ContextModel context)
        {
            var contexts = await _getContexts();
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
            _setContexts(contexts);

            return context;
        }

        /// <summary>
        /// Updates the context.
        /// </summary>
        /// <param name="context">The context.</param>
        public async Task<ContextModel> UpdateContext(ContextModel context)
        {
            var contexts = await _getContexts();
            var sourceContext = contexts.Single(x => x.Id == context.Id);
            sourceContext.Name = context.Name;
            _setContexts(contexts);

            return sourceContext;
        }

        /// <summary>
        /// Deletes the context.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async void DeleteContext(int id)
        {
            var contexts = await _getContexts();
            var context = contexts.Single(x => x.Id == id);
            contexts.Remove(context);
            _setContexts(contexts);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// _gets the contexts.
        /// </summary>
        /// <returns>A list with the element type of ContextModel <see cref="ContextModel.cs"/></returns>
        private async Task<List<ContextModel>> _getContexts()
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
        /// _sets the contexts.
        /// </summary>
        /// <param name="contexts">The contexts.</param>
        private async void _setContexts(List<ContextModel> contexts)
        {
            var contents = JsonConvert.SerializeObject(contexts);
            await FileUtility.WriteFileAsTextAsync(Constants.ContextDataSource, contents);
        }

        #endregion
    }
}
