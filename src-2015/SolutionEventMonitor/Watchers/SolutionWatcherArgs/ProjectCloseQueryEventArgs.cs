using Microsoft.VisualStudio.Shell.Interop;
using System;

namespace SolutionEventMonitor.Watchers
{
    internal class ProjectCloseQueryEventArgs : EventArgs
    {
        public IVsHierarchy Hierarchy { get; }

        public bool Removing { get; }

        public bool Cancel { get; set; }

        public ProjectCloseQueryEventArgs(IVsHierarchy hierarchy, bool removing)
        {
            if (hierarchy == null)
            {
                throw new ArgumentNullException(nameof(hierarchy));
            }

            Hierarchy = hierarchy;
            Removing = removing;
        }
    }
}