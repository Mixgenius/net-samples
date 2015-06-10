using System;

namespace Fusebill.ApiWrapper.Contracts
{
    public interface ILog
    {
        bool IsDebugEnabled { get; }
        //
        // Summary:
        //     Checks if this logger is enabled for the Common.Logging.LogLevel.Error level.
        bool IsErrorEnabled { get; }
        //
        // Summary:
        //     Checks if this logger is enabled for the Common.Logging.LogLevel.Fatal level.
        bool IsFatalEnabled { get; }
        //
        // Summary:
        //     Checks if this logger is enabled for the Common.Logging.LogLevel.Info level.
        bool IsInfoEnabled { get; }
        //
        // Summary:
        //     Checks if this logger is enabled for the Common.Logging.LogLevel.Trace level.
        bool IsTraceEnabled { get; }
        //
        // Summary:
        //     Checks if this logger is enabled for the Common.Logging.LogLevel.Warn level.
        bool IsWarnEnabled { get; }

        //
        // Summary:
        //     Log a message object with the Common.Logging.LogLevel.Debug level.
        //
        // Parameters:
        //   message:
        //     The message object to log.
        void Debug(object message);
        //
        // Summary:
        //     Log a message object with the Common.Logging.LogLevel.Debug level including
        //     the stack trace of the System.Exception passed as a parameter.
        //
        // Parameters:
        //   message:
        //     The message object to log.
        //
        //   exception:
        //     The exception to log, including its stack trace.
        void Debug(object message, Exception exception);
  
