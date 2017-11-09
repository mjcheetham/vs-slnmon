using Microsoft.VisualStudio.Shell.Interop;
using System;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
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