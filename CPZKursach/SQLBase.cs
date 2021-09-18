using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPZKursach
{
    class SQLBase
    {
        private MySqlConnection connection = new MySqlConnection();
        private string login;
        public SQLBase()
        {
            connection.ConnectionString = "server=localhost;port=3306;username=root;database=kursach;SSL Mode=None";
            connection.Open();
        }
        public void CreateTable(string tname)
        {
            login = tname;
            MySqlCommand create = new MySqlCommand("CREATE TABLE " + tname + " (Resource TEXT, Login TEXT, Password TEXT);", connection);
            create.BeginExecuteNonQuery();
            
        }
        public void Add(string login, string password, string resource)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO " + this.login + " (Resource, Login, Password) VALUES (\"" + resource + "\",\"" + login + "\",\"" + password + "\");", connection);
            command.BeginExecuteNonQuery();
        }
        public void Close()
        {
            connection.Close();
        }
    }
}
