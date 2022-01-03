using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace CPZKursach
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            UpdateTable();
        }

        private void dataGridDblClick(object sender, EventArgs e)
        {
            string id = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            string login = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
            if(login == LoginForm.sql.login)
            {
                MessageBox.Show("Вы не можете удалить текущего пользователя!");
                return;
            }
            DialogResult result = MessageBox.Show("Вы хотите удалить выбранного пользователя?", "", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                MySqlConnection connection = new MySqlConnection();
                connection.ConnectionString = "server=localhost;port=3306;username=root;database=kursach;SSL Mode=None";
                connection.Open();               
                MySqlCommand command = new MySqlCommand("DROP TABLE " + login, connection);
                command.ExecuteNonQuery();
                command.CommandText = "DROP TABLE " + login + "_log";
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM basetable WHERE id = " + id;
                command.ExecuteNonQuery();
            }
            UpdateTable();
        }
        private void UpdateTable()
        {
            dataGridView1.Rows.Clear();
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = "server=localhost;port=3306;username=root;database=kursach;SSL Mode=None";
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM basetable", connection);
            MySqlDataReader reader = command.ExecuteReader();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            while (reader.Read())
            {
                rsa.FromXmlString(reader.GetString("Xmlkey"));
                dataGridView1.Rows.Add(reader.GetString("Id"), reader.GetString("Login"), Encoding.Unicode.GetString(Convert.FromBase64String(Convert.ToBase64String(rsa.Decrypt(Convert.FromBase64String(reader.GetString("Pass")), false)))));
            }
            reader.Close();
        }
    }
}
