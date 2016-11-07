﻿using Microsoft.VisualStudio.Shell.Interop;
using System;

namespace SolutionEventMonitor.Watchers
{
    internal class ProjectOpenedEventArgs : EventArgs
    {
        public IVsHierarchy Hierarchy { get; }

        public bool Added { get; }

        public ProjectOpenedEventArgs(IVsHierarchy hierarchy, bool v)
        {
            if (hierarchy == null)
            {
                throw new ArgumentNullException(nameof(hierarchy));
            }

            Hierarchy = hierarchy;
            Added = v;
        }
    }
}