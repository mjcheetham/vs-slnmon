using System;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    internal static class Consts
    {
        public const string PackageGuidString = "39CAAFB4-8190-4486-8B66-17C8DF62E2C8";
        public const string PackageActivationGuidString = "D2D8FFC4-E7E1-4875-BC90-8566141A8281";
        public const string HasCSharpVbProjectContextGuidString = "71B25C70-E48B-438A-9A70-D61301C70483";

        public static readonly Guid PackageGuid = new Guid(PackageGuidString);
        public static readonly Guid PackageActivationGuid = new Guid(PackageActivationGuidString);
        public static readonly Guid HasCSharpVbProjectContextGuid = new Guid(HasCSharpVbProjectContextGuidString);
    }
}
