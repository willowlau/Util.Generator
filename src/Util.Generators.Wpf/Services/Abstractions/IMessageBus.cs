using System;
using Util.Dependency;

namespace Util.Generators.Services.Abstractions
{
    /// <summary>
    /// 消息总线接收事件参数
    /// </summary>
    public class MessageReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public MessageReceivedEventArgs(string type, object? value)
        {
            Value = value;
            MessageType = type;
        }

        /// <summary>
        /// 消息值
        /// </summary>
        public object? Value { get; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string MessageType { get; }
    }

    /// <summary>
    /// 消息总线
    /// </summary>
    public interface IMessageBus : ISingletonDependency
    {
        /// <summary>
        /// 接收事件
        /// </summary>
        event EventHandler<MessageReceivedEventArgs> Received;

        /// <summary>
        /// 发送方法
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="value"></param>
        void Send(string msgType, object? value = null);
    }
}