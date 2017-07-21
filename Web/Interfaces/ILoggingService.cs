using System;

namespace PersonalBlog.Web.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Debug(string message, Exception ex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Info(string message, Exception ex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Info(string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Warn(string message, Exception ex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Warn(string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Error(string message, Exception ex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);
    }
}
