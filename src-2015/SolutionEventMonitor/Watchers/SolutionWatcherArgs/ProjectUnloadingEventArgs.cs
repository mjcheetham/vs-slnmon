using Microsoft.VisualStudio.Shell.Interop;
using System;

namespace SolutionEventMonitor.Watchers
{
    public class ProjectUnloadingEventArgs : EventArgs
    {
        public IVsHierarchy RealHierarchy { get; }

        public IVsHierarchy StubHierarchy { get; }

        public ProjectUnloadingEventArgs(IVsHierarchy stubHierarchy, IVsHierarchy realHierarchy)
        {
            if (stubHierarchy == null)
            {
                throw new ArgumentNullException(nameof(stubHierarchy));
            }

            if (realHierarchy == null)
            {
                throw new ArgumentNullException(nameof(realHierarchy));
            }

            StubHierarchy = stubHierarchy;
            RealHierarchy = realHierarchy;
        }
    }
}