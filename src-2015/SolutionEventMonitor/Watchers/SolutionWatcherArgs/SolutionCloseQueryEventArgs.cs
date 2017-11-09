using System;

namespace SolutionEventMonitor.Watchers
{
    internal class SolutionCloseQueryEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
    }
}