using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CPZKursach
{
    public struct SQLResult
    {
        public string id;
        public string resource;
        public string login;
        public string password;
    }
    public struct SQLLogs
    {
        public string resource;
        public string login;
        public string password;
        public string newlogin;
        public string newpassword;
    }
    public class SQLBase
    {
        private MySqlConnection connection = new MySqlConnection();
        public string login;
        public SQLBase()
        {
            connection.ConnectionString = "server=localhost;port=3306;username=root;database=kursach;SSL Mode=None";
            connection.Open();
        }

        public void CreateTable(string tname)
        {
            login = tname;
            MySqlCommand create = new MySqlCommand("CREATE TABLE `kursach`.`" + login + "` ( `id` INT NOT NULL AUTO_INCREMENT,  `Resource` TEXT NOT NULL ,  `Login` TEXT NOT NULL ,  `Password` TEXT NOT NULL ,    PRIMARY KEY  (`id`))", connection);
            MySqlCommand createlog = new MySqlCommand("CREATE TABLE `kursach`.`" + login + "_log" + "` ( `id` INT NOT NULL AUTO_INCREMENT,  `Resource` TEXT NOT NULL ,  `Login` TEXT NOT NULL ,  `Password` TEXT NOT NULL , `NewLogin` TEXT NOT NULL ,  `NewPassword` TEXT NOT NULL ,   PRIMARY KEY  (`id`))", connection);
            
            try
            {
                create.ExecuteNonQuery();
                createlog.ExecuteNonQuery();
            }
            catch {}       
        }

        public void Add(string login, string password, string resource)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(LoginForm.rsa.key);
            MySqlCommand command = new MySqlCommand("INSERT INTO " + this.login + " (Resource, Login, Password) VALUES (\"" + resource + "\",\"" + Convert.ToBase64String(rsa.Encrypt(Encoding.Unicode.GetBytes(login), false)) + "\",\"" + Convert.ToBase64String(rsa.Encrypt(Encoding.Unicode.GetBytes(password), false)) + "\");", connection);
            command.ExecuteNonQuery();
        }

        public List<SQLResult> AccountList()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM " + login, connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<SQLResult> resultlst = new List<SQLResult>();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            string key = LoginForm.rsa.key;
            rsa.FromXmlString(LoginForm.rsa.key);
            while (reader.Read())
            {
                SQLResult result = new SQLResult();
                result.id = reader.GetString("Id");
                result.resource = reader.GetString("Resource");
                result.login = Encoding.Unicode.GetString(rsa.Decrypt(Convert.FromBase64String(reader.GetString("Login")), false));
                result.password = Encoding.Unicode.GetString(rsa.Decrypt(Convert.FromBase64String(reader.GetString("Password")), false));
                resultlst.Add(result);
            }  
            reader.Close();
            return resultlst;
        }

        public string GetLogin(string id)
        {
            MySqlCommand command = new MySqlCommand("SELECT Login FROM " + this.login + " WHERE Id = \"" + id + "\"", connection);
            string login = command.ExecuteScalar().ToString();
            return login;
        }

        public string GetPassword(string id)
        {
            MySqlCommand command = new MySqlCommand("SELECT Password FROM " + this.login + " WHERE Id = \"" + id + "\"", connection);
            string pass = command.ExecuteScalar().ToString();
            return pass;
        }

        public string GetResource(string id)
        {
            MySqlCommand command = new MySqlCommand("SELECT Resource FROM " + this.login + " WHERE Id = \"" + id + "\"", connection);
            string resource = command.ExecuteScalar().ToString();
            return resource;
        }

        public void UpdateLogin(string id, string login)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(LoginForm.rsa.key);
            MySqlCommand command = new MySqlCommand("UPDATE " + this.login + " SET Login = \"" + Convert.ToBase64String(rsa.Encrypt(Encoding.Unicode.GetBytes(login), false)) + "\" WHERE Id = " + id, connection);
            command.ExecuteNonQuery();
        }

        public void UpdatePassword(string id, string password)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(LoginForm.rsa.key);
            MySqlCommand command = new MySqlCommand("UPDATE " + this.login + " SET Password = \"" + Convert.ToBase64String(rsa.Encrypt(Encoding.Unicode.GetBytes(password), false)) + "\" WHERE Id = " + id, connection);
            command.ExecuteNonQuery();
        }

        public void Delete(string id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM " + login + " WHERE id = " + id,connection);
            command.ExecuteNonQuery();
        }

        public void LogAll(string id,string newlogin,string newpassword)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(LoginForm.rsa.key);
            MySqlCommand command = new MySqlCommand("INSERT INTO " + this.login + "_log" + " (Resource, Login, Password, NewLogin, NewPassword) VALUES (\"" + GetResource(id) + "\",\"" + GetLogin(id) + "\",\"" + GetPassword(id) + "\",\"" + Convert.ToBase64String(rsa.Encrypt(Encoding.Unicode.GetBytes(newlogin), false)) + "\",\"" + Convert.ToBase64String(rsa.Encrypt(Encoding.Unicode.GetBytes(newpassword), false)) + "\");", connection);
            command.ExecuteNonQuery();
        }
        public void LogOnlyLogin(string id, string newlogin)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(LoginForm.rsa.key);
            MySqlCommand command = new MySqlCommand("INSERT INTO " + this.login + "_log" + " (Resource, Login, Password, NewLogin, NewPassword) VALUES (\"" + GetResource(id) + "\",\"" + GetLogin(id) + "\",\"" + GetPassword(id) + "\",\"" + Convert.ToBase64String(rsa.Encrypt(Encoding.Unicode.GetBytes(newlogin), false)) + "\",\"" + GetPassword(id) + "\");", connection);
            command.ExecuteNonQuery();
        }

        public void LogOnlyPassword(string id, string newpassword)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(LoginForm.rsa.key);
            MySqlCommand command = new MySqlCommand("INSERT INTO " + this.login + "_log" + " (Resource, Login, Password, NewLogin, NewPassword) VALUES (\"" + GetResource(id) + "\",\"" + GetLogin(id) + "\",\"" + GetPassword(id) + "\",\"" + GetLogin(id) + "\",\"" + Convert.ToBase64String(rsa.Encrypt(Encoding.Unicode.GetBytes(newpassword), false)) + "\");", connection);
            command.ExecuteNonQuery();
        }

        public List<SQLLogs> LogList()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM " + login + "_log", connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<SQLLogs> resultlst = new List<SQLLogs>();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(LoginForm.rsa.key);
            while (reader.Read())
            {
                SQLLogs result = new SQLLogs();
                result.resource = reader.GetString("Resource");
                result.login = Encoding.Unicode.GetString(rsa.Decrypt(Convert.FromBase64String(reader.GetString("Login")), false));
                result.password = Encoding.Unicode.GetString(rsa.Decrypt(Convert.FromBase64String(reader.GetString("Password")), false));
                result.newlogin = Encoding.Unicode.GetString(rsa.Decrypt(Convert.FromBase64String(reader.GetString("NewLogin")), false));
                result.newpassword = Encoding.Unicode.GetString(rsa.Decrypt(Convert.FromBase64String(reader.GetString("NewPassword")), false));
                resultlst.Add(result);
            }
            reader.Close();
            return resultlst;
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
