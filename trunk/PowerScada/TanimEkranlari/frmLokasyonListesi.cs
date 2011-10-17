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
using PowerScada;
using PowerScada;

namespace PowerScada
{
    public partial class frmLokasyonListesi : frmListForm 
    {

        public frmLokasyonListesi()
        {
            InitializeComponent();
            showData(null);
            this.EntityType = typeof(Lokasyon);
            this.Text = Utility.FormName(this.EntityType);
        }


        protected override DataTable retrieveData()
        {
            return Transaction.Instance.ExecuteSql(@"select 
                                                         *
                                                         from Lokasyon
                                                      ");
        }

        protected override frmInfoForm newInfoForm()
        {
            return new frmLokasyon();
        }

        protected override Entity findById(int oid)
        {
            return Persistence.Read<Lokasyon>(oid);
        }

        protected override void New(object sender)
        {
            frmLokasyon lokasyonform = new frmLokasyon();
            lokasyonform.MdiParent = this.MdiParent;
            lokasyonform.Text = "Lokasyon Tanım Formu";
            lokasyonform.Show();

            //((MdiTabControl.TabControl)(this.Parent.Parent)).TabPages.Add(kisiform);
        }

        protected override void Edit(object sender)
        {

            if (gridView1.GetFocusedDataRow()["Id"]!=null && gridView1.GetFocusedDataRow()["Id"]!=System.DBNull.Value)
            {
                int objId = Convert.ToInt32(gridView1.GetFocusedDataRow()["Id"]);

                frmLokasyon sablonform = new frmLokasyon(objId, EkranDurumu.Duzenle);
                sablonform.Text = "Lokasyon Tanım Formu";
                sablonform.MdiParent = this.MdiParent;
                sablonform.Show();
               
                //((MdiTabControl.TabControl)(this.Parent.Parent)).TabPages.Add(ulke);
            }
        }
        protected override void View(object sender)
        {
            if (gridView1.GetFocusedDataRow()["Id"]!=null && gridView1.GetFocusedDataRow()["Id"]!=System.DBNull.Value)
            {
                int objId = Convert.ToInt32(gridView1.GetFocusedDataRow()["Id"]);

                frmLokasyon sablonform = new frmLokasyon(objId, EkranDurumu.Izle);
                sablonform.MdiParent = this.MdiParent;
                sablonform.Text = "Lokasyon Tanım Formu";
                sablonform.Show();
              
                //((MdiTabControl.TabControl)(this.Parent.Parent)).TabPages.Add(ulke);
            }
        }


        protected override string EntityName()
        {
            return "Lokasyon";
        }
        

     
        
    }
}

