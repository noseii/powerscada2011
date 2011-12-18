using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using mymodel;
using SharpBullet;
using SharpBullet.OAL;

namespace AHBS2010
{
    public partial class frmMuayene : frmDialogBase
    {

        private Muayene muayene;

        public Muayene MuayeneEntity
        {
            get
            {
                if (muayene == null)
                    muayene = (Muayene)CommandNew();
                else
                    return (Muayene)formEntity;

                return muayene;
            }
            set
            {
                muayene = value;
            }
        }


        public frmMuayene()
        {
            InitializeComponent();
            this.formState = mymodel.myenum.EditMode.emYeni;
            InitDataControl();
            //this.Load += new EventHandler(frmHasta_Load);

            InitializeForm();

        }

        public frmMuayene(long Id, mymodel.myenum.EditMode formstate)
        {
            InitializeComponent();
            this.formState = formstate;
            InitDataControl();
            //this.Load += new EventHandler(frmHasta_Load);

            InitializeForm(Id, formstate);
        }

        protected override Entity CommandNew()
        {
            Muayene entity = new Muayene();
            entity.ProtokolNo = Utility.GetProtokolNo();
            entity.SiraNo = Utility.GetSiraNo();
            entity.MuayeneTarihi = DateTime.Today;
            if (Current.AktifHastaId > 0)
            {
                entity.Hasta.Id = Current.AktifHastaId;
                entity.Hasta = Current.AktifHasta;
                entity.Doktor.Id = Current.AktifHasta.Doktor.Id;
                entity.Doktor = Current.AktifHasta.Doktor;
                if (Current.AktifDoktorId != entity.Doktor.Id)
                {
                    entity.VekilDoktor.Id = Current.AktifDoktorId;
                    entity.VekilDoktor = Current.AktifDoktor;
                }
            }
            else
            {
                throw new Exception("Hasta seçmeden muayene başalatamassınız.");
            }

            entity.MuayeneKapalimi = false;
            entity.MuayeneDurumu = myenum.MuayeneDurumu.Bekliyor;
            if (Current.AktifRandevuId > 0)
            {
                entity.Randevu.Id = Current.AktifRandevuId;
                entity.Randevu = Current.AktifRandevu;
            }

            return entity;
        }

        protected override void CommandRead(long objId)
        {

            Muayene mentity = SharpBullet.OAL.Persistence.Read<Muayene>(objId);
            if (mentity.Doktor.Id > 0)
                mentity.Doktor = Persistence.Read<Doktor>(mentity.Doktor.Id);

            if (mentity.Hasta.Id > 0)
                mentity.Hasta = Persistence.Read<Hasta>(mentity.Hasta.Id);


            formEntity = mentity;
        }

        public override void updatedata()
        {
            MuayeneEntity.MuayeneTarihi = DateEditMuayeneTarihi.DateTime;
            MuayeneEntity.MuayeneDurumu = (myenum.MuayeneDurumu)ucEnumGoster2.Deger;
            MuayeneEntity.ProtokolNo = textEditProtokolno.Text;
            MuayeneEntity.SiraNo = Convert.ToInt16(textEditSiraNo.Text);
            MuayeneEntity.MuayeneKapalimi = checkBox1.Checked;
            if (MuayeneEntity.MuayeneKapalimi)
                MuayeneEntity.MuayeneKapamaTarihi = System.DateTime.Now;
            else
                MuayeneEntity.MuayeneKapamaTarihi = DateTime.MinValue;

        }

        public override void showdata()
        {
            if (MuayeneEntity.VekilDoktor != null && MuayeneEntity.VekilDoktor.Id > 0)
            {
                textEdit4.Text = MuayeneEntity.VekilDoktor.ToString();
            }
            else
                textEdit4.Text = MuayeneEntity.Doktor.ToString();
            textEdit1.Text = MuayeneEntity.Hasta.ToString();
            textEdit3.Text = (DateTime.Now.Subtract(MuayeneEntity.Hasta.DogumTarihi).Days / 365).ToString();
            DateEditMuayeneTarihi.DateTime = MuayeneEntity.MuayeneTarihi;
            checkBox1.Checked = MuayeneEntity.MuayeneKapalimi;
            ucEnumGoster2.Deger = (myenum.MuayeneDurumu)MuayeneEntity.MuayeneDurumu;
            textEditSiraNo.Text = MuayeneEntity.SiraNo.ToString();
            textEditProtokolno.Text = MuayeneEntity.ProtokolNo;

        }

        public override void formtamam()
        {
            MuayeneAc();

        }

        private void MuayeneAc()
        {
            Transaction.Instance.Join(
                     delegate()
                     {
                         if (Current.AktifRandevu == null || Current.AktifRandevuId == 0)
                         {
                             Takvim randevu = Utility.RandevuOlustur(Current.AktifHasta, Convert.ToDateTime(MuayeneEntity.MuayeneTarihi.ToShortDateString()), null, myenum.IslemTuru.Muayene, 0, "Muayene");
                             if (randevu.Id > 0)
                             {
                                 foreach (TakvimSatiri satir in randevu.TakvimSatirlari)
                                 {
                                     int kayitvarmi = Transaction.Instance.ExecuteScalarI("Select count(Id) from TakvimSatiri where Takvim_Id=@prm0 and Aktif=1 and IzlemTuru=@prm1 and IslemTuru=@prm2 ", new object[] { satir.Takvim.Id, satir.Izlemturu.ToString(), satir.IslemTuru.ToString() });
                                     if (kayitvarmi == 0)
                                     {
                                         satir.Takvim.Id = randevu.Id;
                                         if (satir.Id == 0)
                                             satir.Insert();
                                     }
                                 }
                             }
                             else
                             {
                                 randevu.Insert();
                                 foreach (TakvimSatiri satir in randevu.TakvimSatirlari)
                                 {
                                     satir.Takvim.Id = randevu.Id;
                                     satir.Insert();
                                 }
                             }
                             Current.AktifRandevu = randevu;
                             Current.AktifRandevu.Id = randevu.Id;

                         }
                         MuayeneEntity.Randevu.Id = Current.AktifRandevuId;
                         base.formtamam();
                         if (Current.AktifRandevuId > 0)
                         {
                             if (Convert.ToDateTime(MuayeneEntity.EklemeTarihi.ToShortDateString()) < Current.AktifRandevu.BasTarih)
                                 throw new Exception("Muayene tarihi ile randevu tarihi farklı olamaz.");

                             Takvim.UpdateTakvimDurumu(Current.AktifRandevuId, myenum.RandevuDurumu.Geldi);
                         }

                     });
        }
    }
}