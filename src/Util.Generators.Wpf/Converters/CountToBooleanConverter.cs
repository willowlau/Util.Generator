using System;
using System.Globalization;
using System.Windows.Data;

namespace Util.Generators.Converters
{
    /// <summary>
    /// 值大于0为真，小于0假
    /// </summary>
    public class CountToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// 转换值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString().ToInt() > 0;
        }

        /// <summary>
        /// 转换值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}