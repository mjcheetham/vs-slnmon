using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using System;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    internal partial class VsSolutionMonitor : ISolutionMonitor
    {
        private readonly IVsSolution _solution;
        private readonly ILogger _logger;

        private uint _solutionCookie;
        private bool _isDisposed;

        #region ISolutionWatcher

        public event EventHandler<ProjectLoadedEventArgs> AfterLoadProject;

        public event EventHandler<ProjectUnloadQueryEventArgs> QueryUnloadProject;
        public event EventHandler<ProjectUnloadingEventArgs> BeforeUnloadProject;

        public event EventHandler<ProjectOpeningEventArgs> BeforeOpenProject;
        public event EventHandler<ProjectOpenedEventArgs> AfterOpenProject;

        public event EventHandler<ProjectCloseQueryEventArgs> QueryCloseProject;
        public event EventHandler<ProjectClosingEventArgs> BeforeCloseProject;

        public event EventHandler<SolutionCloseQueryEventArgs> QueryCloseSolution;
        public event EventHandler<SolutionClosingEventArgs> BeforeCloseSolution;
        public event EventHandler<SolutionClosedEventArgs> AfterCloseSolution;

        public event EventHandler<SolutionOpenedEventArgs> AfterOpenSolution;

        #endregion

        #region Constructors

        public VsSolutionMonitor(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            _solution = serviceProvider.GetService<SVsSolution, IVsSolution>();
            if (_solution == null)
            {
                throw new ArgumentException("Failed to acquire solution service from service provider.", nameof(serviceProvider));
            }

            StartWatching();
        }

        public VsSolutionMonitor(IServiceProvider serviceProvider, ILogger logger)
            : this(serviceProvider)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _logger = logger;
        }

        #endregion

        private void StartWatching()
        {
            ErrorHandler.ThrowOnFailure(_solution.AdviseSolutionEvents(this, out _solutionCookie));
        }

        private void StopWatching()
        {
            ErrorHandler.ThrowOnFailure(_solution.UnadviseSolutionEvents(_solutionCookie));
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    StopWatching();
                }

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
