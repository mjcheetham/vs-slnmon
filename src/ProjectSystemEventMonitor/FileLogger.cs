using System;
using System.IO;
using System.Text;

namespace Mjcheetham.VisualStudio.ProjectSystemMonitor
{
    internal class FileLogger : ILogger
    {
        private readonly StreamWriter _writer;

        private bool _isDisposed;

        public FileLogger(string logBasePath, bool autoFlush = false)
        {
            if (string.IsNullOrWhiteSpace(logBasePath))
            {
                throw new ArgumentNullException(nameof(logBasePath));
            }

            string dateStr = DateTime.UtcNow.ToString("yyyy-MM-dd_HHmmss");
            LogPath = Path.Combine(logBasePath, $"ProjectSystemEvents_{dateStr}.log");

            _writer = new StreamWriter(LogPath, false, Encoding.UTF8)
            {
                AutoFlush = autoFlush
            };
        }

        public string LogPath { get; }

        public void Log(string message)
        {
            _writer.WriteLine($"{DateTime.UtcNow:u}\t{message}");
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _writer.Flush();
                    _writer.Dispose();
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
