using CopyDirectory.Services;
using System;

namespace CopyDirectory.Shared.Utils
{
    public class MethodExecutionUtils : IMethodExecutionUtils
    {

        private readonly IMessageHandler _messageHandler;

        public MethodExecutionUtils(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        /// <inheritdoc cref="IMethodExecutionUtils.TryCatchMethod{T}(Func{T}, string)"/>
        public T TryCatchMethod<T>(Func<T> someMethod, string errorMessage)
        {
            T result = default;
            try
            {
                result = someMethod();
            }
            catch (Exception e)
            {
                _messageHandler.PrintError($"{errorMessage} ERROR: {e.Message}");
            }

            return result;
        }
    }
}
