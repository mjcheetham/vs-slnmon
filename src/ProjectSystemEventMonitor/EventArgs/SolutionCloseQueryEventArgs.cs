using System;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    internal class SolutionCloseQueryEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
    }
}