using System;
using System.Runtime.CompilerServices;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    internal static class LoggerExtensions
    {
        public static void LogEvent(this ILogger logger, [CallerMemberName] string caller = null)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.Log(caller);
        }

        public static void LogEventMessage(this ILogger logger, string message, [CallerMemberName] string caller = null)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.Log($"{caller}\t{message}");
        }
    }
}
