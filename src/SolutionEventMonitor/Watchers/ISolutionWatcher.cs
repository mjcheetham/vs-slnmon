using System;

namespace SolutionEventMonitor.Watchers
{
    internal interface ISolutionWatcher : IDisposable
    {
        event EventHandler<ProjectLoadedEventArgs> AfterLoadProject;

        event EventHandler<ProjectUnloadQueryEventArgs> QueryUnloadProject;
        event EventHandler<ProjectUnloadingEventArgs> BeforeUnloadProject;

        event EventHandler<ProjectOpeningEventArgs> BeforeOpenProject;
        event EventHandler<ProjectOpenedEventArgs> AfterOpenProject;

        event EventHandler<ProjectCloseQueryEventArgs> QueryCloseProject;
        event EventHandler<ProjectClosingEventArgs> BeforeCloseProject;

        event EventHandler<SolutionCloseQueryEventArgs> QueryCloseSolution;
        event EventHandler<SolutionClosingEventArgs> BeforeCloseSolution;
        event EventHandler<SolutionClosedEventArgs> AfterCloseSolution;

        event EventHandler<SolutionOpenedEventArgs> AfterOpenSolution;
    }
}
