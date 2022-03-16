using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleJobsMonitor
{
    class OraConnectionService
    {
        private readonly string _oraConnString;
        private readonly ILogger _logger;
        private OracleConnection _oraConnection;
        public OraConnectionService(ILogger logger, string oraConnString)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _oraConnString = oraConnString ?? throw new ArgumentNullException(nameof(oraConnString));
        }
        public OracleConnection GetOracleConnection()
        {
            try
            {
                _oraConnection = new OracleConnection(_oraConnString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }

            return _oraConnection;
        }
    }
}
