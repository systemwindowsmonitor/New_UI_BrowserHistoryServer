using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;

namespace BrowserHistory_Server.Data
{
    class DbManager : System.IDisposable
    {
        string dataBaseName;
        SQLiteConnection connection;
        public DbManager(string dataBaseName)
        {
            this.dataBaseName = dataBaseName;
        }

        #region  подключение/отключение БД
        public void Connect()
        {
            try
            {
                connection = new SQLiteConnection(string.Format($"Data Source={dataBaseName};"));
                connection.Open();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public void Disconnect()
        {
            try
            {
                if (this.connection != null)
                    if (this.connection.State == System.Data.ConnectionState.Open)
                    {
                        this.connection.ClearCachedSettings();
                        this.connection.ClearTypeCallbacks();
                        this.connection.ClearTypeMappings();
                        this.connection.Close();
                        this.connection.Dispose();
                        this.connection = null;
                    }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region взаимодействие с логином/паролем
        public bool CheckLogin(string login)
        {
            return GetLogins(login) > 0 ? true : false;
        }
        public bool CheckPassword(string crypt_password)
        {
            return GetPasswords(crypt_password) > 0 ? true : false; ;
        }
        #endregion

        #region методы выборки с таблиц
        public List<User> getUsers()
        {
            List<User> data = new List<User>();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Сlient;", connection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                foreach (DbDataRecord record in reader)
                {
                    data.Add(new User(
                        record["id"].ToString(),
                        record["account_name"].ToString(),
                        record["ip"].ToString(),
                        record["Region"].ToString()
                    ));
                }
            }

            return data;
        }

        public List<string> GetLogins()
        {
            List<string> data = new List<string>();
            SQLiteCommand command = new SQLiteCommand("SELECT login FROM AuthorizationData;", connection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                foreach (DbDataRecord record in reader)
                {
                    data.Add(record["login"].ToString());
                }
            }
            return data;
        }
        private int GetLogins(string key)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand("SELECT id FROM AuthorizationData where login = @login;", connection);
                command.Parameters.Add(new SQLiteParameter("@login", key));
                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    return reader.FieldCount;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            return 0;
        }
        public List<string> GetPasswords()
        {
            List<string> data = new List<string>();
            SQLiteCommand command = new SQLiteCommand("SELECT password FROM AuthorizationData;", connection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                foreach (DbDataRecord record in reader)
                {
                    data.Add(record["password"].ToString());
                }
            }
            return data;
        }
        private int GetPasswords(string key)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand("SELECT id FROM AuthorizationData where password = @password;", connection);
                command.Parameters.Add(new SQLiteParameter("@password", key));
                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    return reader.FieldCount;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            return 0;
        }
        #endregion

        #region добавление пользователей
        public bool AddUser(string acc_name, string ip, string region)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand("INSERT INTO Сlient (account_name,ip,Region)" +
                    "VALUES(@account_name,@ip,@region);", connection);
                command.Parameters.Add(new SQLiteParameter("@account_name", acc_name));
                command.Parameters.Add(new SQLiteParameter("@ip", ip));
                command.Parameters.Add(new SQLiteParameter("@region", region));
                command.CreateParameter();
                if (command.ExecuteNonQuery() > 0) return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public bool AddAdmin(string name, string surname, string middle, string login, string pass, string role)
        {
            try
            {
                int id = AddAutorizationData(login, pass, role);
                if (id > 0)
                {
                    SQLiteCommand command = new SQLiteCommand("INSERT INTO Admin (IdAutorizationData,NAME,SURNAME,MIDDLENAME)" +
                        " VALUES(@idAutorizationData, @name, @surname, @middle)", connection);
                    command.Parameters.Add(new SQLiteParameter("@idAutorizationData", id));
                    command.Parameters.Add(new SQLiteParameter("@name", name));
                    command.Parameters.Add(new SQLiteParameter("@surname", surname));
                    command.Parameters.Add(new SQLiteParameter("@middle", middle));
                    command.CreateParameter();

                    if (command.ExecuteNonQuery() > 0) return true;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return false;
        }

        /// <summary>
        /// ДОБАВЛЯЕТ НОВЫЕ ДАННЫЕ АВТОРИЗАЦИИ И ВОЗРАЩАЕТ ID ЗАПИСИ
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public int AddAutorizationData(string login, string pass, string role)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand("INSERT INTO AuthorizationData (login,password,role)VALUES(@login,@password,@role);", connection);
                command.Parameters.Add(new SQLiteParameter("@login", login));
                command.Parameters.Add(new SQLiteParameter("@password", pass));
                command.Parameters.Add(new SQLiteParameter("@role", role));
                command.CreateParameter();
                if (command.ExecuteNonQuery() > 0)
                {
                    command = new SQLiteCommand("SELECT MAX(Id) From AuthorizationData", connection);

                    var i = command.ExecuteReader();
                    if (i.Read())
                    {
                        return int.Parse(i.GetValue(0).ToString());
                    }

                }
            }
            catch (System.Exception ex)
            {
                return -1;
            }
            return -1;
        }
        #endregion

        public void Dispose()
        {
            try
            {
                if (connection != null)
                {
                    connection.Clone();
                    connection.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
    }
}
