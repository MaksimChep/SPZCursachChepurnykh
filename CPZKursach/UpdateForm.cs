using System;
using System.Windows.Forms;

namespace CPZKursach
{
    public partial class UpdateForm : Form
    {
        string id;
        public UpdateForm(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = checkBox2.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(checkBox1.Checked && checkBox2.Checked)
            {
                LoginForm.sql.LogAll(id, textBox1.Text, textBox2.Text);
                LoginForm.sql.UpdateLogin(id, textBox1.Text);
                LoginForm.sql.UpdatePassword(id, textBox2.Text);
            }
            else if(checkBox1.Checked)
            {
                LoginForm.sql.LogOnlyLogin(id, textBox1.Text);
                LoginForm.sql.UpdateLogin(id, textBox1.Text);
            }
            else if (checkBox2.Checked)
            {
                LoginForm.sql.LogOnlyPassword(id, textBox2.Text);
                LoginForm.sql.UpdatePassword(id, textBox2.Text);
            }
            this.Close();
        }
    }
}
