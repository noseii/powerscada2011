  
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
    public partial class frmLohusaIzlem : frmDialogBase
    {

        private LohusaIzleme lohusaizleme;

        public LohusaIzleme LohusaIzlemeEntity
        {
            get
            {
                if (lohusaizleme == null)
                    lohusaizleme = (LohusaIzleme)CommandNew();
                else
                    lohusaizleme = (LohusaIzleme)formEntity;
                return lohusaizleme;
            }
            set
            {
                lohusaizleme = value;
            }
        }

        Takvim BirSonrakiRandevu = null;

        public frmLohusaIzlem()
        {
            InitializeComponent();
            this.formState = mymodel.myenum.EditMode.emYeni;

            this.Load += new EventHandler(frmLohusaIzlem_Load);
           
            InitializeForm();
           
        }

        public frmLohusaIzlem(long Id, mymodel.myenum.EditMode formstate)
        {
            InitializeComponent();
            this.formState = formstate;

            this.Load += new EventHandler(frmLohusaIzlem_Load);
            
            InitializeForm(Id, formstate);
            
        }

        void frmLohusaIzlem_Load(object sender, EventArgs e)
        {
            edtnabiz.Properties.MinValue = 0;
            edtnabiz.Properties.MaxValue = 10000;

            edtbuyuktansiyon.Properties.MinValue = 0;
            edtbuyuktansiyon.Properties.MaxValue = 10000;

            edtkucuktansiyon.Properties.MinValue = 0;
            edtkucuktansiyon.Properties.MaxValue = 10000;

            edtates.Properties.MinValue = 0;
            edtates.Properties.MaxValue = 10000;
        

            
            if (LohusaIzlemeEntity != null && LohusaIzlemeEntity.Hasta.Id > 0)
            {
                LohusaIzlemiGoster();
            }
            if (Current.AktifMuayeneId > 0)
            {
                DateEditIzlemTarihi.DateTime = Current.AktifMuayene.MuayeneTarihi;
                DateEditIzlemTarihi.Enabled = false;
            }
            edtgebelikNo.Enabled = false;
        }

        private void LohusaIzlemiGoster()
        {
            Takvim[] Randevular = LohusaIzlemleri();
            BilesenGoruntusuAyarla(Randevular);
        }

        private void BilesenGoruntusuAyarla(Takvim[] Randevular)
        {
            if (Randevular != null && Randevular.Length > 0)
            {
                BirSonrakiRandevu = Randevular[0];
                dateEditbirsonrakiIzlemTarihi.Visible = true;
                dateEditbirsonrakiIzlemTarihi.Enabled = false;
                labelIzlemSaati.Visible = true;
                labelIzlemSaatilabel.Visible = true;
                labelIzlemSaati.Text = BirSonrakiRandevu.Saat.ToString();
                dateEditbirsonrakiIzlemTarihi.DateTime = BirSonrakiRandevu.BasTarih;
                simpleButtonRandevuDuzenle.Visible = true;
            }
        }

        private Takvim[] LohusaIzlemleri()
        {
            Takvim[] Randevular = Persistence.ReadList<Takvim>(@"		
                Select 
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
	                ,Takvim.RowVersion,Takvim.EkleyenKullanici,Takvim.DegistirenKullanici	,Takvim.IsAutoImport  Order By Takvim.BasTarih ", new object[] { LohusaIzlemeEntity.Hasta.Id, myenum.IslemTuru.Izlem.ToString(), myenum.IzlemTuru.Lohusa_Izlemi.ToString(), System.DateTime.Today, myenum.RandevuDurumu.Verildi.ToString() });
            return Randevular;
        }

        protected override Entity CommandNew()
        {
            GebeBaslangic[] baslangic = Persistence.ReadList<GebeBaslangic>(@"Select * from GebeBaslangic where Hasta_Id=@prm0 and Gebelikdurumu=@prm1 and Aktif=1 order by GebelikNo desc",
                new object[]{Current.AktifHastaId,myenum.GebelikDurumu.Bitti.ToString() });
            if (baslangic == null || baslangic.Length==0)
            {
                throw new Exception("Bitmiş gebeliği olmayan hastaya losusa izlemi girilemez");
            }

            dateEditGebelikSonucTarihi.DateTime = baslangic[0].GebelikSonuclanmaTarihi;
            edtgebelikNo.Value = baslangic[0].GebelikNo;

            LohusaIzleme lohusaizlem = new LohusaIzleme();
            lohusaizlem.GebelikNo = baslangic[0].GebelikNo;
            lohusaizlem.Hasta.Id = Current.AktifHastaId;
            lohusaizlem.Hasta = Current.AktifHasta;

            lohusaizlem.Doktor.Id = Current.AktifHasta.Doktor.Id;
            if (Current.AktifDoktorId != lohusaizlem.Doktor.Id)
            {
                lohusaizlem.VekilDoktor.Id = Current.AktifDoktorId;
                lohusaizlem.VekilDoktor = Current.AktifDoktor;
            }

            if (Current.AktifMuayeneId > 0)
            {
                lohusaizlem.Muayene.Id = Current.AktifMuayeneId;
                lohusaizlem.Muayene = Current.AktifMuayene;
            }
           
            if (Current.AktifRandevuId > 0)
            {
                {
                    lohusaizlem.Randevu.Id = Current.AktifRandevuId;
                    lohusaizlem.Randevu = Current.AktifRandevu;
                }
            }
            lohusaizlem.GebelikSonucuTarihi = baslangic[0].GebelikSonuclanmaTarihi;
            return lohusaizlem;
        }

        protected override void CommandRead(long objId)
        {
            formEntity = SharpBullet.OAL.Persistence.Read<LohusaIzleme>(objId);
            LohusaIzlemeEntity = (LohusaIzleme)formEntity;
            if (LohusaIzlemeEntity.Hasta.Id > 0)
                LohusaIzlemeEntity.Hasta = Persistence.Read<Hasta>(LohusaIzlemeEntity.Hasta.Id);

            if (LohusaIzlemeEntity.Muayene.Id > 0)
                LohusaIzlemeEntity.Muayene = Persistence.Read<Muayene>(LohusaIzlemeEntity.Muayene.Id);

            if (LohusaIzlemeEntity.LohusaIzlemTanisi.Id > 0)
                LohusaIzlemeEntity.LohusaIzlemTanisi = Persistence.Read<Teshis>(LohusaIzlemeEntity.LohusaIzlemTanisi.Id);
             
        }

        public override void updatedata()
        {
            //base.updatedata();
            //formEntity = new LohusaIzleme();
            LohusaIzlemeEntity.Muayene =Current.AktifMuayene;
            LohusaIzlemeEntity.Muayene.Id =Current.AktifMuayeneId;
            LohusaIzlemeEntity.KucukTansiyon = (byte)edtkucuktansiyon.Value;
            LohusaIzlemeEntity.BuyukTansiyon = (byte)edtbuyuktansiyon.Value;
            LohusaIzlemeEntity.Nabiz = (byte)edtnabiz.Value;
            LohusaIzlemeEntity.Ates = (byte)edtates.Value;
            LohusaIzlemeEntity.AciklamaOgut = edtaciklamaogut.Text;
            LohusaIzlemeEntity.BeslenmeDanismanligiAldimi = edtbeslenmedanismanligialdimi.Checked;
            LohusaIzlemeEntity.EmzirmeDanismanligiAldimi = edtemzirmedan.Checked;
            LohusaIzlemeEntity.DemirDestegiAldimi = edtdemirdestegialdi.Checked;
            LohusaIzlemeEntity.IzlemTarihi = DateEditIzlemTarihi.DateTime;
            LohusaIzlemeEntity.LohusaIzlemTanisi.Id = editButtonIzlem.Id;
        }

        public override void showdata()
        {
             LohusaIzlemeEntity = (LohusaIzleme)formEntity;
          
            edtkucuktansiyon.Value = ((LohusaIzleme)formEntity).KucukTansiyon;
            edtbuyuktansiyon.Value = ((LohusaIzleme)formEntity).BuyukTansiyon;
            edtnabiz.Value = ((LohusaIzleme)formEntity).Nabiz;
            edtates.Value = ((LohusaIzleme)formEntity).Ates;
            edtaciklamaogut.Text = ((LohusaIzleme)formEntity).AciklamaOgut;
            edtbeslenmedanismanligialdimi.Checked = ((LohusaIzleme)formEntity).BeslenmeDanismanligiAldimi;
            edtemzirmedan.Checked = ((LohusaIzleme)formEntity).EmzirmeDanismanligiAldimi;
            edtdemirdestegialdi.Checked = ((LohusaIzleme)formEntity).DemirDestegiAldimi;
            DateEditIzlemTarihi.DateTime=LohusaIzlemeEntity.IzlemTarihi;
            editButtonIzlem.Text = LohusaIzlemeEntity.LohusaIzlemTanisi.Adi;
        
        }

        public override void formtamam()
        {

            Transaction.Instance.Join(
                      delegate()
                      {
                          //Current.TakvimOlustur(myenum.IzlemTuru.Lohusa_Izlemi, LohusaIzlemeEntity.EklemeTarihi, LohusaIzlemeEntity.Hasta,System.DateTime.MinValue);

                          base.formtamam();
                          //if (Current.AktifMuayeneId > 0)
                          //    Muayene.UpdateMuayenedurumu(Current.AktifMuayeneId, myenum.MuayeneDurumu.MuayeneEdildi);

                         
                              if (Current.AktifRandevuId > 0)
                              {
                                  if (Convert.ToDateTime(LohusaIzlemeEntity.EklemeTarihi.ToShortDateString())<Current.AktifRandevu.BasTarih)
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
                LohusaIzlemiGoster();
            }
        }
    }
}