        void DebugFormat(string format, params object[] args);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Debug level.
        //
        // Parameters:
        //   formatProvider:
        //     An System.IFormatProvider that supplies culture-specific formatting information.
        //
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   args:
        void DebugFormat(IFormatProvider formatProvider, string format, params object[] args);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Debug level.
        //
        // Parameters:
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   exception:
        //     The exception to log.
        //
        //   args:
        //     the list of format arguments
        void DebugFormat(string format, Exception exception, params object[] args);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Debug level.
        //
        // Parameters:
        //   formatProvider:
        //     An System.IFormatProvider that supplies culture-specific formatting information.
        //
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   exception:
        //     The exception to log.
        //
        //   args:
        void DebugFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args);
        //
        // Summary:
        //     Log a message object with the Common.Logging.LogLevel.Error level.
        //
        // Parameters:
        //   message:
        //     The message object to log.
        void Error(object message);
        //
        // Summary:
        //     Log a message object with the Common.Logging.LogLevel.Error level including
        //     the stack trace of the System.Exception passed as a parameter.
        //
        // Parameters:
        //   message:
        //     The message object to log.
        //
        //   exception:
        //     The exception to log, including its stack trace.
        void Error(object message, Exception exception);
  
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Error level.
        //
        // Parameters:
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   args:
        //     the list of format arguments
        void ErrorFormat(string format, params object[] args);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Error level.
        //
        // Parameters:
        //   formatProvider:
        //     An System.IFormatProvider that supplies culture-specific formatting information.
        //
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   args:
        void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Error level.
        //
        // Parameters:
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   exception:
        //     The exception to log.
        //
        //   args:
        //     the list of format arguments
        void ErrorFormat(string format, Exception exception, params object[] args);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Error level.
        //
        // Parameters:
        //   formatProvider:
        //     An System.IFormatProvider that supplies culture-specific formatting information.
        //
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   exception:
        //     The exception to log.
        //
        //   args:
        void ErrorFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args);
  
        //
        // Summary:
        //     Log a message object with the Common.Logging.LogLevel.Fatal level.
        //
        // Parameters:
        //   message:
        //     The message object to log.
        void Fatal(object message);
     
        //
        // Summary:
        //     Log a message object with the Common.Logging.LogLevel.Fatal level including
        //     the stack trace of the System.Exception passed as a parameter.
        //
        // Parameters:
        //   message:
        //     The message object to log.
        //
        //   exception:
        //     The exception to log, including its stack trace.
        void Fatal(object message, Exception exception);
      
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Fatal level.
        //
        // Parameters:
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   args:
        //     the list of format arguments
        void FatalFormat(string format, params object[] args);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Fatal level.
        //
        // Parameters:
        //   formatProvider:
        //     An System.IFormatProvider that supplies culture-specific formatting information.
        //
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   args:
        void FatalFormat(IFormatProvider formatProvider, string format, params object[] args);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Fatal level.
        //
        // Parameters:
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   exception:
        //     The exception to log.
        //
        //   args:
        //     the list of format arguments
        void FatalFormat(string format, Exception exception, params object[] args);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Fatal level.
        //
        // Parameters:
        //   formatProvider:
        //     An System.IFormatProvider that supplies culture-specific formatting information.
        //
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   exception:
        //     The exception to log.
        //
        //   args:
        void FatalFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args);
      
        //
        // Summary:
        //     Log a message object with the Common.Logging.LogLevel.Info level.
        //
        // Parameters:
        //   message:
        //     The message object to log.
        void Info(object message);
     
        //
        // Summary:
        //     Log a message object with the Common.Logging.LogLevel.Info level including
        //     the stack trace of the System.Exception passed as a parameter.
        //
        // Parameters:
        //   message:
        //     The message object to log.
        //
        //   exception:
        //     The exception to log, including its stack trace.
        void Info(object message, Exception exception);
     
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Info level.
        //
        // Parameters:
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   args:
        //     the list of format arguments
        void InfoFormat(string format, params object[] args);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Info level.
        //
        // Parameters:
        //   formatProvider:
        //     An System.IFormatProvider that supplies culture-specific formatting information.
        //
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   args:
        void InfoFormat(IFormatProvider formatProvider, string format, params object[] args);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Info level.
        //
        // Parameters:
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   exception:
        //     The exception to log.
        //
        //   args:
        //     the list of format arguments
        void InfoFormat(string format, Exception exception, params object[] args);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Info level.
        //
        // Parameters:
        //   formatProvider:
        //     An System.IFormatProvider that supplies culture-specific formatting information.
        //
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   exception:
        //     The exception to log.
        //
        //   args:
        void InfoFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args);
      
        //
        // Summary:
        //     Log a message object with the Common.Logging.LogLevel.Trace level.
        //
        // Parameters:
        //   message:
        //     The message object to log.
        void Trace(object message);
       
        //
        // Summary:
        //     Log a message object with the Common.Logging.LogLevel.Trace level including
        //     the stack trace of the System.Exception passed as a parameter.
        //
        // Parameters:
        //   message:
        //     The message object to log.
        //
        //   exception:
        //     The exception to log, including its stack trace.
        void Trace(object message, Exception exception);
       
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Trace level.
        //
        // Parameters:
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   args:
        //     the list of format arguments
        void TraceFormat(string format, params object[] args);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Trace level.
        //
        // Parameters:
        //   formatProvider:
        //     An System.IFormatProvider that supplies culture-specific formatting information.
        //
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   args:
        void TraceFormat(IFormatProvider formatProvider, string format, params object[] args);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Trace level.
        //
        // Parameters:
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   exception:
        //     The exception to log.
        //
        //   args:
        //     the list of format arguments
        void TraceFormat(string format, Exception exception, params object[] args);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Trace level.
        //
        // Parameters:
        //   formatProvider:
        //     An System.IFormatProvider that supplies culture-specific formatting information.
        //
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   exception:
        //     The exception to log.
        //
        //   args:
        void TraceFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args);
        //
        // Summary:
        //     Log a message object with the Common.Logging.LogLevel.Warn level.
        //
        // Parameters:
        //   message:
        //     The message object to log.
        void Warn(object message);
        //
        // Summary:
        //     Log a message object with the Common.Logging.LogLevel.Warn level including
        //     the stack trace of the System.Exception passed as a parameter.
        //
        // Parameters:
        //   message:
        //     The message object to log.
        //
        //   exception:
        //     The exception to log, including its stack trace.
        void Warn(object message, Exception exception);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Warn level.
        //
        // Parameters:
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   args:
        //     the list of format arguments
        void WarnFormat(string format, params object[] args);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Warn level.
        //
        // Parameters:
        //   formatProvider:
        //     An System.IFormatProvider that supplies culture-specific formatting information.
        //
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   args:
        void WarnFormat(IFormatProvider formatProvider, string format, params object[] args);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Warn level.
        //
        // Parameters:
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   exception:
        //     The exception to log.
        //
        //   args:
        //     the list of format arguments
        void WarnFormat(string format, Exception exception, params object[] args);
        //
        // Summary:
        //     Log a message with the Common.Logging.LogLevel.Warn level.
        //
        // Parameters:
        //   formatProvider:
        //     An System.IFormatProvider that supplies culture-specific formatting information.
        //
        //   format:
        //     The format of the message object to log.System.String.Format(System.String,System.Object[])
        //
        //   exception:
        //     The exception to log.
        //
        //   args:
        void WarnFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args);
    }
}
