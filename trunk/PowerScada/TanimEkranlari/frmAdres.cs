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
    public partial class frmAdres : frmInfoForm
    {
       
              
        public frmAdres()
        {
           
            InitializeComponent();
            InitdataControl();
            EditNew(null);
           
            //CommandAdd();
      }

        public frmAdres(long id, EkranDurumu ekrandurumu)
        {
            InitializeComponent();
            InitdataControl();
            //CommandAdd();
            this.EkranDurumu = ekrandurumu;
            EditById(id);
            
        }

        protected override Entity findEntity(long id)
        {
            infoformentity = Persistence.Read<Adres>(id);
          
            return ((Adres)infoformentity);
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
          

            return new Adres();
        }

        protected override void ShowEntityData()
        {
            Adres sablon = ((Adres)infoformentity);
            textEditSablonAdi.Text = sablon.TagAdresi;
        }


      

        protected override void UpdateEntityData()
        {
            Adres sablon = ((Adres)infoformentity);
            sablon.TagAdresi = textEditSablonAdi.Text;
        }

        protected override void Save()
        {

            mymodel.Adres sablon = ((Adres)infoformentity);
            base.Save();
        }
    }
}

