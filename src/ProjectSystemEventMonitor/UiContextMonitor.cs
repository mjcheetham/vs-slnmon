using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.Shell;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    internal class UiContextMonitor : IUiContextMonitor
    {
        private readonly ILogger _logger;
        private readonly IDictionary<Guid, string> _contexts = new Dictionary<Guid, string>();

        private bool _isDisposed;

        public UiContextMonitor() { }

        public UiContextMonitor(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _logger = logger;
        }

        #region IUiContextMonitor

        public event EventHandler<UiContextChangedEventArgs> ContextChanged;

        public void AddContext(Guid contextGuid, string contextName)
        {
            AddContext(UIContext.FromUIContextGuid(contextGuid), contextName);
        }

        public void AddContext(UIContext context, string contextName)
        {
            Guid contextGuid = GetId(context);
            if (!_contexts.ContainsKey(contextGuid))
            {
                context.UIContextChanged += OnUiContextChanged;
                _contexts.Add(contextGuid, contextName);

                _logger.LogEventMessage($"Context: {contextName}, Id:{contextGuid:D}, Activated: {context.IsActive}");
            }
        }

        public void RemoveContext(Guid contextGuid)
        {
            RemoveContext(UIContext.FromUIContextGuid(contextGuid));
        }

        public void RemoveContext(UIContext context)
        {
            Guid contextGuid = GetId(context);
            if (_contexts.TryGetValue(contextGuid, out string contextName))
            {
                _contexts.Remove(contextGuid);
                context.UIContextChanged -= OnUiContextChanged;

                _logger.LogEventMessage($"Context {contextName}, Id: {contextGuid:D}, Activated: {context.IsActive}");
            }
        }

        #endregion

        private void OnUiContextChanged(object sender, UIContextChangedEventArgs e)
        {
            var context = sender as UIContext;
            Guid contextId = GetId(context);
            _contexts.TryGetValue(contextId, out string contextName);

            _logger?.LogEventMessage($"Context: {contextName}, Id: {contextId:D}, Activated: {e.Activated}");

            ContextChanged?.Invoke(this, new UiContextChangedEventArgs(contextId, e.Activated));
        }

        private static Guid GetId(UIContext context)
        {
            if (context != null)
            {
                PropertyInfo guidProperty = typeof(UIContext).GetProperty("Guid", BindingFlags.NonPublic | BindingFlags.Instance);
                if (guidProperty != null)
                {
                    return (Guid)guidProperty.GetValue(context);
                }
            }

            return Guid.Empty;
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    foreach (Guid contextGuid in _contexts.Keys.ToArray())
                    {
                        RemoveContext(contextGuid);
                    }
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
