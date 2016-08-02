// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BoolToVisibilityConverter.cs" company="">
//   
// </copyright>
// <summary>
//   The bool to visibility converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FH.Converter
{
    using System;
    using System.Windows;

    /// <summary>
    /// The boolean to visibility converter.
    /// </summary>
    public class BoolToVisibilityConverter : System.Windows.Data.IValueConverter
    {
        /// <summary>
        /// Coverts a boolean value to Visibility of a control ( Visible or Hidden)
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="targetType">The type to convert to</param>
        /// <param name="parameter">Used to compare the value of value and returns that comparison value if non null</param>
        /// <param name="culture">The culture used for the conversion.</param>
        /// <returns>The converted object</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var boolValue = (bool)value;

            if ((boolValue && parameter == null) || (parameter != null && System.Convert.ToBoolean(parameter) == boolValue))
            {
                return Visibility.Visible;
            }

            return Visibility.Hidden;
        }

        /// <summary>
        /// Converts visibility(Visible or Hidden) of a control to boolean
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="targetType">The type to convert to</param>
        /// <param name="parameter">The parameter used for conversion</param>
        /// <param name="culture">The culture used for the conversion.</param>
        /// <returns>The converted object</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var visibility = (Visibility)value;

            if (visibility == Visibility.Visible)
            {
                return true;
            }

            return false;
        }
    }
}
