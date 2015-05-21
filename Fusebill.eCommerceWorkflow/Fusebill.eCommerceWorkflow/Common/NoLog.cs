using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fusebill.ApiWrapper;

namespace Fusebill.eCommerceWorkflow.Common
{
    /// <summary>
    /// Replace with your preferred logging framework!
    /// </summary>
    public class NoLog :ILog
    {
        public bool IsDebugEnabled { get; private set; }
        public bool IsErrorEnabled { get; private set; }
        public bool IsFatalEnabled { get; private set; }
        public bool IsInfoEnabled { get; private set; }
        public bool IsTraceEnabled { get; private set; }
        public bool IsWarnEnabled { get; private set; }
        public void Debug(object message)
        {
            //throw new NotImplementedException();
        }

        public void Debug(object message, Exception exception)
        {
            //throw new NotImplementedException();
        }

        public void DebugFormat(string format, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void DebugFormat(string format, Exception exception, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void DebugFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void Error(object message)
        {
            //throw new NotImplementedException();
        }

        public void Error(object message, Exception exception)
        {
            //throw new NotImplementedException();
        }

        public void ErrorFormat(string format, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void ErrorFormat(string format, Exception exception, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void Fatal(object message)
        {
            //throw new NotImplementedException();
        }

        public void Fatal(object message, Exception exception)
        {
            //throw new NotImplementedException();
        }

        public void FatalFormat(string format, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void FatalFormat(string format, Exception exception, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void Info(object message)
        {
            //throw new NotImplementedException();
        }

        public void Info(object message, Exception exception)
        {
            //throw new NotImplementedException();
        }

        public void InfoFormat(string format, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void InfoFormat(string format, Exception exception, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void Trace(object message)
        {
            //throw new NotImplementedException();
        }

        public void Trace(object message, Exception exception)
        {
            //throw new NotImplementedException();
        }

        public void TraceFormat(string format, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void TraceFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void TraceFormat(string format, Exception exception, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void TraceFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void Warn(object message)
        {
            //throw new NotImplementedException();
        }

        public void Warn(object message, Exception exception)
        {
            //throw new NotImplementedException();
        }

        public void WarnFormat(string format, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void WarnFormat(string format, Exception exception, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            //throw new NotImplementedException();
        }
    }
}