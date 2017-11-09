using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    internal partial class VsSolutionMonitor : IVsSolutionEvents2
    {
        public int OnAfterMergeSolution(object pUnkReserved)
        {
            _logger.LogEvent();

            return VSConstants.S_OK;
        }
    }
}
