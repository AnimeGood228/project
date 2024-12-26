using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class menu : Form
    {
        int id_;
        public menu(int id)
        {
            InitializeComponent();
            this.id_ = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            statement_1 form = new statement_1(id_);
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            statement_2 form = new statement_2(id_);
            form.ShowDialog();
              
        }

    }
}
