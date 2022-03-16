using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleJobsMonitor
{
    abstract class LoggerBase : ILogger
    {
        public void Log(string message)
        {
            WriteEntry(message, EventLogEntryType.Information);
        }

        public void Log(string message, LogLevel logLevel)
        {
            EventLogEntryType eventLogEntryType = EventLogEntryType.Information;
            if (logLevel == LogLevel.Info)
                eventLogEntryType = EventLogEntryType.Information;
            if (logLevel == LogLevel.Error)
                eventLogEntryType = EventLogEntryType.Error;
            WriteEntry(message, eventLogEntryType);
        }

        public void LogError(Exception ex)
        {
            WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }

        public abstract void WriteEntry(string message, EventLogEntryType eventLogEntryType);
    }
}
