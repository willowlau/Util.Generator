using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Util.Generators.ViewModels.Base
{
    /// <summary>
    /// 视图实体基类
    /// </summary>
    public class ViewModelBase : IViewModelBase
    {
        /// <summary>
        /// 属性变更事件委托
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 属性变更
        /// </summary>
        /// <param name="propertyName"></param>
        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        public void Set<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
        {
            if (!Equals(storage, value))
            {
                storage = value;
                RaisePropertyChanged(propertyName);
            }
        }
    }
}