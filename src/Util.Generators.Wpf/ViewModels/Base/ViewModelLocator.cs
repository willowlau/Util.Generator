using System;
using Util.Helpers;

namespace Util.Generators.ViewModels.Base
{
    /// <summary>
    /// 视图定位
    /// </summary>
    public class ViewModelLocator
    {
        public static ViewModelLocator Instance = new Lazy<ViewModelLocator>(() =>
            (App.Current?.TryFindResource("Locator") as ViewModelLocator)!).Value;

        // 公共
        public VMMain Main => Ioc.Create<VMMain>()!;
        public VMMainGenerator MainGenerator => Ioc.Create<VMMainGenerator>();
    }
}