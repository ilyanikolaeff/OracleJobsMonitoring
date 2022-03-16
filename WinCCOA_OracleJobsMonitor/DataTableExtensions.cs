using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleJobsMonitor
{
    public static class DataTableExtensions
    {
        public static string GetAsString(this DataTable dataTable)
        {
            var sb = new StringBuilder();
            sb.AppendLine("(start)");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                sb.AppendLine(string.Join("|", dataRow.ItemArray));
            }
            sb.Append("(end)");
            return sb.ToString();
        }
    }
}
