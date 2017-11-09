using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    internal partial class VsSolutionMonitor : IVsSolutionEvents
    {
        public int OnAfterCloseSolution(object pUnkReserved)
        {
            _logger?.LogEvent();

            AfterCloseSolution?.Invoke(this, new SolutionClosedEventArgs());
            return VSConstants.S_OK;
        }

        public int OnAfterLoadProject(IVsHierarchy pStubHierarchy, IVsHierarchy pRealHierarchy)
        {
            _logger?.LogEventMessage($"StubHierarchy: {Utils.GetName(pStubHierarchy)}, RealHierarchy: {Utils.GetName(pRealHierarchy)}");

            AfterLoadProject?.Invoke(this, new ProjectLoadedEventArgs(pStubHierarchy, pRealHierarchy));
            return VSConstants.S_OK;
        }

        public int OnAfterOpenProject(IVsHierarchy pHierarchy, int fAdded)
        {
            _logger?.LogEventMessage($"Hierarchy: {Utils.GetName(pHierarchy)}, Added: {Convert.ToBoolean(fAdded)}");

            AfterOpenProject?.Invoke(this, new ProjectOpenedEventArgs(pHierarchy, Convert.ToBoolean(fAdded)));
            return VSConstants.S_OK;
        }

        public int OnAfterOpenSolution(object pUnkReserved, int fNewSolution)
        {
            _logger?.LogEventMessage($"NewSolution: {Convert.ToBoolean(fNewSolution)}");

            AfterOpenSolution?.Invoke(this, new SolutionOpenedEventArgs(Convert.ToBoolean(fNewSolution)));
            return VSConstants.S_OK;
        }

        public int OnBeforeCloseProject(IVsHierarchy pHierarchy, int fRemoved)
        {
            _logger?.LogEventMessage($"Removed: {Convert.ToBoolean(fRemoved)}");

            BeforeCloseProject?.Invoke(this, new ProjectClosingEventArgs(Convert.ToBoolean(fRemoved)));
            return VSConstants.S_OK;
        }

        public int OnBeforeCloseSolution(object pUnkReserved)
        {
            _logger?.LogEvent();

            BeforeCloseSolution?.Invoke(this, new SolutionClosingEventArgs());
            return VSConstants.S_OK;
        }

        public int OnBeforeUnloadProject(IVsHierarchy pRealHierarchy, IVsHierarchy pStubHierarchy)
        {
            _logger?.LogEventMessage($"StubHierarchy: {Utils.GetName(pStubHierarchy)}, RealHierarchy: {Utils.GetName(pRealHierarchy)}");

            BeforeUnloadProject?.Invoke(this, new ProjectUnloadingEventArgs(pStubHierarchy, pRealHierarchy));
            return VSConstants.S_OK;
        }

        public int OnQueryCloseProject(IVsHierarchy pHierarchy, int fRemoving, ref int pfCancel)
        {
            _logger?.LogEventMessage($"Hierarchy: {Utils.GetName(pHierarchy)}, Removing: {Convert.ToBoolean(fRemoving)}");

            var eventArgs = new ProjectCloseQueryEventArgs(pHierarchy, Convert.ToBoolean(fRemoving));
            QueryCloseProject?.Invoke(this, eventArgs);

            if (eventArgs.Cancel)
            {
                pfCancel = 1;
                _logger?.LogEventMessage("Cancelled := True");
            }

            return VSConstants.S_OK;
        }

        public int OnQueryCloseSolution(object pUnkReserved, ref int pfCancel)
        {
            _logger?.LogEvent();

            var eventArgs = new SolutionCloseQueryEventArgs();
            QueryCloseSolution?.Invoke(this, eventArgs);

            if (eventArgs.Cancel)
            {
                pfCancel = 1;
                _logger?.LogEventMessage("Cancelled := True");
            }

            return VSConstants.S_OK;
        }

        public int OnQueryUnloadProject(IVsHierarchy pRealHierarchy, ref int pfCancel)
        {
            _logger?.LogEventMessage($"RealHierarchy: {Utils.GetName(pRealHierarchy)}");

            var eventArgs = new ProjectUnloadQueryEventArgs(pRealHierarchy);
            QueryUnloadProject?.Invoke(this, eventArgs);

            if (eventArgs.Cancel)
            {
                pfCancel = 1;
                _logger?.LogEventMessage("Cancelled := True");
            }

            return VSConstants.S_OK;
        }
    }
}
