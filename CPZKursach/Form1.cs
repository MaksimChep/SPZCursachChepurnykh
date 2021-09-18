using MySql.Data.MySqlClient;
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
            }
            if (radioButton1.Checked)
            {
                sql.Add(textBox1.Text, textBox2.Text, textBox3.Text);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = false;
            textBox3.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = true;
            textBox3.Visible = false;

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = true;
            textBox3.Visible = false;
        }

        private void Form1_FormClosing(object sender, EventArgs e)
        {
            MessageBox.Show("sadasd");
        }

        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            sql.Close();
        }
    }
}
