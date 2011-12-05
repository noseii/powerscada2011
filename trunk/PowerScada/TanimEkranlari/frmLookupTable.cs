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

namespace PowerScada
{
    public partial class frmLookupTable : frmInfoForm
    {
       
              
        public frmLookupTable()
        {
           
            InitializeComponent();
            InitdataControl();
            EditNew(null);
           
            //CommandAdd();
      }

        public frmLookupTable(long id, EkranDurumu ekrandurumu)
        {
            InitializeComponent();
            InitdataControl();
            //CommandAdd();
            this.EkranDurumu = ekrandurumu;
            EditById(id);
            
        }

        protected override Entity findEntity(long id)
        {
            infoformentity = Persistence.Read<LookupTable>(id);

            return ((LookupTable)infoformentity);

            

        }

        protected  void InitdataControl()
        {
            

//                   Grid.SetGridStyle(
//                 @" <Style>
//                        <Column Name='id' HeaderText='Id' Width='40' DisplayIndex='0' Visible='false' ReadOnly='false'/>
//                        <Column Name='Adi' HeaderText='İzlem Adi' Width='100' DisplayIndex='1'  Visible='true' ReadOnly='false'/>
//                        <Column Name='IzlemSıraNo' HeaderText='İzlem No' Width='75' DisplayIndex='2'  Visible='true' ReadOnly='true'/>
//                        <Column Name='IlkIzlemdenSonrakiSure' HeaderText='Süre' Width='75' DisplayIndex='3' Visible='true' ReadOnly='false'/>
//                     </Style>");
                   
        }

        protected override Entity getNewEntity()
        {
          

            return new LookupTable();
        }

        protected override void ShowEntityData()
        {
            LookupTable lookup = ((LookupTable)infoformentity);
            textEditAdi.Text = lookup.Adi;
            textEditkodu.Text = lookup.Kodu;
            memoEditAciklama.Text = lookup.Aciklama;
            myComboTipi.Id = (int)lookup.Tip;
            //ucEnumGosterSablonTuru.Deger = (myenum.IzlemTuru)sablon.SablonTuru;
         
        }


       

        protected override void UpdateEntityData()
        {
            LookupTable lookup = ((LookupTable)infoformentity);
            lookup.Adi = textEditAdi.Text;
            lookup.Kodu = textEditkodu.Text;
            lookup.Aciklama = memoEditAciklama.Text;
            lookup.Tip = (myenum.ParametreTipi)myComboTipi.Id;
           
        }

        protected override void Save()
        {

            mymodel.LookupTable lookup = ((LookupTable)infoformentity);
            base.Save();
       
        }

       

      

      

     


       
        
    }
}

