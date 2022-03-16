using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleJobsMonitor
{
    class OraQueryDataExecutor
    {
        private readonly OraConnectionService _oraConnectionService;
        private readonly string _queryString;
        private readonly ILogger _logger;

        public OraQueryDataExecutor(ILogger logger, OraConnectionService oraConnectionSerivce, string query)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _oraConnectionService = oraConnectionSerivce ?? throw new ArgumentNullException(nameof(oraConnectionSerivce));
            _queryString = query ?? throw new ArgumentNullException(nameof(query));
        }


        public DataTable ExecuteQuery()
        {
            try
            {
                var connection = _oraConnectionService.GetOracleConnection();
                connection.Open();
                _logger.Log($"Connection to Oracle opened");

                OracleCommand oraCmd = new OracleCommand
                {
                    Connection = connection,
                    CommandText = _queryString,
                    CommandType = CommandType.Text
                };
                OracleDataReader oraDataReader = oraCmd.ExecuteReader();

                var oraCmdDataTableResult = new DataTable();
                oraCmdDataTableResult.Load(oraDataReader);

                _logger.Log($"CMD = [{_queryString}], result:\n{oraCmdDataTableResult.GetAsString()}");

                connection.Close();
                _logger.Log($"Connection to Oracle closed");

                return oraCmdDataTableResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return null;
            }
        }

    }
}
