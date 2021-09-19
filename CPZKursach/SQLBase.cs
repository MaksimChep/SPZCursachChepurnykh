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

        public List<string> ResourceList()
        {
            MySqlCommand command = new MySqlCommand("SELECT Resource FROM " + this.login, connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<string> str = new List<string>();
            while(reader.Read())
            {
                str.Add(reader.GetString("Resource"));
            }
            reader.Close();
            return str;
        }

        public string GetLogin(string resource)
        {
            MySqlCommand command = new MySqlCommand("SELECT Login FROM " + this.login + " WHERE Resource = \"" + resource + "\"", connection);
            string login = command.ExecuteScalar().ToString();
            return login;
        }

        public string GetPassword(string resource)
        {
            MySqlCommand command = new MySqlCommand("SELECT Password FROM " + this.login + " WHERE Resource = \"" + resource + "\"", connection);
            string pass = command.ExecuteScalar().ToString();
            return pass;
        }

        public void Update(string resource, string login, string password)
        {
            MySqlCommand command = new MySqlCommand("UPDATE " + this.login + " SET Login = " + "\"" + login + "\"" + ", Password = " + "\"" + password + "\"" + " WHERE Resource = " + "\"" + resource + "\"", connection);
            command.ExecuteNonQuery();
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
