using System;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    internal interface ILogger : IDisposable
    {
        void Log(string message);
    }
}
