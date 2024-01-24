﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Generators.Contexts;
using Util.Generators.Logs;
using Util.Generators.Resources;
using Util.Generators.Services.Abstractions;
using Util.Generators.Templates;

namespace Util.Generators.Services.Implements
{
    /// <summary>
    /// 生成器
    /// </summary>
    public class AppGenerator : IAppGenerator
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly IGeneratorLogger _logger;
        /// <summary>
        /// 生成器上下文构建器
        /// </summary>
        private readonly IGeneratorContextBuilder _contextBuilder;
        /// <summary>
        /// 生成器上下文
        /// </summary>
        public GeneratorContext Context { get; set; }
        /// <summary>
        /// 模板查找器
        /// </summary>
        private readonly ITemplateFinder _templateFinder;
        /// <summary>
        /// 静态资源管理器
        /// </summary>
        private readonly IResourceManager _resourceManager;
        /// <summary>
        /// 计时器
        /// </summary>
        private readonly Stopwatch _stopwatch;

        /// <summary>
        /// 初始化生成器
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="contextBuilder">生成器上下文构建器</param>
        /// <param name="templateFinder">模板查找器</param>
        /// <param name="resourceManager">静态资源管理器</param>
        public AppGenerator(IGeneratorLogger logger, IGeneratorContextBuilder contextBuilder, ITemplateFinder templateFinder, IResourceManager resourceManager)
        {
            _logger = logger;
            _contextBuilder = contextBuilder ?? throw new ArgumentNullException(nameof(contextBuilder));
            _templateFinder = templateFinder ?? throw new ArgumentNullException(nameof(templateFinder));
            _resourceManager = resourceManager ?? throw new ArgumentNullException(nameof(resourceManager));
            _stopwatch = new Stopwatch();
        }

        /// <inheritdoc />
        public async Task BuildContextAsync()
        {
            try
            {
                Context = await _contextBuilder.BuildAsync();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task GenerateAsync()
        {
            try
            {
                _stopwatch.Start();
                Context = await _contextBuilder.BuildAsync();
                foreach (var project in Context.Projects)
                {
                    await GenerateProject(project);
                }
                _logger.EndGenerate(_stopwatch);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// 生成项目
        /// </summary>
        /// <param name="projectContext">项目上下文</param>
        protected virtual async Task GenerateProject(ProjectContext projectContext)
        {
            if (projectContext.Enabled == false)
            {
                _logger.DisableProject(projectContext.Name);
                return;
            }
            var templates = GeTemplates(Context.TemplateRootPath, projectContext);
            _logger.BeginGenerateProject(projectContext.Name);
            foreach (var entity in projectContext.Entities)
            {
                await GenerateEntity(entity, templates);
            }
            await _resourceManager.ImportsAsync(Context.TemplateRootPath, Context.OutputRootPath, projectContext);
            _logger.EndGenerateProject(projectContext.Name);
        }

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="templateRootPath">模板根路径</param>
        /// <param name="projectContext">项目上下文</param>
        protected virtual List<ITemplate> GeTemplates(string templateRootPath, ProjectContext projectContext)
        {
            _logger.BeginGeTemplates(projectContext.Name, templateRootPath);
            var result = _templateFinder.Find(templateRootPath, projectContext).ToList();
            _logger.EndGeTemplates(projectContext.Name, result);
            return result;
        }

        /// <summary>
        /// 生成选择的实体
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task GenerateSelectedEntity(List<EntityContext> entities, ProjectContext project)
        {
            var templates = GeTemplates(Context.TemplateRootPath, project);
            foreach (var entity in entities)
            {
                await GenerateEntity(entity, templates);
            }
        }
        /// <summary>
        /// 生成实体
        /// </summary>
        /// <param name="entityContext">实体上下文</param>
        /// <param name="templates">模板列表</param>
        protected virtual async Task GenerateEntity(EntityContext entityContext, List<ITemplate> templates)
        {
            _logger.BeginGenerateEntity(entityContext);
            if (IsSkip(entityContext))
                return;
            foreach (var template in templates)
            {
                await template.RenderAsync(entityContext.Clone());
                _logger.RenderTemplate(entityContext, template);
            }
            _logger.EndGenerateEntity(entityContext);
        }

        /// <summary>
        /// 是否跳过
        /// </summary>
        protected virtual bool IsSkip(EntityContext entityContext)
        {
            if (entityContext.IsRelationTable)
            {
                _logger.SkipRelationTable(entityContext);
                return true;
            }
            return false;
        }
    }
}
