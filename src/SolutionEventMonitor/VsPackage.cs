using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using SolutionEventMonitor.Watchers;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SolutionEventMonitor
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [Guid(Consts.PackageGuidString)]
    [ProvideAutoLoad(Consts.PackageActivationGuidString)]
    [ProvideUIContextRule(Consts.PackageActivationGuidString,
        "SolutionEventMonitorPackageActivation",
        "ShellInit",
        new[] { "ShellInit" },
        new[] { VSConstants.UICONTEXT.ShellInitialized_string })]
    [ProvideUIContextRule(Consts.HasCSharpVBProjectContextGuidString,
        "HasCSharpVBProject",
        "HasCSharpProject | HasVBProject",
        new[] { "HasCSharpProject", "HasVBProject" },
        new[] { "SolutionHasProjectCapability:CSharp", "SolutionHasProjectCapability:VB" })]
    public sealed class VsPackage : Package
    {
        private ILogger _eventLogger;
        private ISolutionWatcher _solutionWatcher;
        private UIContext _packageContext;
        private UIContext _csVbProjectContext;
        private UIContext _solutionLoadedContext;
        private UIContext _solutionExistsContext;
        private UIContext _solutionOpeningContext;

        protected override void Initialize()
        {
            var logPath = Path.Combine(Path.GetTempPath(), $"SolutionEventMonitor_{DateTime.Now:yyyyMMddTHHmmss}.log");
            _eventLogger = new FileLogger(logPath, autoFlush: true);

            _solutionWatcher = new VsSolutionWatcher(this, _eventLogger);

            _packageContext = UIContext.FromUIContextGuid(Consts.PackageActivationGuid);
            _packageContext.UIContextChanged += PackageContext_UIContextChanged;

            _csVbProjectContext = UIContext.FromUIContextGuid(Consts.HasCSharpVBProjectContextGuid);
            _csVbProjectContext.UIContextChanged += CSharpVBProjectContext_UIContextChanged;

            _solutionExistsContext = KnownUIContexts.SolutionExistsContext;
            _solutionExistsContext.UIContextChanged += SolutionExistsContext_UIContextChanged;

            _solutionLoadedContext = KnownUIContexts.SolutionExistsAndFullyLoadedContext;
            _solutionLoadedContext.UIContextChanged += SolutionLoadedContext_UIContextChanged;

            _solutionOpeningContext = KnownUIContexts.SolutionOpeningContext;
            _solutionOpeningContext.UIContextChanged += SolutionOpeningContext_UIContextChanged;

            base.Initialize();
        }

        private void PackageContext_UIContextChanged(object sender, UIContextChangedEventArgs e)
        {
            _eventLogger.Log($"PackageContext # Activated: {e.Activated}");
        }
        
        private void CSharpVBProjectContext_UIContextChanged(object sender, UIContextChangedEventArgs e)
        {
            _eventLogger.Log($"SolutionHasCSharpVBProjectContext # Activated: {e.Activated}");
        }

        private void SolutionExistsContext_UIContextChanged(object sender, UIContextChangedEventArgs e)
        {
            _eventLogger.Log($"SolutionExistsContext # Activated: {e.Activated}");
        }

        private void SolutionLoadedContext_UIContextChanged(object sender, UIContextChangedEventArgs e)
        {
            _eventLogger.Log($"SolutionFullyLoadedContext # Activated: {e.Activated}");
        }

        private void SolutionOpeningContext_UIContextChanged(object sender, UIContextChangedEventArgs e)
        {
            _eventLogger.Log($"SolutionOpeningContext # Activated: {e.Activated}");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _solutionWatcher?.Dispose();
                _solutionWatcher = null;

                _packageContext.UIContextChanged -= PackageContext_UIContextChanged;
                _packageContext = null;

                _csVbProjectContext.UIContextChanged -= CSharpVBProjectContext_UIContextChanged;
                _csVbProjectContext = null;

                _solutionExistsContext.UIContextChanged -= SolutionExistsContext_UIContextChanged;
                _solutionExistsContext = null;

                _solutionLoadedContext.UIContextChanged -= SolutionLoadedContext_UIContextChanged;
                _solutionLoadedContext = null;

                _solutionOpeningContext.UIContextChanged -= SolutionOpeningContext_UIContextChanged;
                _solutionOpeningContext = null;

                _eventLogger?.Dispose();
                _eventLogger = null;
            }

            base.Dispose(disposing);
        }
    }
}
