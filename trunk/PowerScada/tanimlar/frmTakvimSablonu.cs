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

namespace AHBS2010
{
    public partial class frmTakvimSablonu : frmInfoForm
    {
        private BindingList<TakvimSablonSatiri> Sablonsatiri = new BindingList<TakvimSablonSatiri>();
              
        public frmTakvimSablonu()
        {
           
            InitializeComponent();
            InitdataControl();
            EditNew(null);
           
            //CommandAdd();
      }

        public frmTakvimSablonu(long id, EkranDurumu ekrandurumu)
        {
            InitializeComponent();
            InitdataControl();
            //CommandAdd();
            this.EkranDurumu = ekrandurumu;
            EditById(id);
            
        }

        protected override Entity findEntity(long id)
        {
            infoformentity = Persistence.Read<TakvimSablonu>(id);
          
            TakvimSablonSatiri[] sablonsatiri = TakvimSablonu.ReadSablonSatiri(infoformentity.Id);
            ((TakvimSablonu)infoformentity).SablonSatiri = new BindingList<TakvimSablonSatiri>();
            if (sablonsatiri != null)
            {
                Sablonsatiri.Clear();
                foreach (TakvimSablonSatiri item in sablonsatiri)
                {
                    ((TakvimSablonu)infoformentity).SablonSatiri.Add(item);
                    Sablonsatiri.Add(item);
                }
            }
            return ((TakvimSablonu)infoformentity);

            

        }

        protected  void InitdataControl()
        {
            

                   Grid.SetGridStyle(
                 @" <Style>
                        <Column Name='id' HeaderText='Id' Width='40' DisplayIndex='0' Visible='false' ReadOnly='false'/>
                        <Column Name='Adi' HeaderText='İzlem Adi' Width='100' DisplayIndex='1'  Visible='true' ReadOnly='false'/>
                        <Column Name='IzlemSıraNo' HeaderText='İzlem No' Width='75' DisplayIndex='2'  Visible='true' ReadOnly='true'/>
                        <Column Name='IlkIzlemdenSonrakiSure' HeaderText='Süre' Width='75' DisplayIndex='3' Visible='true' ReadOnly='false'/>
                     </Style>");
                   
        }

        protected override Entity getNewEntity()
        {
            Sablonsatiri = new BindingList<TakvimSablonSatiri>();

            return new TakvimSablonu();
        }

        protected override void ShowEntityData()
        {
            TakvimSablonu sablon = ((TakvimSablonu)infoformentity);
            textEditSablonAdi.Text = sablon.Adi;
            ucEnumGosterSablonTuru.Deger = (myenum.IzlemTuru)sablon.SablonTuru;
            
            Grid.DataSource = Sablonsatiri;
        }


        private void KisiNotlariUpdate()
        {
            ((TakvimSablonu)infoformentity).SablonSatiri.Clear();
            foreach (TakvimSablonSatiri item in Sablonsatiri)
            {
               
                ((TakvimSablonu)infoformentity).SablonSatiri.Add(item);

            }
          
        }

        protected override void UpdateEntityData()
        {
            TakvimSablonu sablon = ((TakvimSablonu)infoformentity);
            sablon.Adi = textEditSablonAdi.Text;
            sablon.SablonTuru=(myenum.IzlemTuru)ucEnumGosterSablonTuru.Deger;
           
            KisiNotlariUpdate();
        }

        protected override void Save()
        {

           mymodel.TakvimSablonu sablon = ((TakvimSablonu)infoformentity);
            List<SharpBullet.ActiveRecord.ActiveRecordBase> childlist = new List<SharpBullet.ActiveRecord.ActiveRecordBase>();
            foreach (TakvimSablonSatiri entity in Sablonsatiri)
            {
                childlist.Add((SharpBullet.ActiveRecord.ActiveRecordBase)entity);
            }
           
           
            sablon.SetChilds(childlist);
            base.Save();
                //Transaction.Instance.Join(delegate()
                //{
                    //base.Save();
                //    try
                //    {
                //        int i = Transaction.Instance.ExecuteNonQuery(" delete from TakvimSablonSatiri where TakvimSablonu_Id=@prm0", infoformentity.Id);
                //    }
                //    catch (Exception)
                //    {
                //        throw new Exception("Takvim Sablon Satırları silinemdi");
                //    }
                //    foreach (TakvimSablonSatiri sablonsatiri in ((TakvimSablonu)infoformentity).SablonSatiri)
                //    {

                //        sablonsatiri.TakvimSablonu.Id = infoformentity.Id;
                //        sablonsatiri.Insert();
                //    }
                //});

          

          


        }

       

        private void Grid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 1)
            {
                Grid.Rows[e.RowIndex].Cells["IzlemSıraNo"].Value = int.Parse(Grid.Rows[e.RowIndex-1].Cells["IzlemSıraNo"].Value.ToString()) + 1;
            }
            else
                Grid.Rows[e.RowIndex].Cells["IzlemSıraNo"].Value = 1;
        }

        private void Grid_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
           

            if (e.RowIndex >= 1)
            {
                Grid.Rows[e.RowIndex].Cells["IzlemSıraNo"].Value = int.Parse(Grid.Rows[e.RowIndex-1].Cells["IzlemSıraNo"].Value.ToString()) + 1;
            }
            else
            {
                Grid.Rows[e.RowIndex].Cells["IzlemSıraNo"].Value = 1;
            }
            
        }

        private void Grid_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (Grid.Rows[e.RowIndex].Cells["IlkIzlemdenSonrakiSure"].Value!=null &&
                Grid.Rows[e.RowIndex].Cells["IlkIzlemdenSonrakiSure"].Value!=System.DBNull.Value &&
                int.Parse(Grid.Rows[e.RowIndex].Cells["IlkIzlemdenSonrakiSure"].Value.ToString())==0)
            {

                if (Grid.Rows[e.RowIndex].Cells["Adi"].Value != null &&
                Grid.Rows[e.RowIndex].Cells["Adi"].Value != System.DBNull.Value &&
                (Grid.Rows[e.RowIndex].Cells["Adi"].Value.ToString()) == string.Empty)
                {
                    Sablonsatiri.RemoveAt(e.RowIndex);
                }
 
               
            }
           
        }


       
        
    }
}

