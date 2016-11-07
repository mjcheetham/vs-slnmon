using System;

namespace SolutionEventMonitor
{
    internal interface ILogger : IDisposable
    {
        void Log(string message);
    }
}