using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserHistoryServer
{
    class Export_Excel
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string IP { get; set; }

        public string Regione { get; set; }

        public Export_Excel(string ID, string Name, string IP, string Region)
        {
            ID = ID;
            Name = Name;
            IP = IP;
            Region = Region;
        }
    }
}
