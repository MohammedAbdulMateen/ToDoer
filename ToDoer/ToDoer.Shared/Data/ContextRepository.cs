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
        /// _gets the storage file for contexts.
        /// </summary>
        /// <returns>An instance of StorageFile <see cref="StorageFile.cs"/></returns>
        private async Task<StorageFile> _getStorageFile()
        {
            var storageFolder = Package.Current.InstalledLocation;
            var storageFile = await storageFolder.GetFileAsync(Constants.ContextDataSource);

            return storageFile;
        }

        /// <summary>
        /// _reads the file as text.
        /// </summary>
        /// <returns>The file contents as a string</returns>
        private async Task<string> _readFileAsText()
        {
            string contents = null;
            var storageFile = await _getStorageFile();
            contents = await FileIO.ReadTextAsync(storageFile);

            return contents;
        }

        /// <summary>
        /// _gets the contexts.
        /// </summary>
        /// <returns>A list with the element type of ContextModel <see cref="ContextModel.cs"/></returns>
        private async Task<List<ContextModel>> _getContexts()
        {
            List<ContextModel> contexts = null;
            var contents = await _readFileAsText();
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
            var storageFile = await _getStorageFile();
            var contents = JsonConvert.SerializeObject(contexts);
            await FileIO.WriteTextAsync(storageFile, contents);
        }

        #endregion
    }
}
