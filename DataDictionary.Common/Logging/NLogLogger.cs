using NLog;
using System;


namespace DataDictionary.Common.Logging
{
    public class NLogLogger : ILogger
    {
        private readonly Logger _logger;

        public NLogLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception ex)
        {
            Error(LogUtility.BuildExceptionMessage(ex));
        }

        public void Error(string message, Exception ex)
        {
            _logger.ErrorException(message, ex);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception ex)
        {
            Fatal(LogUtility.BuildExceptionMessage(ex));
        }


        public void Error(Exception ex, string message)
        {
            _logger.Error(message, ex);
        }

        public void Fatal(Exception ex, string message)
        {
            _logger.Fatal(message, ex);
        }
    }
}
