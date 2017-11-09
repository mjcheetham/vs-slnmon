using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [Guid(Consts.PackageGuidString)]
    [ProvideAutoLoad(Consts.PackageActivationGuidString)]
    [ProvideUIContextRule(Consts.PackageActivationGuidString,
        "ProjectSystemEventMonitorPackageActivation",
        "ShellInit",
        new[] { "ShellInit" },
        new[] { VSConstants.UICONTEXT.ShellInitialized_string })]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class MonitorPackage : Package
    {
        #region Package Members

        private ILogger _logger;
        private ISolutionMonitor _solutionMonitor;
        private UiContextMonitor _uiContextMonitor;

        protected override void Initialize()
        {
            _logger = new FileLogger(Path.GetTempPath(), autoFlush: true);
            _solutionMonitor = new VsSolutionMonitor(this, _logger);
            _uiContextMonitor = new UiContextMonitor(_logger);

            _uiContextMonitor.AddContext(KnownUIContexts.NoSolutionContext, "NoSolution");
            _uiContextMonitor.AddContext(KnownUIContexts.SolutionOpeningContext, "SolutionOpening");
            _uiContextMonitor.AddContext(KnownUIContexts.SolutionExistsContext, "SolutionExists");
            _uiContextMonitor.AddContext(KnownUIContexts.SolutionExistsAndFullyLoadedContext, "SolutionExistsAndFullyLoaded");
            _uiContextMonitor.AddContext(KnownUIContexts.SolutionExistsAndNotBuildingAndNotDebuggingContext, "SolutionExistsAndNotBuildingAndNotDebugging");
            _uiContextMonitor.AddContext(KnownUIContexts.SolutionBuildingContext, "SolutionBuilding");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uiContextMonitor?.Dispose();
                _uiContextMonitor = null;

                _solutionMonitor?.Dispose();
                _solutionMonitor = null;

                _logger?.Dispose();
                _logger = null;
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}
