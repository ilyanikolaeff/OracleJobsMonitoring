using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleJobsMonitor
{
    class CommonLogger : LoggerBase
    {
        private readonly IEnumerable<ILogger> _loggers;
        public CommonLogger(IEnumerable<ILogger> loggers)
        {
            _loggers = loggers ?? throw new ArgumentNullException(nameof(loggers));
        }

        public override void WriteEntry(string message, EventLogEntryType eventLogEntryType)
        {
            foreach (var logger in _loggers)
            {
                (logger as LoggerBase).WriteEntry(message, eventLogEntryType);
            }
        }
    }
}
