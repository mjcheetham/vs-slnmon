using System;

namespace SolutionEventMonitor.Watchers
{
    internal class ProjectClosingEventArgs : EventArgs
    {
        public bool Removed { get; }

        public ProjectClosingEventArgs(bool removed)
        {
            Removed = removed;
        }
    }
}