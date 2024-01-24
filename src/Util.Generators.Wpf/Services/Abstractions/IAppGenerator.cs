using System.Collections.Generic;
using System.Threading.Tasks;
using Util.Dependency;
using Util.Generators.Contexts;
using Util.Generators.Templates;

namespace Util.Generators.Services.Abstractions
{
    /// <summary>
    /// 生成器
    /// </summary>
    public interface IAppGenerator : IGenerator, ISingletonDependency
    {
        /// <summary>
        /// 生成器上下文
        /// </summary>
        public GeneratorContext Context { get; set; }

        /// <summary>
        /// 生成上下文
        /// </summary>
        /// <returns></returns>
        Task BuildContextAsync();

        /// <summary>
        /// 生成选择的实体
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        Task GenerateSelectedEntity(List<EntityContext> entities, ProjectContext project);
    }
}
