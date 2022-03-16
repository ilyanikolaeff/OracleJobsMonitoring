using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleJobsMonitor
{
    class OraQueryResultAnalyzer
    {
        private readonly OraQueryDataExecutor _oraQueryDataExecutor;
        private readonly ILogger _logger;
        private readonly int _maxRunnigTime;
        public OraQueryResultAnalyzer(OraQueryDataExecutor oraQueryDataExecutor, ILogger logger, int maxRunningTime)
        {
            _oraQueryDataExecutor = oraQueryDataExecutor ?? throw new ArgumentNullException(nameof(oraQueryDataExecutor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _maxRunnigTime = maxRunningTime;
        }

        public void Analyze()
        {
            try
            {
                var dataTableResult = _oraQueryDataExecutor.ExecuteQuery();
                if (dataTableResult != null)
                {
                    var currDateTime = DateTime.Now;

                    // 2 column (zero-based) - last_start_date
                    // 1 column (zero-based)  - state
                    foreach (DataRow dataRow in dataTableResult.Rows)
                    {
                        var currJobName = dataRow.ItemArray[0].ToString();
                        var currJobState = dataRow.ItemArray[1].ToString();
                        var currJobLastStartDate = DateTime.Parse(dataRow.ItemArray[2].ToString());

                        if (currJobState.Equals("running", StringComparison.OrdinalIgnoreCase))
                        {
                            var dateTimeDiff = TimeSpan.FromTicks(currDateTime.Ticks - currJobLastStartDate.Ticks).TotalSeconds;
                            if (dateTimeDiff >= _maxRunnigTime)
                            {
                                _logger.Log($"Job name: {currJobName}. Max run duration error!\nStart date time: {currJobLastStartDate}, " +
                                    $"execution time: {dateTimeDiff} (possible max_duration = {_maxRunnigTime})\nImmediately call Makogon K.N. (aka chert sdku)", LogLevel.Error);
                            }
                        }
                        _logger.Log($"JobName = {currJobName}, JobState = {currJobState}, JobLastStartDate = {currJobLastStartDate}");
                    }
                }
                else
                {
                    _logger.Log("Query result is null");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }
    }
}
