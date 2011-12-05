using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mymodel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using SharpBullet.OAL;
using AHBS2010;

namespace AHBS2010
{
    public partial class frmTakvimSablonListesi : frmListForm 
    {

        public frmTakvimSablonListesi()
        {
            InitializeComponent();
            showData(null);
            this.EntityType = typeof(TakvimSablonu);
            this.Text = Utility.FormName(this.EntityType);
        }


        protected override DataTable retrieveData()
        {
            return Transaction.Instance.ExecuteSql(@"select 
                                                         TakvimSablonu.Id
                                                         ,TakvimSablonu.Adi
                                                         ,TakvimSablonu.SablonTuru
                                                         ,TakvimSablonu.Aktif
                                                         from TakvimSablonu
                                                      ");
        }

        protected override frmInfoForm newInfoForm()
        {
            return new frmTakvimSablonu();
        }

        protected override Entity findById(int oid)
        {
            return Persistence.Read<TakvimSablonu>(oid);
        }

        protected override void New(object sender)
        {
            frmTakvimSablonu sablonform = new frmTakvimSablonu();
            sablonform.MdiParent = this.MdiParent;
            sablonform.Text = "Takvim Şablon Formu";
            sablonform.Show();

            //((MdiTabControl.TabControl)(this.Parent.Parent)).TabPages.Add(kisiform);
        }

        protected override void Edit(object sender)
        {

            if (grid.CurrentRow.Cells["id"].Value != null && grid.CurrentRow.Cells["id"].Value != System.DBNull.Value)
            {
                int objId = Convert.ToInt32(grid.CurrentRow.Cells["id"].Value);

                frmTakvimSablonu sablonform = new frmTakvimSablonu(objId, EkranDurumu.Duzenle);
                sablonform.Text = "Takvim Şablon Formu";
                sablonform.MdiParent = this.MdiParent;
                sablonform.Show();
               
                //((MdiTabControl.TabControl)(this.Parent.Parent)).TabPages.Add(ulke);
            }
        }
        protected override void View(object sender)
        {
            if (grid.CurrentRow != null && grid.CurrentRow.Cells["id"].Value != null && grid.CurrentRow.Cells["id"].Value != System.DBNull.Value)
            {
                int objId = Convert.ToInt32(grid.CurrentRow.Cells["id"].Value);

                frmTakvimSablonu sablonform = new frmTakvimSablonu(objId, EkranDurumu.Izle);
                sablonform.MdiParent = this.MdiParent;
                sablonform.Text = "Takvim Şablon Formu";
                sablonform.Show();
              
                //((MdiTabControl.TabControl)(this.Parent.Parent)).TabPages.Add(ulke);
            }
        }

      

        

     
        
    }
}

