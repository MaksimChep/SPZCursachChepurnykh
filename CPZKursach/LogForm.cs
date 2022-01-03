using System.Collections.Generic;
using System.Windows.Forms;

namespace CPZKursach
{
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
            List<SQLLogs> lst = LoginForm.sql.LogList();
            foreach(SQLLogs r in lst)
            {
                dataGridView1.Rows.Add(r.resource, r.login, r.password, r.newlogin, r.newpassword);
            }
        }
    }
}
