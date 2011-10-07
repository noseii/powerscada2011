


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mymodel;
using SharpBullet.OAL;
using AHBS2010;

namespace AHBS2010
{
    public partial class frmGebeIzlem : frmDialogBase
    {

        private GebeIzleme gebeizleme;

        public GebeIzleme GebeIzlemeEntity
        {
            get
            {
                if (gebeizleme == null)
                    gebeizleme = (GebeIzleme)CommandNew();
                else
                    gebeizleme = (GebeIzleme)formEntity;
                return gebeizleme;
            }
            set
            {
                gebeizleme = value;
            }
        }

        Takvim BirSonrakiRandevu = null;
        static GebeBaslangic baslangic = null;

        public static Sonuc GebeIzlemKontrol()
        {
            Condition[] con = new Condition[3];
            con[0].Field = "Gebelikdurumu";
            con[0].Operator = Operator.Equal;
            con[0].Value = myenum.GebelikDurumu.Basladi.ToString();

            con[1].Field = "Hasta_Id";
            con[1].Operator = Operator.Equal;
            con[1].Value = Current.AktifHastaId;

            con[2].Field = "Aktif";
            con[2].Operator = Operator.Equal;
            con[2].Value = 1;


            baslangic = Persistence.Read<GebeBaslangic>(con);
            if (baslangic == null)
            {
                return new Sonuc(true, "Gebe başlangıcı olmayan hastaya gebe sonucu girilemez");
            }
            else
                return new Sonuc();
        }

        public frmGebeIzlem()
        {
            InitializeComponent();
            this.formState = mymodel.myenum.EditMode.emYeni;
            this.Load += new EventHandler(frmGebeBaslangic_Load);

            InitializeForm();

        }

        public frmGebeIzlem(long Id, mymodel.myenum.EditMode formstate)
        {
            InitializeComponent();
            this.formState = formstate;

            this.Load += new EventHandler(frmGebeBaslangic_Load);

            InitializeForm(Id, formstate);

        }

        void frmGebeBaslangic_Load(object sender, EventArgs e)
        {


            spinEdit1.Properties.MinValue = 0;
            spinEdit1.Properties.MaxValue = 100000;

            spinEdit2.Properties.MinValue = 0;
            spinEdit2.Properties.MaxValue = 100000;

            spinEdit3.Properties.MinValue = 0;
            spinEdit3.Properties.MaxValue = 100000;

            spinEdit4.Properties.MinValue = 0;
            spinEdit4.Properties.MaxValue = 100000;

            spinEdit5.Properties.MinValue = 0;
            spinEdit5.Properties.MaxValue = 100000;

            spinEdit6.Properties.MinValue = 0;
            spinEdit6.Properties.MaxValue = 100000;

            spinEdit7.Properties.MinValue = 0;
            spinEdit7.Properties.MaxValue = 100000;

            spinEdit8.Properties.MinValue = 0;
            spinEdit8.Properties.MaxValue = 100000;
            spinEditCocukKalpSesiAdedi.Properties.MinValue = 0;
            spinEditCocukKalpSesiAdedi.Properties.MaxValue = 100000;

            if (GebeIzlemeEntity != null && GebeIzlemeEntity.Hasta.Id > 0)
            {
                GebeIzlemiGoster();
            }
            if (Current.AktifMuayeneId > 0)
            {
                DateEditIzlemTarihi.DateTime = Current.AktifMuayene.MuayeneTarihi;
                DateEditIzlemTarihi.Enabled = false;
            }

            spinEdit8.Enabled = false;
        }

        private void GebeIzlemiGoster()
        {
            Takvim[] Randevular = GebeIzlemleri();
            BilesenlerinGorunumunuAyarla(Randevular);
        }

        private void BilesenlerinGorunumunuAyarla(Takvim[] Randevular)
        {
            if (Randevular != null && Randevular.Length > 0)
            {
                BirSonrakiRandevu = Randevular[0];
                labelControl25.Visible = true;
                dateEditbirsonrakiIzlemTarihi.Visible = true;
                dateEditbirsonrakiIzlemTarihi.Enabled = false;
                labelIzlemSaati.Visible = true;
                labelIzlemSaatilabel.Visible = true;
                simpleButtonRandevuDuzenle.Visible = true;
                labelIzlemSaati.Text = BirSonrakiRandevu.Saat.ToString();
                dateEditbirsonrakiIzlemTarihi.DateTime = BirSonrakiRandevu.BasTarih;

            }
        }

