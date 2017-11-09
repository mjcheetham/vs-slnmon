using System;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    internal class UiContextChangedEventArgs : EventArgs
    {
        public Guid ContextId { get; set; }

        public bool IsActivated { get; }

        public UiContextChangedEventArgs(Guid contextId, bool isActivated)
        {
            ContextId = contextId;
            IsActivated = isActivated;
        }
    }
}