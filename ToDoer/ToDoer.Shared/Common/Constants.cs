namespace ToDoer.Common
{
    using Windows.ApplicationModel.Resources;

    /// <summary>
    /// Contains all the constants used across the application
    /// </summary>
    public static class Constants
    {
        #region Default Contexts

        /// <summary>
        /// The default context identifier
        /// </summary>
        public const int DefaultContextId = -1;

        /// <summary>
        /// The inbox
        /// </summary>
        public const string Inbox = "Inbox";

        /// <summary>
        /// The today
        /// </summary>
        public const string Today = "Today";

        /// <summary>
        /// The tomorrow
        /// </summary>
        public const string Tomorrow = "Tomorrow";

        /// <summary>
        /// The week
        /// </summary>
        public const string Week = "Week";

        #endregion

        #region Pages

        /// <summary>
        /// The main page
        /// </summary>
        public const string MainPage = "MainPage";

        /// <summary>
        /// The add context
        /// </summary>
        public const string AddContext = "AddContext";

        /// <summary>
        /// The task
        /// </summary>
        public const string Task = "Task";

        /// <summary>
        /// The add task
        /// </summary>
        public const string AddTask = "AddTask";

        #endregion

        #region AppData

        /// <summary>
        /// The context data source
        /// </summary>
        public const string ContextDataSource = "App_Data\\contexts.json";

        /// <summary>
        /// The task data source
        /// </summary>
        public const string TaskDataSource = "App_Data\\tasks.json";

        #endregion

        #region Messages

        /// <summary>
        /// The okay
        /// </summary>
        public const string Okay = "Okay";

        /// <summary>
        /// The cancel
        /// </summary>
        public const string Cancel = "Cancel";

        /// <summary>
        /// The delete prompt
        /// </summary>
        public const string DeletePrompt = "DeletePrompt";

        #endregion

        #region Validation Properties

        /// <summary>
        /// The context name property
        /// </summary>
        public const string ContextNameProperty = "Name";

        #endregion

        #region Validation Messages

        /// <summary>
        /// The context name empty error
        /// </summary>
        public const string ContextNameEmptyError = "Context Name cannot be blank.";

        #endregion
    }
}
