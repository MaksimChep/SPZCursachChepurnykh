using MySql.Data.MySqlClient;
using System;
using System.Text;
using System.Security.Cryptography;

namespace CPZKursach
{
    public class CryptoRSA
    {
        public string key;
        public bool Open(string login, string password)
        {
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = "server=localhost;port=3306;username=root;database=kursach;SSL Mode=None";
            connection.Open();
            
            MySqlCommand command = new MySqlCommand("SELECT login FROM basetable WHERE login = \"" + login + "\";", connection);

            if (command.ExecuteScalar() == null)
            {

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                key = rsa.ToXmlString(true);
                MySqlCommand insert = new MySqlCommand("INSERT INTO basetable (login,pass,xmlkey) VALUES (\"" + login + "\"" + ", \"" + Convert.ToBase64String(rsa.Encrypt(Encoding.Unicode.GetBytes(password), false)) + "\", \"" + key + "\");", connection);
                insert.ExecuteNonQuery();
                connection.Close();
                rsa.Dispose();
                return true;
            }
            else
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                MySqlCommand selectxml = new MySqlCommand("SELECT xmlkey FROM basetable WHERE login = \"" + login + "\";", connection);
                rsa.FromXmlString(selectxml.ExecuteScalar().ToString());
                key = selectxml.ExecuteScalar().ToString();
                MySqlCommand selectpass = new MySqlCommand("SELECT pass FROM basetable WHERE login = \"" + login + "\";", connection);
                string pass = selectpass.ExecuteScalar().ToString();
                string decodedpass = Encoding.Unicode.GetString(rsa.Decrypt(Convert.FromBase64String(pass),false));
                if (decodedpass == password)
                {
                    connection.Close();
                    rsa.Dispose();
                    return true;
                }
                else
                {
                    connection.Close();
                    rsa.Dispose();
                    return false;
                }
            }        
        }
    }
}
