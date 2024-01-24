using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Util.Generators.Resources.Languages
{
    /// <summary>
    /// 快速读取语言的帮助类
    /// </summary>
    internal static class Lang
    {
        /// <summary>
        /// 语言列表
        /// </summary>
        public static List<(string name, string code, string resourceUri)> Langs { get; }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static Lang()
        {
            Langs = new List<(string, string, string)>
            {
                ("简体中文", "zh-CN", "pack://application:,,,/Generators;component/Resources/Languages/zh-CN.xaml"),
                ("English", "en-US", "pack://application:,,,/Generators;component/Resources/Languages/en-US.xaml")
            };
        }

        /// <summary>
        /// 文件检查
        /// </summary>
        /// <returns></returns>
        public static bool FileCheck()
        {
            var langResourceDictionaries =
                from langInfo in Langs
                select new ResourceDictionary() { Source = new Uri(langInfo.Item3) };

            var resourceDictionaries =
                langResourceDictionaries as ResourceDictionary[] ?? langResourceDictionaries.ToArray();
            var baseLang = resourceDictionaries.FirstOrDefault();
            var allOk = true;

            for (var i = 1; i < resourceDictionaries.Count(); i++)
            {
                allOk = DiffAndPrint(baseLang, resourceDictionaries.ElementAt(i));
            }

            return allOk;
        }

        /// <summary>
        /// 比较和打印
        /// </summary>
        /// <param name="baseLang"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        private static bool DiffAndPrint(ResourceDictionary? baseLang, ResourceDictionary other)
        {
            var keysArray = new object[baseLang!.Keys.Count];
            baseLang.Keys.CopyTo(keysArray, 0);
            var otherKeysArray = new object[other.Keys.Count];
            other.Keys.CopyTo(otherKeysArray, 0);

            var missing = from key in keysArray
                          where !otherKeysArray.Contains(key)
                          select key;

            var extra = from key in otherKeysArray
                        where !keysArray.Contains(key)
                        select key;

            missing.All((k) =>
            {
                Console.WriteLine($"missing: {k}");
                return true;
            });

            extra.All((k) =>
            {
                Console.WriteLine($"extra: {k}");
                return true;
            });

            return !(missing.Any() || extra.Any());
        }
    }
}