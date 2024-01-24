using System;
using System.Globalization;
using System.Windows.Data;

namespace Util.Generators.Converters
{
    /// <summary>
    /// 字符串转颜色转换器
    /// </summary>
    public class StringToColorConverter : IValueConverter
    {
        /// <summary>
        /// 转换值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colorStr = value is string ? value.ToString() : default;
            return System.Windows.Media.ColorConverter.ConvertFromString(colorStr);
        }

        /// <summary>
        /// 转换值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}