using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserHistoryServer.Data
{
    [Serializable]
    public class SimpleUser
    {
        public string pass;
        public string log;
        public SimpleUser()
        {

        }
        public SimpleUser(string log, string pass)
        {
            this.log = log;
            this.pass = pass;
        }
    }
}
