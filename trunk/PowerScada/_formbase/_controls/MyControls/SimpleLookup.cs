using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PowerScada
{
    public partial class SimpleLookup : Form
    {
        public static string CommandName = "";
        public static DataTable dt = new DataTable();
        private SimpleLookup()
        {
            InitializeComponent();
            InitDataConstrols();
           
        }

        private SimpleLookup(string commandname)
        {
            InitializeComponent();
            CommandName = commandname;
            InitDataConstrols();
        }

        private void InitDataConstrols()
        {
            if (!string.IsNullOrEmpty(CommandName))
            {
                if (CommandName == "EditButtonHasta2")
                {
                    Ustpanel.Visible = true;
                    superTextBoxAdi.textBox.TextChanged += new EventHandler(textBox_TextChanged);
                    superTextBoxSoyadi.textBox.TextChanged += new EventHandler(textBox_TextChanged);
                    superTextBoxTckimlikno.textBox.TextChanged += new EventHandler(textBox_TextChanged);
                }
                else
                    Ustpanel.Visible = false;
            }
            else
            {
                Ustpanel.Visible = false;
            }
        }

        void textBox_TextChanged(object sender, EventArgs e)
        {
            string filter = string.Empty;
            filter += "1=1";
            if (superTextBoxAdi.textBox.Text.Length > 0)
            {
                filter +=" AND Adi like '"+superTextBoxAdi.textBox.Text+"%'";
            }
            if (superTextBoxSoyadi.textBox.Text.Length > 0)
            {
                filter += " AND soyadi like '" + superTextBoxSoyadi.textBox.Text + "%'";
              
            }
            if (superTextBoxTckimlikno.textBox.Text.Length > 0)
            {
                filter += " AND TckNo  like '" + superTextBoxTckimlikno.textBox.Text + "%'";
            }
            if (filter == "1=1")
            {
                grid.DataSource = dt;
               
            }
            else
            {
              
                DataView view = dt.DefaultView;
                view.RowFilter = filter;
                grid.DataSource = view;
            }
        }

      

        public static int Select(DataTable table)
        {
            int result = 0;

            SimpleLookup lookup = new SimpleLookup();
            lookup.StartPosition = FormStartPosition.CenterScreen;
            lookup.grid.DataSource = table;
            lookup.ShowDialog();

            if (lookup.DialogResult == DialogResult.OK)
            {
                result = 0;
                if (lookup.grid.CurrentRow != null && Convert.ToInt32(lookup.grid.CurrentRow.Cells["Id"].Value) > 0)
                {
                    result = Convert.ToInt32(lookup.grid.CurrentRow.Cells["Id"].Value);
                }
            }
            lookup.Dispose();

            return result;
        }
        /// <summary>
        /// Editbuttonlar için sadece id ve isim lazım boşuna entityi okumaya gerek yok "deger" parametresi bize adi gibi istenen değeri döndersin
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static long Select(DataTable table, string fieldname, ref string deger)
        {
            long result = 0;

            SimpleLookup lookup = new SimpleLookup();
            lookup.StartPosition = FormStartPosition.CenterScreen;
            dt = table;
            lookup.grid.DataSource = dt;
            if (lookup.grid.Columns.Contains("Id"))
                lookup.grid.Columns["Id"].Visible = false;

            if (CommandName == "EditButtonHasta2")
            {
                if (lookup.grid.Columns.Contains("AdiSoyadi"))
                    lookup.grid.Columns["AdiSoyadi"].Visible = false;
            }
            lookup.ShowDialog();
            
           
            
            if (lookup.DialogResult == DialogResult.OK)
            {
                result = 0;
                if (lookup.grid.CurrentRow != null && Convert.ToInt64(lookup.grid.CurrentRow.Cells["Id"].Value) > 0)
                {
                    result = Convert.ToInt64(lookup.grid.CurrentRow.Cells["Id"].Value);
                    deger = lookup.grid.CurrentRow.Cells[fieldname].Value.ToString();
                }
            }
            CommandName = string.Empty;
            lookup.Dispose();

            return result;
        }

        private void SimpleLookup_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)27:
                    e.Handled = true;
                    this.DialogResult = DialogResult.Cancel;
                    break;
                case (char)13:
                    e.Handled = true;
                    this.DialogResult = DialogResult.OK;
                    break;
            }
        }

        private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}