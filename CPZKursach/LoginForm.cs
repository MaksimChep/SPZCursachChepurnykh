using System;
using System.Windows.Forms;

namespace CPZKursach
{   
    public partial class LoginForm : Form
    {
        public static CryptoRSA rsa = new CryptoRSA();
        public static SQLBase sql = new SQLBase();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rsa.Open(textBox1.Text, maskedTextBox1.Text))
            {
                sql.CreateTable(textBox1.Text);
                MainForm dlg = new MainForm(textBox1.Text);
                dlg.ShowDialog();
                this.Close();
            }
            else
            {
                maskedTextBox1.Text = "";
                MessageBox.Show("Неверный пароль!");
            }
        }

        private void LoginFormClosed(object sender, FormClosedEventArgs e)
        {
            sql.Close();
        }
    }
}
