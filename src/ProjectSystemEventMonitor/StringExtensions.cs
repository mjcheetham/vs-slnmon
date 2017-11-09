using System;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    internal static class StringExtensions
    {
        public static Guid ToGuid(this string guidString)
        {
            if (Guid.TryParse(guidString, out Guid guid))
            {
                return guid;
            }

            return Guid.Empty;
        }
    }
}
