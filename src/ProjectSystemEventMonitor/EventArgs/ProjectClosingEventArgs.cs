using System;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
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