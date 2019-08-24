using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace BrowserHistoryServer.Data
{
    public class PasswordSaver : IDisposable
    {
        List<SimpleUser> data;
        XmlSerializer formatter;
        public PasswordSaver()
        {
            data = new List<SimpleUser>();
            formatter = new XmlSerializer(typeof(List<SimpleUser>));
            if(File.Exists(Directory.GetCurrentDirectory() + "\\people.xml"))
                data = getFromXML();
            
        }
        public void addItem(string pass, string log)
        {
            if (data == null)
                data = new List<SimpleUser>();
            data.Add(new SimpleUser(pass, log));
        }
        public string getPass(string log)
        {
            if (data != null)
            {
                foreach (var item in data)
                {
                    if (item.log.Equals(log)) return item.pass;
                }
            }
            return String.Empty;
        }

        private List<SimpleUser> getFromXML()
        {
            try
            {
                using (FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\people.xml", FileMode.Open))
                {
                    data = (List<SimpleUser>)formatter.Deserialize(fs);
                }
                return data;
            }
            catch (Exception exs)
            {

                
            }
            return null;
        }

        public void setToXML()
        {

            using (FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\people.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, data);
            }
        }

        public void Dispose()
        {
            setToXML();
        }
    }

}
