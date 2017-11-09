using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    internal partial class VsSolutionMonitor : IVsSolutionEvents3
    {
        public int OnBeforeOpeningChildren(IVsHierarchy pHierarchy)
        {
            _logger.LogEventMessage($"Hierarchy: {Utils.GetName(pHierarchy)}");

            return VSConstants.S_OK;
        }

        public int OnAfterOpeningChildren(IVsHierarchy pHierarchy)
        {
            _logger.LogEventMessage($"Hierarchy: {Utils.GetName(pHierarchy)}");

            return VSConstants.S_OK;
        }

        public int OnBeforeClosingChildren(IVsHierarchy pHierarchy)
        {
            _logger.LogEventMessage($"Hierarchy: {Utils.GetName(pHierarchy)}");

            return VSConstants.S_OK;
        }

        public int OnAfterClosingChildren(IVsHierarchy pHierarchy)
        {
            _logger.LogEventMessage($"Hierarchy: {Utils.GetName(pHierarchy)}");

            return VSConstants.S_OK;
        }
    }
}
