using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OracleJobsMonitor
{
    class ApplicationConfigurationLoader
    {
        private readonly ILogger _logger;
        public ApplicationConfigurationLoader(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ApplicationConfiguration LoadConfiguration(string fileName)
        {
            try
            {
                // deserialize
                var xmlSerializer = new XmlSerializer(typeof(ApplicationConfiguration));
                using (var stream = new StreamReader(fileName))
                {
                    return (ApplicationConfiguration)xmlSerializer.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return null;
            }
        }

        public void SaveConfiguration(string fileName, ApplicationConfiguration applicationConfiguration)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(ApplicationConfiguration));
                using (var stream = new StreamWriter(fileName, false))
                {
                    xmlSerializer.Serialize(stream, applicationConfiguration);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }
    }
}
