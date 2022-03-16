using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace OracleJobsMonitor
{
    class CycleJobRunnerService
    {
        private readonly OraQueryResultAnalyzer _oraQueryResultAnalyzer;
        private readonly ILogger _logger;
        private readonly int _runPeriod;
        private Timer _cycleTimer;
        public CycleJobRunnerService(OraQueryResultAnalyzer oraQueryResultAnalyzer, ILogger logger, int runPeriod)
        {
            _oraQueryResultAnalyzer = oraQueryResultAnalyzer ?? throw new ArgumentNullException(nameof(oraQueryResultAnalyzer));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (runPeriod <= 0)
                throw new ArgumentException("Run Period must be more than 0");
            else
                _runPeriod = runPeriod;

            InitTimer();
        }

        private void InitTimer()
        {
            _cycleTimer = new Timer(_runPeriod * 1000)
            {
                AutoReset = true,
                Enabled = false
            };
            _cycleTimer.Elapsed += OnCycleTimerElapsed;
        }

        private void OnCycleTimerElapsed(object sender, ElapsedEventArgs e)
        {
            _logger.Log($"=========== Starting job ===========");
            _oraQueryResultAnalyzer.Analyze();
            _logger.Log($"=========== End job (successfull) ===========");
        }

        public void Start()
        {
            // first time call 
            OnCycleTimerElapsed(this, null);

            _cycleTimer.Enabled = true;
        }
    }
}
