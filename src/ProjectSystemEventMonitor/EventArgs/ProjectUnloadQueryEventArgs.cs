using Microsoft.VisualStudio.Shell.Interop;
using System;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
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