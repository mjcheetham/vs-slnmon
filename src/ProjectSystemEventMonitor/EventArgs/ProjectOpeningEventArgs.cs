using System;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    public class ProjectOpeningEventArgs : EventArgs
    {
        public Guid ProjectId { get; }

        public Guid ProjectType { get; }

        public string FilePath { get; }

        public ProjectOpeningEventArgs(Guid projectId, Guid projectType, string filePath)
        {
            if (projectId == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(projectId));
            }

            if (projectType == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(projectType));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            ProjectId = projectId;
            ProjectType = projectType;
            FilePath = filePath;
        }
    }
}
