using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Runtime.CompilerServices;

namespace SolutionEventMonitor.Watchers
{
    internal class VsSolutionWatcher : ISolutionWatcher, IVsSolutionEvents, IVsSolutionEvents5
    {
        private readonly IVsSolution _solution;
        private readonly ILogger _eventLogger;

        private uint _solutionCookie;
        private bool _isDisposed;

        #region ISolutionWatcher

        public event EventHandler<ProjectLoadedEventArgs> AfterLoadProject;

        public event EventHandler<ProjectUnloadQueryEventArgs> QueryUnloadProject;
        public event EventHandler<ProjectUnloadingEventArgs> BeforeUnloadProject;

        public event EventHandler<ProjectOpeningEventArgs> BeforeOpenProject;
        public event EventHandler<ProjectOpenedEventArgs> AfterOpenProject;

        public event EventHandler<ProjectCloseQueryEventArgs> QueryCloseProject;
        public event EventHandler<ProjectClosingEventArgs> BeforeCloseProject;

        public event EventHandler<SolutionCloseQueryEventArgs> QueryCloseSolution;
        public event EventHandler<SolutionClosingEventArgs> BeforeCloseSolution;
        public event EventHandler<SolutionClosedEventArgs> AfterCloseSolution;

        public event EventHandler<SolutionOpenedEventArgs> AfterOpenSolution;

        #endregion

        #region Constructors

        public VsSolutionWatcher(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            _solution = serviceProvider.GetService<SVsSolution, IVsSolution>();
            if (_solution == null)
            {
                throw new ArgumentException("Failed to acquire solution service from service provider.", nameof(serviceProvider));
            }

            StartWatching();
        }

        public VsSolutionWatcher(IServiceProvider serviceProvider, ILogger logger)
            : this(serviceProvider)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _eventLogger = logger;
        }

        #endregion

        private void StartWatching()
        {
            ErrorHandler.ThrowOnFailure(_solution.AdviseSolutionEvents(this, out _solutionCookie));
        }

        private void StopWatching()
        {
            ErrorHandler.ThrowOnFailure(_solution.UnadviseSolutionEvents(_solutionCookie));
        }

        #region IVsSolutionEvents

        public int OnAfterCloseSolution(object pUnkReserved)
        {
            LogEvent();

            AfterCloseSolution?.Invoke(this, new SolutionClosedEventArgs());
            return VSConstants.S_OK;
        }

        public int OnAfterLoadProject(IVsHierarchy pStubHierarchy, IVsHierarchy pRealHierarchy)
        {
            LogEventMessage($"StubHierarchy: {Utils.GetName(pStubHierarchy)}, RealHierarchy: {Utils.GetName(pRealHierarchy)}");

            AfterLoadProject?.Invoke(this, new ProjectLoadedEventArgs(pStubHierarchy, pRealHierarchy));
            return VSConstants.S_OK;
        }

        public int OnAfterOpenProject(IVsHierarchy pHierarchy, int fAdded)
        {
            LogEventMessage($"Hierarchy: {Utils.GetName(pHierarchy)}, Added: {Convert.ToBoolean(fAdded)}");

            AfterOpenProject?.Invoke(this, new ProjectOpenedEventArgs(pHierarchy, Convert.ToBoolean(fAdded)));
            return VSConstants.S_OK;
        }

        public int OnAfterOpenSolution(object pUnkReserved, int fNewSolution)
        {
            LogEventMessage($"NewSolution: {Convert.ToBoolean(fNewSolution)}");

            AfterOpenSolution?.Invoke(this, new SolutionOpenedEventArgs(Convert.ToBoolean(fNewSolution)));
            return VSConstants.S_OK;
        }

        public int OnBeforeCloseProject(IVsHierarchy pHierarchy, int fRemoved)
        {
            LogEventMessage($"Removed: {Convert.ToBoolean(fRemoved)}");

            BeforeCloseProject?.Invoke(this, new ProjectClosingEventArgs(Convert.ToBoolean(fRemoved)));
            return VSConstants.S_OK;
        }

        public int OnBeforeCloseSolution(object pUnkReserved)
        {
            LogEvent();

            BeforeCloseSolution?.Invoke(this, new SolutionClosingEventArgs());
            return VSConstants.S_OK;
        }

        public int OnBeforeUnloadProject(IVsHierarchy pRealHierarchy, IVsHierarchy pStubHierarchy)
        {
            LogEventMessage($"StubHierarchy: {Utils.GetName(pStubHierarchy)}, RealHierarchy: {Utils.GetName(pRealHierarchy)}");

            BeforeUnloadProject?.Invoke(this, new ProjectUnloadingEventArgs(pStubHierarchy, pRealHierarchy));
            return VSConstants.S_OK;
        }

        public int OnQueryCloseProject(IVsHierarchy pHierarchy, int fRemoving, ref int pfCancel)
        {
            LogEventMessage($"Hierarchy: {Utils.GetName(pHierarchy)}, Removing: {Convert.ToBoolean(fRemoving)}");

            var eventArgs = new ProjectCloseQueryEventArgs(pHierarchy, Convert.ToBoolean(fRemoving));
            QueryCloseProject?.Invoke(this, eventArgs);

            if (eventArgs.Cancel)
            {
                pfCancel = 1;
                LogEventMessage("Cancelled := true");
            }

            return VSConstants.S_OK;
        }

        public int OnQueryCloseSolution(object pUnkReserved, ref int pfCancel)
        {
            LogEvent();

            var eventArgs = new SolutionCloseQueryEventArgs();
            QueryCloseSolution?.Invoke(this, eventArgs);

            if (eventArgs.Cancel)
            {
                pfCancel = 1;
                LogEventMessage("Cancelled := true");
            }

            return VSConstants.S_OK;
        }

        public int OnQueryUnloadProject(IVsHierarchy pRealHierarchy, ref int pfCancel)
        {
            LogEventMessage($"RealHierarchy: {Utils.GetName(pRealHierarchy)}");

            var eventArgs = new ProjectUnloadQueryEventArgs(pRealHierarchy);
            QueryUnloadProject?.Invoke(this, eventArgs);

            if (eventArgs.Cancel)
            {
                pfCancel = 1;
                LogEventMessage("Cancelled := true");
            }

            return VSConstants.S_OK;
        }

        #endregion

        #region IVsSolutionEvents5

        public void OnBeforeOpenProject(ref Guid guidProjectID, ref Guid guidProjectType, string pszFileName)
        {
            BeforeOpenProject?.Invoke(this, new ProjectOpeningEventArgs(guidProjectID, guidProjectType, pszFileName));
        }

        #endregion

        private void LogEvent([CallerMemberName] string caller = null)
        {
            _eventLogger?.Log(caller);
        }

        private void LogEventMessage(string message, [CallerMemberName] string caller = null)
        {
            _eventLogger?.Log($"{caller} # {message}");
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    StopWatching();
                }

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
