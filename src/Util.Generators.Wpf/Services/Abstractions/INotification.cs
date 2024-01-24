using System.Threading.Tasks;
using Util.Dependency;

namespace Util.Generators.Services.Abstractions
{
    /// <summary>
    /// 通知管理
    /// </summary>
    public interface INotification : ISingletonDependency
    {
        /// <summary>
        /// 会话
        /// </summary>
        string? Token { get; set; }

        /// <summary>
        /// 询问
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        Task<bool> Ask(string msg);

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="msg"></param>
        void Warn(string msg);

        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="msg"></param>
        void Info(string msg);

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="msg"></param>
        void Success(string msg);

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg"></param>
        void Error(string msg);
    }
}