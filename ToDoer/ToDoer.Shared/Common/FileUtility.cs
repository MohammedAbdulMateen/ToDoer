namespace ToDoer.Common
{
    using System;
    using System.Threading.Tasks;
    using Windows.ApplicationModel;
    using Windows.Storage;

    /// <summary>
    /// The utility to work with files in a UWP project.
    /// </summary>
    public static class FileUtility
    {
        /// <summary>
        /// Reads the file as text asynchronous.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        /// The file contents as a string
        /// </returns>
        public static async Task<string> ReadFileAsTextAsync(string filePath)
        {
            string contents = null;
            var storageFile = await GetStorageFile(filePath);
            contents = await FileIO.ReadTextAsync(storageFile);

            return contents;
        }

        /// <summary>
        /// Writes the file as text asynchronous.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="contents">The contents.</param>
        public static async Task WriteFileAsTextAsync(string filePath, string contents)
        {
            var storageFile = await GetStorageFile(Constants.ContextDataSource);
            await FileIO.WriteTextAsync(storageFile, contents);
        }

        /// <summary>
        /// _gets the storage file for contexts.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        /// An instance of StorageFile <see cref="StorageFile.cs" />
        /// </returns>
        private static async Task<StorageFile> GetStorageFile(string filePath)
        {
            var storageFolder = Package.Current.InstalledLocation;
            var storageFile = await storageFolder.GetFileAsync(filePath);

            return storageFile;
        }
    }
}
