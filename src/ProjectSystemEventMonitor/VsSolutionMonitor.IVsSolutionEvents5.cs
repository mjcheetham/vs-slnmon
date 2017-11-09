using System;
using Microsoft.VisualStudio.Shell.Interop;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    internal partial class VsSolutionMonitor : IVsSolutionEvents5
    {
        public void OnBeforeOpenProject(ref Guid guidProjectID, ref Guid guidProjectType, string pszFileName)
        {
            _logger?.LogEventMessage($"ProjectId: {guidProjectID}, ProjectType: {guidProjectType}, FileName: {pszFileName}");

            BeforeOpenProject?.Invoke(this, new ProjectOpeningEventArgs(guidProjectID, guidProjectType, pszFileName));
        }
    }
}
