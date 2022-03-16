using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OracleJobsMonitor
{
    public class ApplicationConfiguration
    {
        public OracleConnectionConfigration OraConnection;
        // query text
        public string CmdText;
        // max time (seconds) job can be running
        public int MaxRunningTime;
        // runperiod (seconds)
        public int RunPeriod;


        public string GetDbConnectionString()
        {
            // Connection String для прямого подключения к Oracle.
            return $"Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = {OraConnection.HostName})(PORT = {OraConnection.Port}))" +
                $"(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = {OraConnection.SID})));Password={OraConnection.Password};User ID={OraConnection.User}";
        }
    }

    public class OracleConnectionConfigration
    {
        // oracle connection
        [XmlAttribute]
        public string HostName;
        [XmlAttribute]
        public int Port;
        [XmlAttribute]
        public string SID;
        [XmlAttribute]
        public string User;
        [XmlAttribute]
        public string Password;
    }
}
