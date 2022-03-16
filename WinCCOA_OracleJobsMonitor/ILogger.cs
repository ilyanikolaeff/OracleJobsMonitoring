using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleJobsMonitor
{
    interface ILogger
    {
        void Log(string message);
        void LogError(Exception ex);

        void Log(string message, LogLevel logLevel);
    }

    enum LogLevel
    {
        Error,
        Info
    }
}
