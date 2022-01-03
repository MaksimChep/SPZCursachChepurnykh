using System;
using System.Windows.Forms;

namespace CPZKursach
{
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                LoginForm.sql.Add(textBox2.Text, textBox3.Text, textBox1.Text);
                this.Close();
            }
            
        }
        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
