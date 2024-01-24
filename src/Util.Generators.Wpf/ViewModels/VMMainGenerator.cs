using CommunityToolkit.Mvvm.Input;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util.Generators.Contexts;
using Util.Generators.Services.Abstractions;
using Util.Generators.ViewModels.Base;

namespace Util.Generators.ViewModels
{
    /// <summary>
    /// 生成窗口视图实体
    /// </summary>
    public class VMMainGenerator : ViewModelAppBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public VMMainGenerator(IAppGenerator appGenerator)
        {
            _appGenerator = appGenerator;
            InitData();
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly IAppGenerator _appGenerator;

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
        /// 生成路径
        /// </summary>
        public string? ExportFolder
        {
            get => _exportFolder;
            set => Set(ref _exportFolder, value);
        }

        private string? _exportFolder;

        /// <summary>
        /// 当前项目
        /// </summary>
        public ProjectContext Project
        {
            get => _project;
            set => Set(ref _project, value);
        }
        private ProjectContext _project;

        /// <summary>
        /// 项目列表
        /// </summary>
        public ObservableCollection<ProjectContext> ProjectList
        {
            get => _projectList;
            set => Set(ref _projectList, value);
        }
        private ObservableCollection<ProjectContext> _projectList;

        /// <summary>
        /// 实体列表
        /// </summary>
        public ObservableCollection<EntityContext> EntityList
        {
            get => _entityList;
            set => Set(ref _entityList, value);
        }
        private ObservableCollection<EntityContext> _entityList;

        /// <inheritdoc />
        protected override async Task Loaded()
        {
            await base.Loaded();
            await _appGenerator.BuildContextAsync();
            if (_appGenerator.Context != null)
            {
                ExportFolder = _appGenerator.Context.OutputRootPath;
                ProjectList = new ObservableCollection<ProjectContext>(_appGenerator.Context.Projects);
                //Project = ProjectList.First();
                //EntityList = new ObservableCollection<EntityContext>(Project.Entities);
            }
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
        }

        /// <summary>
        /// 切换项目
        /// </summary>
        public RelayCommand<ProjectContext> SwitchProjectCmd => new(project =>
        {
            if (project != null)
            {
                Project = project;
                EntityList = new ObservableCollection<EntityContext>(project.Entities);
            }
        });

        /// <summary>
        /// 选择导出目录
        /// </summary>
        public RelayCommand SelectFolderCmd => new(() =>
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ExportFolder = dialog.SelectedPath;
            }
        });

        public AsyncRelayCommand<object> BuildCmd => new(async param =>
        {
            var items = (IList)param!;
            var list = items.Cast<EntityContext>().ToList();
            if (list.Count == 0)
            {
                _notification.Warn(Lang("MainGenerator.Message.PleaseSelectedTable"));
                return;
            }

            await AnimateLoad(async () =>
            {
                await _appGenerator.GenerateSelectedEntity(list, Project);
                _notification.Success(Lang("Common.Message.Success"));
            });
        });
    }
}