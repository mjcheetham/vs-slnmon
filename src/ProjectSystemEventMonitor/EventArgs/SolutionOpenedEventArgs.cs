using System;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    internal class SolutionOpenedEventArgs : EventArgs
    {
        public bool NewSolution { get; }

        public SolutionOpenedEventArgs(bool newSolution)
        {
            NewSolution = newSolution;
        }
    }
}