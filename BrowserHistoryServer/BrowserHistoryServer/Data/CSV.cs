using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

public static class CSV
{
    private static char separator = ';';
    /// <summary>
    /// Загрузка данных из CSV файла
    /// </summary>
    /// <param name="fileName">Имя файла</param>
    /// <param name="first_row_is_column_name">Указывает что первая строка файла содержит имена столбцов</param>
    /// <returns></returns>
    public static System.Data.DataTable ImportFromCSV(string fileName, bool first_row_is_column_name)
    {
        System.Data.DataTable ret = new System.Data.DataTable();
        TextReader tr = new StreamReader(fileName, Encoding.GetEncoding(1251));
        String line = "";
        line = tr.ReadLine();
        string[] columns = line.Split(separator);
        for (int i = 0; i < columns.Length; i++)
            if (first_row_is_column_name)
                ret.Columns.Add(columns[i]);
            else
                ret.Columns.Add("column_" + i.ToString());
        if (first_row_is_column_name)
            line = tr.ReadLine();
        while (line != null)
        {
            string[] blocks = line.Split(separator);
            DataRow dr = ret.NewRow();
            for (int i = 0; i < (blocks.Length & ret.Columns.Count); i++)
            {
                dr["column_" + i.ToString()] = blocks[i];
            }
            ret.Rows.Add(dr);
            line = tr.ReadLine();
        }
        return ret;
    }
    /// <summary>
    /// Выгрузка в CSV файл
    /// </summary>
    /// <param name="fileName">Имя файла</param>
    /// <param name="table">Таблица для выгрузки</param>
    /// <returns></returns>
    public static bool ExportToCSV(string fileName, System.Data.DataTable table)
    {
        if (table != null)
        {
            FileStream fs = null;
            try
            {
                fs = File.OpenWrite(fileName);
            }
            catch
            {
                return false;
            }
            using (TextWriter tw = new StreamWriter(fs, Encoding.GetEncoding(1251)))
            {
                String line = "";
                //Выводим имя таблицы
                if (!String.IsNullOrEmpty(table.TableName))
                    tw.WriteLine(table.TableName);
                //Вывод названий столбцов
                foreach (DataColumn colName in table.Columns)
                {
                    line += String.Format("\"{0}\"{1}", colName.ColumnName, separator);
                }
                tw.WriteLine(line.TrimEnd(separator));
                //Вывод данных
                foreach (DataRow dr in table.Rows)
                {
                    line = "";
                    Array.ForEach(dr.ItemArray, obj => line += String.Format("\"{0}\"{1}", obj, separator));
                    tw.WriteLine(line.TrimEnd(separator));
                }
            }
            fs.Close();
            fs.Dispose();
            return true;
        }
        return false;
    }
    
}