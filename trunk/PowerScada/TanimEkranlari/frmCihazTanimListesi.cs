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
    public partial class frmCihazTanimListesi : frmListForm 
    {

        public frmCihazTanimListesi()
        {
            InitializeComponent();
            showData(null);
            this.EntityType = typeof(Cihaz);
            this.Text = Utility.FormName(this.EntityType);
            grid.SetGridStyle(@"<Style>
                    <Column Name='Id'               HeaderText='Id'                         Width='0'   DisplayIndex='0'    Visible='false' />
                    <Column Name='LokasyonAdi'      HeaderText='LokasyonAdi'                Width='100' DisplayIndex='1'    Visible='true'  />
                    <Column Name='Adi'              HeaderText='Adi'                        Width='100' DisplayIndex='2'    Visible='true'  />
                    <Column Name='Kodu'             HeaderText='Kodu'                       Width='100' DisplayIndex='3'    Visible='true' />
                    <Column Name='Aciklama'         HeaderText='Aciklama'                   Width='100' DisplayIndex='4'    Visible='true' />
                    <Column Name='Aktif'            HeaderText='Aktif'                      Width='100' DisplayIndex='5'    Visible='true'    Type ='Checkbox' />                 
            </Style>");
        }
        public frmCihazTanimListesi(bool multiselect)
        {
            InitializeComponent();
            this.multiSelect = multiselect;
            showData(null);
            this.EntityType = typeof(Cihaz);
            this.Text = Utility.FormName(this.EntityType);
        }

        protected override DataTable retrieveData()
        {
            return Transaction.Instance.ExecuteSql(@"    Select 
                                                            *,Lokasyon.Adi as LokasyonAdi
                                                         From Cihaz
                                                         Inner Join Lokasyon on Lokasyon.Id=Cihaz.Lokasyon_Id
                                                          
                                                                
                                                      ");
        }

        protected override frmInfoForm newInfoForm()
        {
            return new frmCihazTanim();
        }

        protected override Entity findById(int oid)
        {
            return Persistence.Read<Cihaz>(oid);
        }

        protected override void New(object sender)
        {
            frmCihazTanim lokasyonform = new frmCihazTanim();
            lokasyonform.MdiParent = this.MdiParent;
            lokasyonform.Text = "Cihaz Tanım Formu";
            lokasyonform.Show();

            //((MdiTabControl.TabControl)(this.Parent.Parent)).TabPages.Add(kisiform);
        }

        protected override void Edit(object sender)
        {

            if (gridView1.GetFocusedDataRow()["Id"]!=null && gridView1.GetFocusedDataRow()["Id"]!=System.DBNull.Value)
            {
                int objId = Convert.ToInt32(gridView1.GetFocusedDataRow()["Id"]);

                frmCihazTanim sablonform = new frmCihazTanim(objId, EkranDurumu.Duzenle);
                sablonform.Text = "Cihaz Tanım Formu";
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

                frmCihazTanim sablonform = new frmCihazTanim(objId, EkranDurumu.Izle);
                sablonform.MdiParent = this.MdiParent;
                sablonform.Text = "Cihaz Tanım Formu";
                sablonform.Show();
              
                //((MdiTabControl.TabControl)(this.Parent.Parent)).TabPages.Add(ulke);
            }
        }




        protected override string EntityName()
        {
            return "Cihaz";
        }
        
       
     
        
    }
}

