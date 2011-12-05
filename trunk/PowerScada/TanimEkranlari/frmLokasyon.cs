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
    public partial class frmLokasyon : frmInfoForm
    {
        private BindingList<Lokasyon> Sablonsatiri = new BindingList<Lokasyon>();
              
        public frmLokasyon()
        {
           
            InitializeComponent();
            InitdataControl();
            EditNew(null);
           
            //CommandAdd();
      }

        public frmLokasyon(long id, EkranDurumu ekrandurumu)
        {
            InitializeComponent();
            InitdataControl();
            //CommandAdd();
            this.EkranDurumu = ekrandurumu;
            EditById(id);
            
        }

        protected override Entity findEntity(long id)
        {
            infoformentity = Persistence.Read<Lokasyon>(id);

            return ((Lokasyon)infoformentity);

            

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
            Sablonsatiri = new BindingList<Lokasyon>();

            return new Lokasyon();
        }

        protected override void ShowEntityData()
        {
            Lokasyon sablon = ((Lokasyon)infoformentity);
            textEditAdi.Text = sablon.Adi;
            textEditkodu.Text = sablon.Kodu;
            memoEditAciklama.Text = sablon.Aciklama;
            if (sablon.UstLokasyon.Id > 0)
            {
                Lokasyon ustlokasyon = Persistence.Read<Lokasyon>(sablon.UstLokasyon.Id);
                editButtonLokasyon.Id = ustlokasyon.Id;
                editButtonLokasyon.Text = ustlokasyon.Adi;
                sablon.UstLokasyon = ustlokasyon;
            }

            if (sablon.Adres.Id > 0)
            {
                Adres adres = Persistence.Read<Adres>(sablon.Adres.Id);
                editButtonHataAdresi.Id = adres.Id;
                editButtonHataAdresi.Text = adres.TagAdresi;
                sablon.Adres = adres;
            }
        }


       

        protected override void UpdateEntityData()
        {
            Lokasyon sablon = ((Lokasyon)infoformentity);
            sablon.Adi = textEditAdi.Text;
            sablon.Kodu = textEditkodu.Text;
            sablon.Aciklama = memoEditAciklama.Text;
            sablon.UstLokasyon.Id = editButtonLokasyon.Id;
            sablon.Adres.Id = editButtonHataAdresi.Id;
            //sablon.SablonTuru=(myenum.IzlemTuru)ucEnumGosterSablonTuru.Deger;
           
           
        }

        protected override void Save()
        {

            mymodel.Lokasyon sablon = ((Lokasyon)infoformentity);
            base.Save();
       
        }

       

      

      

     


       
        
    }
}

