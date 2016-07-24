namespace ToDoer.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ToDoer.Interfaces;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// A value converter to convert IValidationMessage to string
    /// </summary>
    public class ValidationMessageConverter : IValueConverter
    {
        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>A validation message</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is IEnumerable<IValidationMessage>))
            {
                return string.Empty;
            }

            var collection = value as IEnumerable<IValidationMessage>;
            if (!collection.Any())
            {
                return string.Empty;
            }

            return collection.FirstOrDefault().Message;
        }

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>An instance of an object</returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
