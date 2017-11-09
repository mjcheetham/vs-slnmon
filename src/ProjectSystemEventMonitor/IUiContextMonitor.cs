using System;
using Microsoft.VisualStudio.Shell;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    internal interface IUiContextMonitor : IDisposable
    {
        event EventHandler<UiContextChangedEventArgs> ContextChanged;

        void AddContext(Guid contextGuid, string contextName);

        void AddContext(UIContext context, string contextName);

        void RemoveContext(Guid contextGuid);

        void RemoveContext(UIContext context);
    }
}
