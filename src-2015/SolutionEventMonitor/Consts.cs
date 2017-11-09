using System;

namespace SolutionEventMonitor
{
    internal static class Consts
    {
        public const string PackageGuidString = "227607cf-6e98-4d34-a7ba-c5f6141114fe";
        public const string PackageActivationGuidString = "269080b1-2fd4-4e32-906e-b8d4d6ed0168";
        public const string HasCSharpVBProjectContextGuidString = "71b25c70-e48b-438a-9a70-d61301c70483";

        public readonly static Guid PackageGuid = new Guid(PackageGuidString);
        public readonly static Guid PackageActivationGuid = new Guid(PackageActivationGuidString);
        public readonly static Guid HasCSharpVBProjectContextGuid = new Guid(HasCSharpVBProjectContextGuidString);
    }
}
