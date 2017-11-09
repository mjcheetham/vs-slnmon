using System;

namespace SolutionEventMonitor.Watchers
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