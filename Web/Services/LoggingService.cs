using System;
using log4net;
using PersonalBlog.Web.Interfaces;

namespace PersonalBlog.Web.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class LoggingService : ILoggingService
    {
        /// <summary>
        /// 
        /// </summary>
        public LoggingService()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Debug(string message, Exception ex)
        {
            _log.Debug(message,ex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
            _log.Debug(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Info(string message, Exception ex)
        {
            _log.Info(message,ex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            _log.Info(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Warn(string message, Exception ex)
        {
            _log.Warn(message,ex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Warn(string message)
        {
            _log.Warn(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Error(string message, Exception ex)
        {
            _log.Error(message,ex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            _log.Error(message);
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly ILog _log;
    }
}