        private Takvim[] GebeIzlemleri()
        {
            Takvim[] Randevular = Persistence.ReadList<Takvim>(@"Select 
                   	Takvim.Id,Takvim.SiraNo,Takvim.BasTarih,Takvim.Doktor_Id,Takvim.Hasta_Id,Takvim.RandevuDurumu,Takvim.Aciklama,Takvim.Konu
	                ,Takvim.Saat,Takvim.Aktif,Takvim.EklemeTarihi,Takvim.DegistirmeTarihi,Takvim.EkleyenMakAdres,Takvim.DegistirenMakAdres,Takvim.Id
	                ,Takvim.RowVersion,Takvim.EkleyenKullanici,Takvim.DegistirenKullanici	,Takvim.IsAutoImport  
                From Takvim 
                inner join TakvimSatiri as Ts on Ts.Takvim_Id=Takvim.Id 
                Where Takvim.Hasta_Id=@prm0 
                and Takvim.Aktif=1 
                and Ts.IzlemTuru=@prm2 
                and Takvim.BasTarih>@prm3 
                and Takvim.RandevuDurumu=@prm4
                and Ts.IslemTuru=@prm1  
		Group By Takvim.Id,Takvim.SiraNo,Takvim.BasTarih,Takvim.Doktor_Id,Takvim.Hasta_Id,Takvim.RandevuDurumu,Takvim.Aciklama,Takvim.Konu
	                ,Takvim.Saat,Takvim.Aktif,Takvim.EklemeTarihi,Takvim.DegistirmeTarihi,Takvim.EkleyenMakAdres,Takvim.DegistirenMakAdres,Takvim.Id
	                ,Takvim.RowVersion,Takvim.EkleyenKullanici,Takvim.DegistirenKullanici,Takvim.IsAutoImport  Order By Takvim.BasTarih ", new object[] { GebeIzlemeEntity.Hasta.Id, myenum.IslemTuru.Izlem.ToString(), myenum.IzlemTuru.Gebe_Izlemi.ToString(), System.DateTime.Today, myenum.RandevuDurumu.Verildi.ToString() });
            return Randevular;
        }




        protected override Entity CommandNew()
        {



            GebeIzleme gbizlem = new GebeIzleme();

            gbizlem.Hasta.Id = Current.AktifHastaId;
            gbizlem.Hasta = Current.AktifHasta;
            gbizlem.Doktor.Id = Current.AktifHasta.Doktor.Id;
            if (Current.AktifDoktorId != gbizlem.Doktor.Id)
            {
                gbizlem.VekilDoktor.Id = Current.AktifDoktorId;
                gbizlem.VekilDoktor = Current.AktifDoktor;
            }
            if (baslangic != null)
            {
                gbizlem.GebeBaslangic.Id = baslangic.Id;

                double ghaftano = ((DateTime.Now.Subtract(baslangic.SonAdetTarihi).TotalDays) / 7.0f);
                double haftano = System.Math.Ceiling(ghaftano);
                gbizlem.GebelikHaftaNo = Convert.ToByte(haftano);
                gbizlem.GebelikNo = baslangic.GebelikNo;

                double izlemhaftano = (DateTime.Now.Subtract(baslangic.GebelikBildirimTarihi).TotalDays / 7.0f);
                izlemhaftano = System.Math.Ceiling(izlemhaftano);
                gbizlem.izlemHaftaNo = Convert.ToByte(izlemhaftano);
            }
            if (Current.AktifMuayeneId > 0)
            {
                gbizlem.Muayene.Id = Current.AktifMuayeneId;
                gbizlem.Muayene = Current.AktifMuayene;
            }

            if (Current.AktifRandevuId > 0)
            {
                gbizlem.Randevu.Id = Current.AktifRandevuId;
                gbizlem.Randevu = Current.AktifRandevu;
            }


            return gbizlem;
        }

        protected override void CommandRead(long objId)
        {
            formEntity = SharpBullet.OAL.Persistence.Read<GebeIzleme>(objId);
            GebeIzlemeEntity = (GebeIzleme)formEntity;
            if (GebeIzlemeEntity.Hasta.Id > 0)
                GebeIzlemeEntity.Hasta = Persistence.Read<Hasta>(GebeIzlemeEntity.Hasta.Id);

            if (GebeIzlemeEntity.Muayene.Id > 0)
                GebeIzlemeEntity.Muayene = Persistence.Read<Muayene>(GebeIzlemeEntity.Muayene.Id);

        }

        public override void updatedata()
        {

            GebeIzlemeEntity.Muayene = Current.AktifMuayene;
            GebeIzlemeEntity.Muayene.Id = Current.AktifMuayeneId;
            GebeIzlemeEntity.Agirligi = Convert.ToByte(spinEdit4.EditValue);
            GebeIzlemeEntity.BuyukTansiyon = Convert.ToByte(spinEdit6.EditValue);
            GebeIzlemeEntity.CocukKalpSesiAdedi = Convert.ToByte(spinEditCocukKalpSesiAdedi.Value);
            GebeIzlemeEntity.GebelikHaftaNo = Convert.ToByte(spinEdit3.EditValue);
            GebeIzlemeEntity.GebelikNo = Convert.ToByte(spinEdit8.EditValue);
            GebeIzlemeEntity.KucukTansiyon = Convert.ToByte(spinEdit5.EditValue);
            GebeIzlemeEntity.Nabiz = Convert.ToByte(spinEdit7.EditValue);
            GebeIzlemeEntity.GelisBicimi = (myenum.CocukGelisBicimi)comboBoxEdit1.Deger;
            GebeIzlemeEntity.Ogutler = memoEdit1.Text;
            GebeIzlemeEntity.Hemoglobin = Convert.ToByte(spinEdit2.EditValue);
            GebeIzlemeEntity.idrardaProteinVarmi = checkEdit3.Checked;
            GebeIzlemeEntity.izlemHaftaNo = Convert.ToByte(spinEdit1.EditValue);
            //GebeIzlemeEntity.izlemTarihi = dateEdit2.DateTime;
            GebeIzlemeEntity.OdemVarmi = checkEdit1.Checked;

            GebeIzlemeEntity.TetanozAsisiYapildi = checkEdit5.Checked;
            GebeIzlemeEntity.VarisVarmi = checkEdit4.Checked;
            GebeIzlemeEntity.IzlemTarihi = DateEditIzlemTarihi.DateTime;
        }

        public override void showdata()
        {
            GebeIzlemeEntity = (GebeIzleme)formEntity;
            spinEdit4.EditValue = GebeIzlemeEntity.Agirligi;
            comboBoxEdit1.Deger = GebeIzlemeEntity.GelisBicimi;
            checkEdit3.Checked = GebeIzlemeEntity.idrardaProteinVarmi;

            spinEdit2.EditValue = GebeIzlemeEntity.Hemoglobin;
            spinEdit8.EditValue = GebeIzlemeEntity.GebelikNo;
            spinEdit3.EditValue = GebeIzlemeEntity.GebelikHaftaNo;
            spinEdit1.EditValue = GebeIzlemeEntity.izlemHaftaNo;

            checkEdit4.Checked = GebeIzlemeEntity.VarisVarmi;
            spinEdit6.EditValue = GebeIzlemeEntity.BuyukTansiyon;
            checkEdit1.Checked = GebeIzlemeEntity.OdemVarmi;
            memoEdit1.Text = GebeIzlemeEntity.Ogutler;
            spinEdit5.EditValue = GebeIzlemeEntity.KucukTansiyon;
            spinEdit7.EditValue = GebeIzlemeEntity.Nabiz;
            //dateEdit2.DateTime = GebeIzlemeEntity.izlemTarihi;
            checkEdit5.Checked = GebeIzlemeEntity.TetanozAsisiYapildi;
            DateEditIzlemTarihi.DateTime = GebeIzlemeEntity.IzlemTarihi;
            spinEditCocukKalpSesiAdedi.Value = GebeIzlemeEntity.CocukKalpSesiAdedi;
        }

        public override void formtamam()
        {
            Transaction.Instance.Join(
                         delegate()
                         {
                             base.formtamam();
                             //if (Current.AktifMuayeneId > 0)
                             //    Muayene.UpdateMuayenedurumu(Current.AktifMuayeneId, myenum.MuayeneDurumu.MuayeneEdildi);

                             if (Current.AktifRandevuId > 0)
                             {
                                 if (Convert.ToDateTime(GebeIzlemeEntity.EklemeTarihi.ToShortDateString()) < Current.AktifRandevu.BasTarih)
                                     throw new Exception("İleri tarihli bir randevu işlem yapılamaz.");
                                 Takvim.UpdateTakvimDurumu(Current.AktifRandevuId, myenum.RandevuDurumu.Geldi);
                             }

                         }
             );
        }

        private void simpleButtonRandevuDuzenle_Click(object sender, EventArgs e)
        {
            if (BirSonrakiRandevu != null)
            {
                frmRandevu frm = new frmRandevu(BirSonrakiRandevu);
                frm.ShowDialog();
                GebeIzlemiGoster();
            }
        }
    }
}




