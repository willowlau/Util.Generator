﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Util.Generators.Converters
{
    /// <summary>
    /// 值等于0隐藏，否则显示
    /// </summary>
    public class CountToCollapsedConverter : IValueConverter
    {
        /// <summary>
        /// 转换值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (decimal.TryParse(value?.ToString(), out var count))
            {
                return count == 0 ? Visibility.Collapsed : Visibility.Visible;
            }

            return Visibility.Collapsed;
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