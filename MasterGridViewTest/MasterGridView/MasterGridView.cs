﻿using MasterGridViewTest;
using MasterGridViewTest.MasterGridView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace MasterGridView
{
    public class MasterGridView<T> : DataGridView
    {
        internal List<int> lstCurrentRows = new List<int>();
        internal int rowDefaultHeight = 22;
        internal int rowExpandedHeight = 300;
        internal int rowDefaultDivider = 0;
        internal int rowExpandedDivider = 300 - 22;
        internal int rowDividerMargin = 5;
        internal bool doCollapseRow;


        ImageList rowHeaderIconList = new ImageList();

        internal DetailTabControl detailTabControl = new DetailTabControl
        {
            Height = 278 /*rowExpandedDivider*/ - 5/*rowDividerMargin*/ * 2,
            Visible = false
        };

        private System.ComponentModel.IContainer components;

        public enum rowHeaderIcons
        {
            expand = 1,
            collapse = 0
        }



        public MasterGridView()
        {
            InitializeComponent();

            
            rowHeaderIconList.Images.Add(Datos.expanded);
            rowHeaderIconList.Images.Add(Datos.collapsed);
            rowHeaderIconList.TransparentColor = System.Drawing.Color.Transparent;
            rowHeaderIconList.Images.SetKeyName(1, "expanded.gif");
            rowHeaderIconList.Images.SetKeyName(0, "collapsed.gif");


            Scroll += MasterControl_Scroll;

            // Dibuja el símbolo "+"
            RowPostPaint += MasterControl_RowPostPaint;

            // Al dar click en el símbolo "+" del rowheader
            RowHeaderMouseClick += MasterControl_RowHeaderMouseClick;

            // Mostrar en un tooltip la descripción de la Propiedad cuando se pase el mouse por encima
            CellMouseEnter += MasterGridView_CellMouseEnter;

            // Limpiar la celda cuando se presiona ESC
            CellEndEdit += MasterGridView_CellEndEdit;


            // Eliminar registros o abrir detalles
            KeyDown += MasterGridView_KeyDown;

            cModule.applyGridTheme(this);
        }


        


        void MasterGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            MasterGridView<T> grid = (MasterGridView<T>)sender;

            // Clear the row error in case the user presses ESC.   
            grid.Rows[e.RowIndex].ErrorText = String.Empty;
        }


        void MasterGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // refrescar las celdas a las que no se les haya hecho commit
            if (this.IsCurrentCellDirty)
            {
                this.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }


        void MasterGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode.Equals(Keys.Delete))
            {
                BindingSource bs = (BindingSource)this.DataSource;

                if (bs.Current != null)
                {
                    //TODO pendiente validar que no se esté usando en otros lados
                    if (MessageBox.Show("Está seguro de eliminar " + bs.Current.ToString(), "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        bs.RemoveCurrent();
                    }
                }
            }
            else if (e.Control && e.KeyCode.Equals(Keys.Enter))
            {
                // abrir detalle
                OpenDetail(this.CurrentRow.Index);
            }


        }




        void comboOpt2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Delete))
            {
                ComboBox combo = (ComboBox)this.EditingControl;
                combo.SelectedIndex = -1;
            }
        }

        void comboOpt1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Delete))
            {
                ComboBox combo = (ComboBox)this.EditingControl;
                combo.SelectedIndex = -1;

            }
        }


        ToolTip tt = new ToolTip();
        void MasterGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Mostrar en un tooltip la descripción de la Propiedad cuando se pase el mouse por encima
            if (e.RowIndex == -1 && e.ColumnIndex != -1 && this.DataSource != null)
            {
                var tipo = typeof(T);
                var property = tipo.GetProperty(this.Columns[e.ColumnIndex].DataPropertyName);
                var description = TypeMethods.GetDescriptionFromPropertyInfo(property);

                if (string.IsNullOrWhiteSpace(description))
                {
                    tt.Hide(this);
                }
                else
                {
                    tt.SetToolTip(this, description);
                }
            }
            //else
            //{
            //    tt.Hide(this);
            //}
        }



        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterGridView<T>));


            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Indica si el objeto contiene listas genéricas
        /// Nota: Tener en cuenta que no todas las listas genéricas se convierten en grillas hijas
        /// </summary>
        /// <returns></returns>
        private bool HasDetailList()
        {

            bool hasDetailList = false;
            foreach (FieldInfo field in typeof(T).GetFields())
            {
                if (field.FieldType.IsGenericType 
                    && field.FieldType.GetGenericTypeDefinition() == typeof(List<>)
                    )
                {
                    hasDetailList |= true;
                }
            }

            return hasDetailList;
        }
        /// <summary>
        /// Es necesario fijar la grilla hija al mismo nivel de jerarquia que el Mastergrid
        /// </summary>
        public void SetChild()
        {
            if (this.Parent == null)
            {
                throw new Exception("El control debe estar en un contenedor.");
            }
            this.Parent.Controls.Add(detailTabControl);
            detailTabControl.BringToFront();
        }


        private void MasterControl_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rect = new Rectangle(e.RowBounds.X + ((rowDefaultHeight - 16) / 2), e.RowBounds.Y + ((rowDefaultHeight - 16) / 2), 16, 16);
            //Point point = new Point(rect.Left, rect.Height);

            if (HasDetailList())
            {
                if (doCollapseRow)
                {
                    if (lstCurrentRows.Contains(e.RowIndex))
                    {
                        this.Rows[e.RowIndex].DividerHeight = this.Rows[e.RowIndex].Height - rowDefaultHeight;

                        e.Graphics.DrawImage(rowHeaderIconList.Images[(int)rowHeaderIcons.collapse], rect);

                        detailTabControl.Location = new Point(e.RowBounds.Left + this.RowHeadersWidth, e.RowBounds.Top + rowDefaultHeight + 5);
                        detailTabControl.Width = e.RowBounds.Right - this.RowHeadersWidth;
                        detailTabControl.Height = this.Rows[e.RowIndex].DividerHeight - 10;
                        detailTabControl.Visible = true;
                    }
                    else
                    {
                        detailTabControl.Visible = false;
                        e.Graphics.DrawImage(rowHeaderIconList.Images[(int)rowHeaderIcons.expand], rect);
                    }
                    doCollapseRow = false;
                }
                else
                {
                    if (lstCurrentRows.Contains(e.RowIndex))
                    {
                        this.Rows[e.RowIndex].DividerHeight = this.Rows[e.RowIndex].Height - rowDefaultHeight;
                        e.Graphics.DrawImage(rowHeaderIconList.Images[(int)rowHeaderIcons.collapse], rect);
                        detailTabControl.Location = new Point(e.RowBounds.Left + this.RowHeadersWidth, e.RowBounds.Top + rowDefaultHeight + 5);
                        detailTabControl.Width = e.RowBounds.Right - this.RowHeadersWidth;
                        detailTabControl.Height = this.Rows[e.RowIndex].DividerHeight - 10;
                        detailTabControl.Visible = true;
                    }
                    else
                    {
                        e.Graphics.DrawImage(rowHeaderIconList.Images[(int)rowHeaderIcons.expand], rect);
                    }
                }
            }

            cModule.rowPostPaint_HeaderCount(sender, e);
        }

        private void MasterControl_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.Rows[e.RowIndex].DataBoundItem == null)
            {
                return;
            }
            Rectangle rect = new Rectangle((rowDefaultHeight - 16) / 2, (rowDefaultHeight - 16) / 2, 16, 16);
            if (rect.Contains(e.Location))
            {
                // si se presiona en el símbolo más se abre el detalle
                OpenDetail(e.RowIndex);
            }
            else
            {
                doCollapseRow = false;
            }
        }

        /// <summary>
        /// Abre/Cierra la subGrilla de detalle
        /// </summary>
        /// <param name="rowIndex">Índice del registro a editar</param>
        private void OpenDetail(int rowIndex)
        {
            if (lstCurrentRows.Contains(rowIndex))
            {
                lstCurrentRows.Clear();
                this.Rows[rowIndex].Height = rowDefaultHeight;
                this.Rows[rowIndex].DividerHeight = rowDefaultDivider;
            }
            else
            {
                if (lstCurrentRows.Count != 0)
                {
                    int eRow = lstCurrentRows[0];
                    lstCurrentRows.Clear();
                    this.Rows[eRow].Height = rowDefaultHeight;
                    this.Rows[eRow].DividerHeight = rowDefaultDivider;
                    this.ClearSelection();
                    doCollapseRow = true;
                    this.Rows[eRow].Selected = true;
                }

                lstCurrentRows.Add(rowIndex);

                Type parentType = TypeMethods.HeuristicallyDetermineType((IList)this.DataSource);
                object parentObject = this.Rows[rowIndex].DataBoundItem;

                // Detalle
                detailTabControl.TabPages.Clear();

                detailTabControl.openDetailEvent += detailTabControl_OpenDetail;

                if (parentObject != null)
                {
                    foreach (FieldInfo childField in parentType.GetFields())
                    {
                        if (childField.FieldType.IsGenericType
                            && childField.FieldType.GetGenericTypeDefinition() == typeof(List<>)
                            && childField.FieldType.GetGenericTypeDefinition() != typeof(List<double>))
                        {
                            IList listOfDetail = (IList)childField.GetValue(parentObject);

                            string name = TypeMethods.GetDescriptionFromFieldInfo(childField);

                            
                            detailTabControl.AddChildgrid(listOfDetail, name);
                        }
                    }
                }

                // expandir la fila
                if (detailTabControl.HasChildren)
                {
                    this.Rows[rowIndex].Height = rowExpandedHeight;
                    this.Rows[rowIndex].DividerHeight = rowExpandedDivider;
                }
                else
                {
                    detailTabControl.Visible = false;
                }
            }
            this.ClearSelection();
            doCollapseRow = true;
            this.Rows[rowIndex].Selected = true;
        }

        void detailTabControl_OpenDetail()
        {
            // Se invoca al método que abre/cierra la grilla de detalle
            OpenDetail(this.CurrentRow.Index);
        }





        private void MasterControl_Scroll(object sender, ScrollEventArgs e)
        {
            if (!(lstCurrentRows.Count == 0))
            {
                doCollapseRow = true;
                this.ClearSelection();
                this.Rows[lstCurrentRows[0]].Selected = true;
            }
        }
    }
}
