using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace OracleJobsMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggers = new List<ILogger>()
            {
                new WindowsEventLogger(),
                new ConsoleLogger()
            };
            var logger = new CommonLogger(loggers);

            if (args.Length == 0 || args[0] != "-testmode")
            {
                var settings = new ApplicationConfigurationLoader(logger).LoadConfiguration("Settings.xml");
                // root composition
                var cycleJobRunnerService = new CycleJobRunnerService(
                    new OraQueryResultAnalyzer(
                        new OraQueryDataExecutor(
                            logger,
                            new OraConnectionService(logger, settings.GetDbConnectionString()), settings.CmdText),
                        logger, settings.MaxRunningTime),
                    logger,
                    settings.RunPeriod);

                cycleJobRunnerService.Start();
                Console.ReadKey();
            }
            else
            {
                logger.Log("Test message", LogLevel.Error);
            }
        }

    }

 
}
