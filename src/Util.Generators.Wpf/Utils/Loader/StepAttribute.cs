using System;

namespace Util.Generators.Utils.Loader
{
    /// <summary>
    /// 步骤属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class StepAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="step"></param>
        public StepAttribute(uint step)
        {
            Step = step;
        }

        /// <summary>
        /// 步骤号
        /// </summary>
        public uint Step { get; }
    }
}