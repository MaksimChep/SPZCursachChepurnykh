using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPZKursach
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool firstclick = true;

        SQLBase sql = new SQLBase();
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && firstclick)
            {
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton3.Visible = true;
                label4.Visible = true;
                sql.CreateTable(textBox1.Text);
                firstclick = false;
                radioButton1.Checked = true;
            }
            if (radioButton1.Checked)
            {
                sql.Add(textBox1.Text, textBox2.Text, textBox3.Text);
            }
            if(radioButton3.Checked)
            {
                sql.Update(comboBox1.Text, textBox1.Text, textBox2.Text);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = false;
            textBox3.Visible = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = true;
            textBox3.Visible = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;

            List<string> lst = sql.ResourceList();
            comboBox1.DataSource = lst;
            
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = true;
            textBox3.Visible = false;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox1.Text = "";
            textBox2.Text = "";

        }

        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            sql.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = sql.GetLogin(comboBox1.Text);
            textBox2.Text = sql.GetPassword(comboBox1.Text);
        }
    }
}
