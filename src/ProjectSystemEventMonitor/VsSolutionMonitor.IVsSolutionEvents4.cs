using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    internal partial class VsSolutionMonitor : IVsSolutionEvents4
    {
        public int OnAfterRenameProject(IVsHierarchy pHierarchy)
        {
            _logger.LogEventMessage($"Hierarchy: {Utils.GetName(pHierarchy)}");

            return VSConstants.S_OK;
        }

        public int OnQueryChangeProjectParent(IVsHierarchy pHierarchy, IVsHierarchy pNewParentHier, ref int pfCancel)
        {
            _logger.LogEventMessage($"Hierarchy: {Utils.GetName(pHierarchy)}, NewParentHierarchy: {Utils.GetName(pNewParentHier)}");

            return VSConstants.S_OK;
        }

        public int OnAfterChangeProjectParent(IVsHierarchy pHierarchy)
        {
            _logger.LogEventMessage($"Hierarchy: {Utils.GetName(pHierarchy)}");

            return VSConstants.S_OK;
        }

        public int OnAfterAsynchOpenProject(IVsHierarchy pHierarchy, int fAdded)
        {
            _logger.LogEventMessage($"Hierarchy: {Utils.GetName(pHierarchy)}, Added: {Convert.ToBoolean(fAdded)}");

            return VSConstants.S_OK;
        }
    }
}
