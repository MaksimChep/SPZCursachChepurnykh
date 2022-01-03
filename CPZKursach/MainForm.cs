using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CPZKursach
{
    public partial class MainForm : Form
    {
        public MainForm(string login)
        {
            InitializeComponent();
            UpdateTable();
            if(login == "admin")
            {
                button3.Visible = true;
            }
        }
        void UpdateTable()
        {
            dataGridView1.Rows.Clear();
            List<SQLResult> lst = LoginForm.sql.AccountList();
            foreach (SQLResult r in lst)
            {
                dataGridView1.Rows.Add(r.id,r.resource, r.login, r.password);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddForm dlg = new AddForm();
            dlg.ShowDialog();
            UpdateTable();
        }

        private void dataGridDblClick(object sender, EventArgs e)
        {
            string id = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            ChooseForm dlg = new ChooseForm(id);
            dlg.ShowDialog();
            UpdateTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LogForm dlg = new LogForm();
            dlg.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminForm dlg = new AdminForm();
            dlg.ShowDialog();
        }
    }
}
