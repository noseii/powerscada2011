using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PowerScada
{
    public partial class SimpleTreeLookup : Form
    {

        public static BindingSource bstumtanimlar = new BindingSource();

        private SimpleTreeLookup()
        {
            InitializeComponent();
        }

        public static int Select(DataTable table)
        {
            int result = 0;

            SimpleTreeLookup lookup = new SimpleTreeLookup();
            lookup.StartPosition = FormStartPosition.CenterScreen;
            bstumtanimlar.DataSource = table;
            lookup.GridTumTeshis.DataSource = bstumtanimlar;
            lookup.ShowDialog();

            if (lookup.DialogResult == DialogResult.OK)
            {
                result = 0;
                if (bstumtanimlar.Current!=null && Convert.ToInt32((bstumtanimlar.Current as DataRowView)["Id"])>0)
                {
                    result = Convert.ToInt32((bstumtanimlar.Current as DataRowView)["Id"]);
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
        public static int Select(DataTable table, string fieldname, ref string deger)
        {
            int result = 0;

            SimpleTreeLookup lookup = new SimpleTreeLookup();
            lookup.StartPosition = FormStartPosition.CenterScreen;


            bstumtanimlar.DataSource = table;

            lookup.GridTumTeshis.DataSource = bstumtanimlar;


            lookup.ShowDialog();

            if (lookup.DialogResult == DialogResult.OK)
            {
                result = 0;
                //if (lookup.GridTumTeshis.CurrentRow != null && Convert.ToInt32(lookup.GridTumTeshis.CurrentRow.Cells["Id"].Value) > 0)
                //{
                //    result = Convert.ToInt32(lookup.GridTumTeshis.CurrentRow.Cells["Id"].Value);
                //    deger = lookup.GridTumTeshis.CurrentRow.Cells[fieldname].Value.ToString();
                //}
                if (bstumtanimlar.Current != null && Convert.ToInt32((bstumtanimlar.Current as DataRowView)["Id"]) > 0)
                {
                    result = Convert.ToInt32((bstumtanimlar.Current as DataRowView)["Id"]);
                    deger = (bstumtanimlar.Current as DataRowView)[fieldname].ToString();
                }
            }
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

        private void Filtrele(string adi, string kodu)
        {
            string sql=@"Select Id,Adi,Kodu,UstTeshis_Id from Teshis where Aktif=1";
            if(adi.Length>0)
            {
                sql+=" and Adi like '%"+adi+"%'";
            }
            if(kodu.Length>0)
            {
                 sql+=" and Kodu like '%"+kodu+"%'";
            }

            bstumtanimlar.DataSource= SharpBullet.OAL.Transaction.Instance.ExecuteSql(sql);

            GridTumTeshis.DataSource = bstumtanimlar;


        }

        private void textBoxAdi_TextChanged(object sender, EventArgs e)
        {
            Filtrele(textBoxAdi.Text, textBox2.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Filtrele(textBoxAdi.Text, textBox2.Text);
        }

        private void GridTumTeshis_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}