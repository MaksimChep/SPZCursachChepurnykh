using System;
using System.Windows.Forms;

namespace CPZKursach
{
    public partial class ChooseForm : Form
    {
        string id;
        public ChooseForm(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm.sql.Delete(id);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateForm dlg = new UpdateForm(id);
            dlg.ShowDialog();
            this.Close();
        }
    }
}
