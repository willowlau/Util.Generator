using System;
using Microsoft.Extensions.Logging;
using Util.Generators.Services.Abstractions;
using Util.Helpers;

namespace Util.Generators.Services.Implements
{
    /// <summary>
    /// 消息总线
    /// </summary>
    public sealed class MessageBus : IMessageBus
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MessageBus()
        {
            _logger = Ioc.Create<ILogger<MessageBus>>();
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        private readonly ILogger<MessageBus> _logger;

        /// <summary>
        /// 接收消息事件委托
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs>? Received;

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="value"></param>
        public void Send(string msgType, object? value)
        {
            try
            {
                Received?.Invoke(this, new MessageReceivedEventArgs(msgType, value));
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occured when handling message : {msgType}/{value}", e);
            }
        }
    }
}