using System;

namespace Util.Generators.Utils.Loader
{
    /// <summary>
    /// 步骤完成事件参数
    /// </summary>
    public class StepFinishedEventArgs : EventArgs
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="finishedStep"></param>
        /// <param name="totalStepCount"></param>
        public StepFinishedEventArgs(uint finishedStep, uint totalStepCount)
        {
            FinishedStep = finishedStep;
            TotalStepCount = totalStepCount;
        }

        /// <summary>
        /// 已完成步骤
        /// </summary>
        public uint FinishedStep { get; }

        /// <summary>
        /// 总步骤
        /// </summary>
        public uint TotalStepCount { get; }
    }
}