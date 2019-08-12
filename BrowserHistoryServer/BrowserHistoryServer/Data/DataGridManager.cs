using BrowserHistory_Server.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BrowserHistoryServer.Data
{
    class DataGridManager
    {
        static DataGridManager dataGrid;
        public List<string> GridColumnsName { get; private set; }
        DbManager db;
        private DataGridManager()
        {
            GridColumnsName = new List<string>();
            GridColumnsName.Add("ID");
            GridColumnsName.Add("Имя");
            GridColumnsName.Add("IP");
            GridColumnsName.Add("Регион");
        }

        public static DataGridManager Init()
        {
            if (dataGrid == null)
            {
                dataGrid = new DataGridManager();
                dataGrid.db = new DbManager(System.IO.Directory.GetCurrentDirectory() + "\\DB.db");
            }
            return dataGrid;
        }

        public ICollectionView GetRegions()
        {
            return (new CollectionViewSource() { Source = db.getRegionsAsync().GetAwaiter().GetResult().Values }).View; ;
        }
        public ICollectionView GetTable(Predicate<object> filter = null)
        {
            var data = (new CollectionViewSource() { Source = db.getUsers().GetAwaiter().GetResult() }).View;
            if (filter != null)
                data.Filter += filter;
            return data; 
        }
    }
}
