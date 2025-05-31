using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintSiteBuilder.Models.Print
{
    public class PrintConfig
    {
        public PrintConfig() { }

        public HeaderConfig headerConfig { get; set; }
        public List<CellConfig> cellConfigs { get; set; }

        public PrintConfig(HeaderConfig _headerConfig, List<CellConfig> _cellConfigs)
        {
            headerConfig = _headerConfig;
            cellConfigs = _cellConfigs;
        }
    }

}
