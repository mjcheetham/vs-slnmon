using System;

namespace SolutionEventMonitor
{
    internal static class Consts
    {
        public const string PackageGuidString = "227607cf-6e98-4d34-a7ba-c5f6141114fe";
        public const string PackageActivationGuidString = "269080b1-2fd4-4e32-906e-b8d4d6ed0168";

        public readonly static Guid PackageGuid = new Guid(PackageGuidString);
        public readonly static Guid PackageActivationGuid = new Guid(PackageActivationGuidString);
    }
}
