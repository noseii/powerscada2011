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
    public partial class frmOlumBildirimi : frmDialogBase
    {

        private OlumBildirimi olumbildirimi;

        public OlumBildirimi OlumBildirimiEntity
        {
            get
            {
                if (olumbildirimi == null)
                    olumbildirimi = (OlumBildirimi)CommandNew();
                else
                    olumbildirimi = (OlumBildirimi)formEntity;
                return olumbildirimi;
            }
            set
            {
                olumbildirimi = value;
            }
        }

        public frmOlumBildirimi()
        {
            InitializeComponent();
            this.formState = mymodel.myenum.EditMode.emYeni;
            InitDataControl();
            this.Load += new EventHandler(frmAnemnez_Load);

            InitializeForm();
        }

        public frmOlumBildirimi(long Id, mymodel.myenum.EditMode formstate)
        {
            InitializeComponent();
            this.formState = formstate;
            InitDataControl();
            this.Load += new EventHandler(frmAnemnez_Load);
            
            InitializeForm(Id, formstate);
        }

        void frmAnemnez_Load(object sender, EventArgs e)
        {
            if (Current.AktifMuayeneId > 0)
            {
                DateEditIzlemTarihi.DateTime = Current.AktifMuayene.MuayeneTarihi;
                DateEditIzlemTarihi.Enabled = false;
            }
        }

        public override void updatedata()
        {
            OlumBildirimiEntity.Hasta = Current.AktifHasta;
            OlumBildirimiEntity.Hasta.Id = Current.AktifHastaId;
            OlumBildirimiEntity.Doktor.Id = Current.AktifDoktorId;
            OlumBildirimiEntity.Doktor = Current.AktifDoktor;

            OlumBildirimiEntity.OlumTarihi =Convert.ToDateTime(dateEditolumTarihi.EditValue);
            OlumBildirimiEntity.OlumYeri = MemoEditolumYeri.Text;
            OlumBildirimiEntity.Teshis1.Id = editButtonTeshis1.Id;
            OlumBildirimiEntity.Teshis2.Id = editButtonTeshis2.Id;
            OlumBildirimiEntity.Teshis3.Id = editButtonTeshis3.Id;
      
            OlumBildirimiEntity.IzlemTarihi = dateEditolumTarihi.DateTime;
         
        }

        public override void showdata()
        {

           
            textEditHastaAdiSoyadi.Text=Current.AktifHasta.Adi + Current.AktifHasta.Soyadi;
            if (OlumBildirimiEntity.OlumTarihi !=System.DateTime.MinValue)
                dateEditolumTarihi.EditValue = OlumBildirimiEntity.OlumTarihi;
            
            MemoEditolumYeri.Text = OlumBildirimiEntity.OlumYeri;

            if (OlumBildirimiEntity.Teshis1.Id > 0)
            {
                
                editButtonTeshis1.Id = OlumBildirimiEntity.Teshis1.Id;
                OlumBildirimiEntity.Teshis1 = SharpBullet.OAL.Persistence.Read<Teshis>(editButtonTeshis1.Id); ; 
                editButtonTeshis1.Text = OlumBildirimiEntity.Teshis1.Adi;
            }

            if (OlumBildirimiEntity.Teshis2.Id > 0)
            {
                editButtonTeshis2.Id = OlumBildirimiEntity.Teshis2.Id;
                OlumBildirimiEntity.Teshis2 = SharpBullet.OAL.Persistence.Read<Teshis>(editButtonTeshis2.Id);
                editButtonTeshis2.Text = OlumBildirimiEntity.Teshis2.Adi;
            }

            if (OlumBildirimiEntity.Teshis3.Id > 0)
            {
                editButtonTeshis3.Id = OlumBildirimiEntity.Teshis3.Id;
                OlumBildirimiEntity.Teshis3 = SharpBullet.OAL.Persistence.Read<Teshis>(editButtonTeshis3.Id);
               
                editButtonTeshis3.Text = OlumBildirimiEntity.Teshis3.Adi;
            }
            dateEditolumTarihi.DateTime = OlumBildirimiEntity.OlumTarihi;
        }

        protected override Entity CommandNew()
        {
            OlumBildirimi olumbildirimi = new OlumBildirimi();
            olumbildirimi.Hasta.Id = Current.AktifHastaId;
            olumbildirimi.Hasta = Current.AktifHasta;

            olumbildirimi.Doktor.Id = Current.AktifHasta.Doktor.Id;
            if (Current.AktifDoktorId != olumbildirimi.Doktor.Id)
            {
                olumbildirimi.VekilDoktor.Id = Current.AktifDoktorId;
                olumbildirimi.VekilDoktor = Current.AktifDoktor;
            }
            if (Current.AktifMuayeneId > 0)
            {
                olumbildirimi.Muayene.Id = Current.AktifMuayeneId;
                olumbildirimi.Muayene = Current.AktifMuayene;
            }

            if (Current.AktifRandevuId > 0)
            {
                olumbildirimi.Randevu.Id = Current.AktifRandevuId;
                olumbildirimi.Randevu = Current.AktifRandevu;
            }

            return olumbildirimi;
        }

        protected override void CommandRead(long objId)
        {
            formEntity = SharpBullet.OAL.Persistence.Read<OlumBildirimi>(objId);
            OlumBildirimiEntity = (OlumBildirimi)formEntity;
            if (OlumBildirimiEntity.Hasta.Id > 0)
                OlumBildirimiEntity.Hasta = Persistence.Read<Hasta>(OlumBildirimiEntity.Hasta.Id);

         }


        public override void formtamam()
        {
            base.formtamam();
            //if (Current.AktifMuayeneId > 0)
            //    Muayene.UpdateMuayenedurumu(Current.AktifMuayeneId, myenum.MuayeneDurumu.MuayeneEdildi);
 
        }
       
    }
}