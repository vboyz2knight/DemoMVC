using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoMVC.Classes;
using NLog;

namespace DemoMVC.Loggers
{
    public class NLogLogger : IMyLogger
    {
        private readonly Logger logger;

        public NLogLogger()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Trace(string message)
        {
            logger.Trace(message);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            //logger.ErrorException(message, exception);
            logger.Error(exception, message, LogUtility.BuildExceptionMessage(exception));
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public void Fatal(string message, Exception exception)
        {
            //logger.FatalException(message, exception);
            logger.Fatal(exception, message, LogUtility.BuildExceptionMessage(exception));
        }
    }
}