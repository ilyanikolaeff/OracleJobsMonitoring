using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleJobsMonitor
{
    class ConsoleLogger : LoggerBase
    {
        public override void WriteEntry(string message, EventLogEntryType eventLogEntryType)
        {
            if (eventLogEntryType == EventLogEntryType.Error)
                Console.ForegroundColor = ConsoleColor.Red;
            if (eventLogEntryType == EventLogEntryType.Information)
                Console.ForegroundColor = ConsoleColor.Green;

            
            Console.WriteLine($"{DateTime.Now} | {eventLogEntryType} | {message}");
        }
    }
}
