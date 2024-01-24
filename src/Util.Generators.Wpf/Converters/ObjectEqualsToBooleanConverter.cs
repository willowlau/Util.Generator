using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Util.Generators.Converters
{
    /// <summary>
    /// Value与Parameter相同True，否则False
    /// </summary>
    [ValueConversion(typeof(object), typeof(Visibility), ParameterType = typeof(object))]
    public class ObjectEqualsToBooleanConverter : IMultiValueConverter
    {
        /// <summary>
        /// 转换值
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 1)
            {
                if ((values[0] is bool) && (bool)values[0] == false)
                {
                    return false;
                }

                return true;
            }

            if (values.Length == 2)
            {
                if (values[0].ToString() == values[1].ToString())
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        /// <summary>
        /// 转换值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return Array.Empty<object>();
        }
    }
}