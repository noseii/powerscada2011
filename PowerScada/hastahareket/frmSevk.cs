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
    public partial class frmSevk : frmDialogBase
    {
        private MuayeneSevk MuayeneSevk;
        public MuayeneSevk MuayeneSevkEntity
        {
            get
            {
                if (MuayeneSevk == null)
                    MuayeneSevk = (MuayeneSevk)CommandNew();
                else
                    MuayeneSevk = (MuayeneSevk)formEntity;
                return MuayeneSevk;
            }
            set
            {
                MuayeneSevk = value;
            }
        }

        public frmSevk()
        {
            InitializeComponent();
            this.formState = mymodel.myenum.EditMode.emYeni;
            this.Load += new EventHandler(frmSevk_Load);
            Initialize();
        }

        public frmSevk(long Id, mymodel.myenum.EditMode formstate)
        {
            InitializeComponent();
            this.formState = formstate;

            this.Load += new EventHandler(frmSevk_Load);

            InitializeForm(Id, formstate);
            Initialize();
        }

        void frmSevk_Load(object sender, EventArgs e)
        {
           
        }

        private void Initialize()
        {
            edttarih.DateTime = Current.AktifMuayene.MuayeneTarihi;
            DataTable kurumlar;
            kurumlar = Transaction.Instance.ExecuteSql("select * from SevkKurum where aktif=1 and sehir ='" +
                Current.AktifDoktor.LokasyonSehir.Adi + "'");

            lbkurum.DataSource = kurumlar;
            lbkurum.DisplayMember = "Adi";
            lbkurum.ValueMember = "Id";

            DataTable bolumler;
            bolumler = Transaction.Instance.ExecuteSql("select * from SevkBolum where aktif=1");
            lbbolum.DataSource = bolumler;
            lbbolum.DisplayMember = "Adi";
            lbbolum.ValueMember = "Id";
        }

        protected override Entity CommandNew()
        {
            MuayeneSevk muayenesevk = new MuayeneSevk();
            muayenesevk.Hasta.Id = Current.AktifHastaId;
            muayenesevk.Hasta = Current.AktifHasta;
            muayenesevk.Doktor.Id = Current.AktifHasta.Doktor.Id;

            if (Current.AktifDoktorId != muayenesevk.Doktor.Id)
            {
                muayenesevk.VekilDoktor.Id = Current.AktifDoktorId;
                muayenesevk.VekilDoktor = Current.AktifDoktor;
            }

            if (Current.AktifMuayeneId > 0)
            {
                muayenesevk.Muayene.Id = Current.AktifMuayeneId;
                muayenesevk.Muayene = Current.AktifMuayene;
            }

            if (Current.AktifRandevuId > 0)
            {
                muayenesevk.Randevu.Id = Current.AktifRandevuId;
                muayenesevk.Randevu = Current.AktifRandevu;
            }
            formEntity = muayenesevk;
            return muayenesevk;
        }

        protected override void CommandRead(long objId)
        {
            formEntity = SharpBullet.OAL.Persistence.Read<MuayeneSevk>(objId);

            MuayeneSevkEntity = (MuayeneSevk)formEntity;

            if (MuayeneSevkEntity.Muayene.Id > 0)
                MuayeneSevkEntity.Muayene = Persistence.Read<Muayene>(MuayeneSevkEntity.Muayene.Id);

            if (MuayeneSevkEntity.Hasta.Id > 0)
                MuayeneSevkEntity.Hasta = Persistence.Read<Hasta>(MuayeneSevkEntity.Hasta.Id);
        }

        public override void updatedata()
        {
            if ((long)lbkurum.SelectedValue > 0)
            {
                long kurumId = (long)lbkurum.SelectedValue;
                SevkKurum sevkkurum = Persistence.Read<SevkKurum>(kurumId);
                MuayeneSevkEntity.SevkKurum = sevkkurum;
                MuayeneSevkEntity.SevkKurum.Id = kurumId;
            }

            if ((long)lbbolum.SelectedValue > 0)
            {
                long bolumId = (long)lbbolum.SelectedValue;
                SevkBolum bolum = Persistence.Read<SevkBolum>(bolumId);
                MuayeneSevkEntity.SevkBolum = bolum;
                MuayeneSevkEntity.SevkBolum.Id = MuayeneSevkEntity.SevkBolum.Id;
            }
           
        }

        public override void showdata()
        {
            if (MuayeneSevkEntity.SevkKurum.Id > 0)
            {
                lbkurum.SelectedValue = MuayeneSevkEntity.SevkKurum.Id;
                lbbolum.SelectedValue = MuayeneSevkEntity.SevkBolum.Id;
                edttarih.DateTime = MuayeneSevkEntity.SevkTarihi;
            }
            
        }

        public override void formtamam()
        {
            base.formtamam();
            if (Current.AktifMuayeneId > 0)
                Muayene.UpdateMuayenedurumu(Current.AktifMuayeneId, myenum.MuayeneDurumu.SevkEdildi);
        }
    }
}