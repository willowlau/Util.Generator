using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Util.Generators.Converters
{
    /// <summary>
    /// 字符串转画刷转换器
    /// </summary>
    public class StringToBrushConverter : IValueConverter
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
            var color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(colorStr);
            return new SolidColorBrush(color);
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