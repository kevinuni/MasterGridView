using MasterGridView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterGridViewTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MasterGridView<Person> dgvPerson = new MasterGridView<Person>();
            DataGridViewColumnCollection columns = dgvPerson.Columns;            
            columns.Add(DataGridColumnFactory.TextColumnStyle("FirstName", "FirstName"));
            columns.Add(DataGridColumnFactory.TextColumnStyle("LastName", "LastName"));

            BindingSource bs = new BindingSource();
            bs.DataSource = Person.getPeople();

            dgvPerson.DataSource = bs;
            dgvPerson.Dock = DockStyle.Fill;

            //ConfigGridStyle.SetDefaultCellStyle(dgvPerson);

            //Agregar la grilla
            this.Controls.Add(dgvPerson);
            dgvPerson.SetChild();

            

        }
    }
}
