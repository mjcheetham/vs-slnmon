using Microsoft.VisualStudio.Shell.Interop;
using System;

namespace SolutionEventMonitor.Watchers
{
    internal class ProjectUnloadQueryEventArgs : EventArgs
    {
        public IVsHierarchy Hierarchy { get; }
        public bool Cancel { get; internal set; }

        public ProjectUnloadQueryEventArgs(IVsHierarchy hierarchy)
        {
            if (hierarchy == null)
            {
                throw new ArgumentNullException(nameof(hierarchy));
            }

            Hierarchy = hierarchy;
        }
    }
}