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
    public partial class frmCihazTanim : frmInfoForm
    {
        private BindingList<CihazAdres> CihazAdresleri = new BindingList<CihazAdres>();
              
        public frmCihazTanim()
        {
           
            InitializeComponent();
            InitdataControl();
            EditNew(null);
       
            //CommandAdd();
      }

        public frmCihazTanim(long id, EkranDurumu ekrandurumu)
        {
            InitializeComponent();
            InitdataControl();
            //CommandAdd();
            this.EkranDurumu = ekrandurumu;
            EditById(id);
            
        }

        protected override Entity findEntity(long id)
        {
            infoformentity = Persistence.Read<Cihaz>(id);

            CihazAdres[] cihazadresler = CihazAdres.ReadCihazAdresleri(infoformentity.Id);
            ((Cihaz)infoformentity).CihazAdresleri = new BindingList<CihazAdres>();
            if (cihazadresler != null)
            {
                CihazAdresleri.Clear();
                foreach (CihazAdres item in cihazadresler)
                {
                    CihazAdresleri.Add(item);
                }
            }

            return ((Cihaz)infoformentity);

            

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
            CihazAdresleri = new BindingList<CihazAdres>();

            return new Cihaz();
        }

        protected override void ShowEntityData()
        {
            Cihaz cihaz = ((Cihaz)infoformentity);
            if(cihaz.CihazTuru.Id>0)
            {
               LookupTable cihazturu=Persistence.Read<LookupTable>(cihaz.cihazturu.Id);
               editButtonCihazTuru.Id = cihazturu.Id;
               editButtonCihazTuru.Text = cihazturu.Adi;
            }
            textEditAdi.Text = cihaz.Adi;
            textEditkodu.Text = cihaz.Kodu;
            memoEditAciklama.Text = cihaz.Aciklama;
            myComboDavranis.Id = (int)cihaz.Davranis;

            GridAdresler.DataSource = CihazAdresleri;
         
        }


       

        protected override void UpdateEntityData()
        {
            Cihaz cihaz = ((Cihaz)infoformentity);
            cihaz.Adi = textEditAdi.Text;
            cihaz.Kodu = textEditkodu.Text;
            cihaz.Aciklama = memoEditAciklama.Text;
            cihaz.Davranis=(myenum.Davranis)myComboDavranis.Id;
            cihaz.CihazTuru.Id = editButtonCihazTuru.Id;
            KisiNotlariUpdate();    
        }

        private void KisiNotlariUpdate()
        {
            ((Cihaz)infoformentity).CihazAdresleri.Clear();
            foreach (CihazAdres item in CihazAdresleri)
            {

                ((Cihaz)infoformentity).CihazAdresleri.Add(item);

            }

        }

        protected override void Save()
        {
            Transaction.Instance.Join(delegate()
            {
                base.Save();
                try
                {
                    int i = Transaction.Instance.ExecuteNonQuery(" delete from CihazAdres where Cihaz_Id=@prm0", infoformentity.Id);
                }
                catch (Exception)
                {
                    throw new Exception("Cihaz ait adresler silinemdi");
                }
                foreach (CihazAdres chzadres in ((Cihaz)infoformentity).CihazAdresleri)
                {

                    chzadres.Cihaz.Id = infoformentity.Id;
                    chzadres.Adres.Id = 0;
                    chzadres.Insert();
                }
            });

            
        }

      

      

      

     


       
        
    }
}

