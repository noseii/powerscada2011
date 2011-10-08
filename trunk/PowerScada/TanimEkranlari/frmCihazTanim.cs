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
using PowerScada.DataGridViewAutoFilter;
using PowerScada.DataGridViewAutoFilter;


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

           

           

            //listTable = PbiOrder.GetOrderedList();
            //listTable.RowChanged += new DataRowChangeEventHandler(listTable_RowChanged);
            //bindingSource1.DataSource = listTable;
            GridAdresler.SetGridStyle(
                @"<Style>
                    <Column Name='Id' HeaderText='Id' Width='49' DisplayIndex='0' />
                    <Column Name='Adres_Id' HeaderText='Adres_Id' Width='100' DisplayIndex='1' Visible='false'/>                    
                    <Column Name='AdresAdi' HeaderText='Adres Adı' Width='60' DisplayIndex='2' />
                    <Column Name='AdresSec' HeaderText='Adres Seçiniz' Width='150' DisplayIndex='3' />
                    <Column Name='Cihaz_Id' HeaderText='İstek' Width='563' DisplayIndex='4' Visible='false'/>
                    <Column Name='CihazAdi' HeaderText='Cihaz Adı' Width='563' DisplayIndex='5' />
                    <Column Name='CihazSec' HeaderText='Cihaz Seçiniz' Width='150' DisplayIndex='6'  type == 'Button'/>
                    <Column Name='Formul' HeaderText='Formül' Width='115' DisplayIndex='7' />                 
                </Style>");
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

