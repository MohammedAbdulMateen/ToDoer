namespace ToDoer.Common
{
    using Windows.ApplicationModel.Resources;

    /// <summary>
    /// Interacts with the ResourceLoader to get the appropriate resource values <see cref="ResourceLoader.cs"/>
    /// </summary>
    public static class LocalizationService
    {
        /// <summary>
        /// Gets the localized message.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Returns the most appropriate string value of a resource, specified by the key</returns>
        public static string GetLocalizedMessage(string key)
        {
            return ResourceLoader.GetForCurrentView().GetString(key);
        }
    }
}
