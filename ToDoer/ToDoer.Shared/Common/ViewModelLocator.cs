namespace ToDoer.Common
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Views;
    using Microsoft.Practices.ServiceLocation;
    using ToDoer.Pages;
    using ToDoer.ViewModels;

    /// <summary>
    /// The view model locator
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelLocator"/> class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
            }
            else
            {
            }

            var navigationService = this.CreateNavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);

            SimpleIoc.Default.Register<IDialogService, DialogService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<NewContextViewModel>();
            SimpleIoc.Default.Register<TaskViewModel>();
            SimpleIoc.Default.Register<NewTaskViewModel>();
        }

        /// <summary>
        /// Creates the navigation service.
        /// </summary>
        /// <returns>An instance of type implementing the INavigationService interface</returns>
        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(Constants.MainPage, typeof(MainPage));
            navigationService.Configure(Constants.AddContext, typeof(AddContext));
            navigationService.Configure(Constants.Task, typeof(Task));
            navigationService.Configure(Constants.AddTask, typeof(AddTask));

            return navigationService;
        }
    }
}
