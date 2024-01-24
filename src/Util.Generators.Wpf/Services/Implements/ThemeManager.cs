using System;
using System.Windows;
using Util.Generators.Enums;
using Util.Generators.Services.Abstractions;
using Util.Helpers;

namespace Util.Generators.Services.Implements
{
    /// <summary>
    /// 主题管理
    /// </summary>
    public sealed class ThemeManager : IThemeManager
    {
        /// <summary>
        /// 主题模式
        /// </summary>
        public ThemeMode ThemeMode
        {
            get => _themeMode;
            set
            {
                _themeMode = value;
                Reload();
            }
        }

        ThemeMode _themeMode = ThemeMode.Light;

        private const int INDEX_OF_THEME = 2;

        private readonly ResourceDictionary _lightTheme;
        private readonly ResourceDictionary _darkTheme;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ThemeManager()
        {
            _lightTheme = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/Generators;component/Resources/Themes/ThemeLight.xaml")
            };
            _darkTheme = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/Generators;component/Resources/Themes/ThemeDark.xaml")
            };
            _themeMode = ThemeMode.Light;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            ApplyTheme(_lightTheme);
        }

        /// <summary>
        /// 重新加载
        /// </summary>
        public void Reload()
        {
            var themeDictionary = ThemeMode switch
            {
                ThemeMode.Light => _lightTheme,
                ThemeMode.Dark => _darkTheme,
                _ => _lightTheme,
                //_ => ShouldUseDarkTheme() ? DarkTheme : LightTheme
            };
            ApplyTheme(themeDictionary);
        }

        /// <summary>
        /// 应用主题
        /// </summary>
        /// <param name="themeDictionary"></param>
        private void ApplyTheme(ResourceDictionary themeDictionary)
        {
            App.Current!.Resources.MergedDictionaries[INDEX_OF_THEME] = themeDictionary;
        }

        /// <summary>
        /// 析构
        /// </summary>
        ~ThemeManager()
        {
        }

        /// <summary>
        /// 自动主题
        /// </summary>
        /// <returns></returns>
        private bool ShouldUseDarkTheme()
        {
            int hour = DateTime.Now.Hour;
            return (hour > 18 || hour < 6);
        }
    }
}