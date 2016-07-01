namespace ToDoer.Common
{
    using Microsoft.Xaml.Interactivity;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;

    /// <summary>
    /// Used to open the associated MenuFlyout
    /// </summary>
    public class OpenMenuFlyoutAction : DependencyObject, IAction
    {
        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The result of the action.</returns>
        public object Execute(object sender, object parameter)
        {
            var senderElement = sender as FrameworkElement;
            if (senderElement.GetType() == typeof(TextBlock))
            {
                var textBlock = senderElement as TextBlock;
                if (textBlock.Text == Constants.DefaultContext || textBlock.Text == "Today" ||
                    textBlock.Text == "Tomorrow" || textBlock.Text == "Week")
                {
                    return null;
                }
            }

            var flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
            flyoutBase.ShowAt(senderElement);

            return null;
        }
    }
}
