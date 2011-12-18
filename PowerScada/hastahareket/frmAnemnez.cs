using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using mymodel;
using SharpBullet.OAL;

namespace AHBS2010
{
    public partial class frmAnemnez : frmDialogBase
    {

        private Anamnez anamnez;

        public Anamnez AnamnezEntity
        {
            get
            {
                if (anamnez == null)
                    anamnez = (Anamnez)CommandNew();
                else
                    anamnez = (Anamnez)formEntity;
                return anamnez;
            }
            set
            {
                anamnez = value;
            }
        }

        public frmAnemnez()
        {
            InitializeComponent();
            this.formState = mymodel.myenum.EditMode.emYeni;
            InitDataControl();
            this.Load += new EventHandler(frmAnemnez_Load);

            InitializeForm();
        }

        void frmAnemnez_Load(object sender, EventArgs e)
        {
            edtnabiz.Properties.MinValue = 0;
            edtnabiz.Properties.MaxValue = 1000000;

            edtbuyuktansiyon.Properties.MinValue = 0;
            edtbuyuktansiyon.Properties.MaxValue = 1000000;

            edtkucuktansiyon.Properties.MinValue = 0;
            edtkucuktansiyon.Properties.MaxValue = 1000000;


            edtates.Properties.MinValue = 0;
            edtates.Properties.MaxValue = 1000000;
            
            edtboy.Properties.MinValue = 0;
            edtboy.Properties.MaxValue = 1000000;


            edtkilo.Properties.MinValue = 0;
            edtkilo.Properties.MaxValue = 1000000;
        }

        public frmAnemnez(long Id, mymodel.myenum.EditMode formstate)
        {
            InitializeComponent();
            this.formState = formstate;
            InitDataControl();
            //this.Load += new EventHandler(frmAnemnez_Load);
            
            InitializeForm(Id, formstate);
        }

        //void frmAnemnez_Load(object sender, EventArgs e)
        //{

        //}

        public override void updatedata()
        {  
           AnamnezEntity.Muayene = Current.AktifMuayene;
           AnamnezEntity.Muayene.Id = Current.AktifMuayeneId;
           AnamnezEntity.Agirligi = Convert.ToByte(edtkilo.EditValue);
           AnamnezEntity.Ates = Convert.ToByte(edtates.EditValue);
           AnamnezEntity.Nabiz = Convert.ToByte(edtnabiz.EditValue);
           AnamnezEntity.KucukTansiyon = Convert.ToByte(edtkucuktansiyon.EditValue);
           AnamnezEntity.BuyukTansiyon = Convert.ToByte(edtbuyuktansiyon.EditValue);
           AnamnezEntity.Boyu = Convert.ToByte(edtboy.EditValue);
           AnamnezEntity.Sikayet = edtsikayet.Text;
           AnamnezEntity.Hikaye = edthikaye.Text;
           AnamnezEntity.Ozgecmis = edtozgecmis.Text;
           AnamnezEntity.Soygecmis = edtsoygecmis.Text;
           AnamnezEntity.FizikiMuayene = edtfizikimuayene.Text;
           AnamnezEntity.Tedavi = edttedavi.Text;
           AnamnezEntity.Tetkik = edtlaboratuvarbilgi.Text;
        }

        public override void showdata()
        {  
            edtkilo.EditValue = AnamnezEntity.Agirligi;
            edtates.EditValue = AnamnezEntity.Ates;
            edtnabiz.EditValue = AnamnezEntity.Nabiz;
            edtkucuktansiyon.EditValue = AnamnezEntity.KucukTansiyon;
            edtbuyuktansiyon.EditValue = AnamnezEntity.BuyukTansiyon;
            edtboy.EditValue = AnamnezEntity.Boyu;
            edtsikayet.Text = AnamnezEntity.Sikayet;
            edthikaye.Text = AnamnezEntity.Hikaye;
            edtozgecmis.Text = AnamnezEntity.Ozgecmis;
            edtsoygecmis.Text = AnamnezEntity.Soygecmis;
            edtfizikimuayene.Text = AnamnezEntity.FizikiMuayene;
            edttedavi.Text = AnamnezEntity.Tedavi;
            edtlaboratuvarbilgi.Text = AnamnezEntity.Tetkik;
            
          
        }

        protected override Entity CommandNew()
        {
            Anamnez anamnez = new Anamnez();
            anamnez.Hasta.Id = Current.AktifHastaId;
            anamnez.Hasta = Current.AktifHasta;
            anamnez.Doktor.Id = Current.AktifHasta.Doktor.Id;
            if (Current.AktifDoktorId != anamnez.Doktor.Id)
            {
                anamnez.VekilDoktor.Id = Current.AktifDoktorId;
                anamnez.VekilDoktor = Current.AktifDoktor;
            }

            if (Current.AktifMuayeneId > 0)
            {
                anamnez.Muayene.Id = Current.AktifMuayeneId;
                anamnez.Muayene = Current.AktifMuayene;
            }
           
            if (Current.AktifRandevuId > 0)
            {
                anamnez.Randevu.Id = Current.AktifRandevuId;
                anamnez.Randevu = Current.AktifRandevu;
            }

           
            

            return anamnez;
       }

        protected override void CommandRead(long objId)
        {
            formEntity = SharpBullet.OAL.Persistence.Read<Anamnez>(objId);
            AnamnezEntity = (Anamnez)formEntity;
            if (AnamnezEntity.Hasta.Id > 0)
                AnamnezEntity.Hasta = Persistence.Read<Hasta>(AnamnezEntity.Hasta.Id);

            if (AnamnezEntity.Muayene.Id > 0)
                AnamnezEntity.Muayene = Persistence.Read<Muayene>(AnamnezEntity.Muayene.Id);
        }

        public override void formtamam()
        {
            base.formtamam();
            //if (Current.AktifMuayeneId > 0)
            //    Muayene.UpdateMuayenedurumu(Current.AktifMuayeneId, myenum.MuayeneDurumu.MuayeneEdildi);
           
                if (Current.AktifRandevuId > 0)
                {
                    if (Convert.ToDateTime(AnamnezEntity.EklemeTarihi.ToShortDateString())<Current.AktifRandevu.BasTarih)
                        throw new Exception("İleri tarihli bir randevu işlem yapılamaz.");
               
                    Takvim.UpdateTakvimDurumu(Current.AktifRandevuId, myenum.RandevuDurumu.Geldi);
                }
            
        }

       
    }
}