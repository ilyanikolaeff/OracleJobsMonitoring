using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleJobsMonitor
{
    class WindowsEventLogger : LoggerBase
    {
        public override void WriteEntry(string message, EventLogEntryType eventLogEntryType)
        {
            if (eventLogEntryType > EventLogEntryType.Warning)
                return;
            // log only errors and warnings
            try
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Oracle Jobs Monitoring";
                    eventLog.WriteEntry(message, eventLogEntryType);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Cannot log to Windows Event Log:\n{ex}");
            }
        }
    }

}
