using MasterGridViewTest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MasterGridView
{
    static class ConfigChildGrid
    {
        public static void configColumns(DataGridView grid, Type tipo)
        {
            DataGridViewColumnCollection columns = grid.Columns;



            if (tipo == typeof(Notes))
            {
                //grid.AutoGenerateColumns = true;
                columns.Add(DataGridColumnFactory.IntegerColumnStyle("Id", "Id"));
                columns.Add(DataGridColumnFactory.IntegerColumnStyle("Nota1", "Nota1"));
                columns.Add(DataGridColumnFactory.IntegerColumnStyle("Nota2", "Nota2"));

            }
            

        }

    }


}
