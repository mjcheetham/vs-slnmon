using System;
using System.IO;
using System.Text;

namespace SolutionEventMonitor
{
    internal class FileLogger : ILogger
    {
        private readonly StreamWriter _writer;

        private bool _isDisposed;

        public FileLogger(string logPath, bool autoFlush = false)
        {
            if (string.IsNullOrWhiteSpace(logPath))
            {
                throw new ArgumentNullException(nameof(logPath));
            }

            _writer = new StreamWriter(logPath, false, Encoding.UTF8);
            _writer.AutoFlush = autoFlush;
        }

        public void Log(string message)
        {
            _writer.WriteLine(message);
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
