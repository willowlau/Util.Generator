using System;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using Util.Generators.Utils;
using Util.Generators.ViewModels.Base;

namespace Util.Generators.ViewModels
{
    /// <summary>
    /// 主窗口视图实体
    /// </summary>
    public class VMMain : ViewModelAppBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public VMMain()
        {
            InitData();
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string? Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private string? _title;

        /// <summary>
        /// 版本信息
        /// </summary>
        public string VersionInfo
        {
            get => _versionInfo;
            set => Set(ref _versionInfo, value);
        }

        private string _versionInfo = null!;
        
        /// <inheritdoc />
        protected override async Task Loaded()
        {
            await base.Loaded();
        }

        /// <inheritdoc />
        protected override async Task Unloaded()
        {
            await base.Unloaded();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitData()
        {
            Title = Lang("App.Name");
            VersionInfo = VersionHelper.GetVersion();
        }
    }
}