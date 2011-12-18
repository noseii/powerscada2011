using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpBullet.OAL;
using DevExpress.XtraGrid.Columns;

namespace PowerScada
{
    public partial class frmRaporBase : Form
    {
        public DataTable dtrapor = null;
        public frmRaporBase()
        {
            InitializeComponent();
            
            simpleButtonCikis.Click += new EventHandler(simpleButtonCikis_Click);
            simpleButtonExceleAktar.Click += new EventHandler(simpleButtonExceleAktar_Click);
            simpleButtonGoruntule.Click += new EventHandler(simpleButtonGoruntule_Click);
           
        }

        void simpleButtonGoruntule_Click(object sender, EventArgs e)
        {
            Goruntule();
        }

        void simpleButtonExceleAktar_Click(object sender, EventArgs e)
        {
            ExceleAktar();
        }

        void simpleButtonCikis_Click(object sender, EventArgs e)
        {
            Cikis();
        }


        public virtual void Cikis()
        {
            this.Close();
        }

        public virtual void ExceleAktar()
        {
            SaveFileDialog sdialog = new SaveFileDialog();
            sdialog.FileName = "Rapor.xls";
            
            sdialog.DefaultExt = "Excel Belgesi *.xls|*.xls";
            if (sdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    grid.ExportToXls(sdialog.FileName);
                    MessageBox.Show("İşlem Tamamlandı.");
                }
                catch (Exception ex)
                {

                    throw new Exception("İşlem Yapılamadı."); ;
                }


            }
        }

        public virtual void Goruntule()
        {
            Sorgula();
        }


        public virtual void Sorgula()
        {
            if (Validate())
            {
                dtrapor = Transaction.Instance.ExecuteSql(Sql());
                gridView.Columns.Clear();
                grid.DataSource = dtrapor;
                foreach (GridColumn item in gridView.Columns)
                    item.Width = 100;
                gridView.ViewCaption = "Bulunan Kayıt Sayısı:" + dtrapor.Rows.Count.ToString();
            }

        }

        public virtual bool Validate()
        {
            return true;
        }

        public virtual string Sql()
        {

            return string.Empty;
        }
    }
}
