using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    internal static class Utils
    {
        public static string GetName(IVsHierarchy hierarchy)
        {
            object pvar;
            int hr = hierarchy.GetProperty((uint)VSConstants.VSITEMID.Root, (int)__VSHPROPID.VSHPROPID_Name, out pvar);

            if (ErrorHandler.Succeeded(hr))
            {
                return pvar as string;
            }

            return null;
        }
    }
}
