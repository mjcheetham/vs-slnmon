using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    internal partial class VsSolutionMonitor : IVsSolutionLoadEvents
    {
        public int OnBeforeOpenSolution(string pszSolutionFilename)
        {
            _logger?.LogEventMessage($"FileName: {pszSolutionFilename}");

            return VSConstants.S_OK;
        }

        public int OnBeforeBackgroundSolutionLoadBegins()
        {
            _logger?.LogEvent();

            return VSConstants.S_OK;
        }

        public int OnQueryBackgroundLoadProjectBatch(out bool pfShouldDelayLoadToNextIdle)
        {
            _logger?.LogEvent();

            pfShouldDelayLoadToNextIdle = false;

            return VSConstants.S_OK;
        }

        public int OnBeforeLoadProjectBatch(bool fIsBackgroundIdleBatch)
        {
            _logger?.LogEventMessage($"IsBackgroundIdleBatch: {Convert.ToBoolean(fIsBackgroundIdleBatch)}");

            return VSConstants.S_OK;
        }

        public int OnAfterLoadProjectBatch(bool fIsBackgroundIdleBatch)
        {
            _logger?.LogEventMessage($"IsBackgroundIdleBatch: {Convert.ToBoolean(fIsBackgroundIdleBatch)}");

            return VSConstants.S_OK;
        }

        public int OnAfterBackgroundSolutionLoadComplete()
        {
            _logger?.LogEvent();

            return VSConstants.S_OK;
        }
    }
}
