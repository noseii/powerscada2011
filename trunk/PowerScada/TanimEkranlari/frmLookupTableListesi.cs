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
    public partial class frmLookupTableListesi : frmListForm 
    {

        public frmLookupTableListesi()
        {
            InitializeComponent();
            showData(null);
            this.EntityType = typeof(LookupTable);
            this.Text = Utility.FormName(this.EntityType);
        }


        protected override DataTable retrieveData()
        {
            return Transaction.Instance.ExecuteSql(@"select 
                                                         *
                                                         from LookupTable
                                                      ");
        }

        protected override frmInfoForm newInfoForm()
        {
            return new frmLookupTable();
        }

        protected override Entity findById(int oid)
        {
            return Persistence.Read<LookupTable>(oid);
        }

        protected override void New(object sender)
        {
            frmLookupTable lokasyonform = new frmLookupTable();
            lokasyonform.MdiParent = this.MdiParent;
            lokasyonform.Text = "LookupTable Tanım Formu";
            lokasyonform.Show();

            //((MdiTabControl.TabControl)(this.Parent.Parent)).TabPages.Add(kisiform);
        }

        protected override void Edit(object sender)
        {

            if (grid.CurrentRow.Cells["id"].Value != null && grid.CurrentRow.Cells["id"].Value != System.DBNull.Value)
            {
                int objId = Convert.ToInt32(grid.CurrentRow.Cells["id"].Value);

                frmLookupTable sablonform = new frmLookupTable(objId, EkranDurumu.Duzenle);
                sablonform.Text = "LookupTable Tanım Formu";
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

                frmLookupTable sablonform = new frmLookupTable(objId, EkranDurumu.Izle);
                sablonform.MdiParent = this.MdiParent;
                sablonform.Text = "LookupTable Tanım Formu";
                sablonform.Show();
              
                //((MdiTabControl.TabControl)(this.Parent.Parent)).TabPages.Add(ulke);
            }
        }

      

        

     
        
    }
}

