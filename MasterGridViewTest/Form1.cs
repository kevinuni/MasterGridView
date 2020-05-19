using KControls;
using MasterGridViewTest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KControlsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {   
            DataGridViewColumnCollection columns = dgvStudents.Columns;
            columns.Add(DataGridColumnFactory.TextColumnStyle("FirstName", "FirstName"));
            columns.Add(DataGridColumnFactory.TextColumnStyle("LastName", "LastName"));

            BindingSource bs = new BindingSource();
            bs.DataSource = Student.getStudents();

            dgvStudents.DataSource = bs;
            dgvStudents.Dock = DockStyle.Fill;

            //Agregar la grilla
            this.Controls.Add(dgvStudents);
            dgvStudents.SetChild();



        }
    }
}
