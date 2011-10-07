

    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mymodel;
using SharpBullet.OAL;

namespace AHBS2010
{
    public partial class frmGebeBaslangic : AHBS2010.frmDialogBase
    {

        private GebeBaslangic gebebaslangic;

        public GebeBaslangic GebeBaslangicEntity
        {
            get
            {
                if (gebebaslangic == null)
                    gebebaslangic = (GebeBaslangic)CommandNew();
                else
                    gebebaslangic = (GebeBaslangic)formEntity;
                return gebebaslangic;
            }
            set
            {
                gebebaslangic = value;
            }
        }


        public frmGebeBaslangic()
        {
            InitializeComponent();
            this.formState = mymodel.myenum.EditMode.emYeni;

            this.Load += new EventHandler(frmGebeBaslangic_Load);
           
            InitializeForm();
           
        }

        public frmGebeBaslangic(long Id, mymodel.myenum.EditMode formstate)
        {
            InitializeComponent();
            this.formState = formstate;

            this.Load += new EventHandler(frmGebeBaslangic_Load);
            
            InitializeForm(Id, formstate);
            
        }

        void frmGebeBaslangic_Load(object sender, EventArgs e)
        {
            edtgebelikNo.Properties.MinValue = 0;
            edtgebelikNo.Properties.MaxValue = 50;

            edtnabiz.Properties.MinValue = 0;
            edtnabiz.Properties.MaxValue = 10000;

            edtbuyuktansiyon.Properties.MinValue = 0;
            edtbuyuktansiyon.Properties.MaxValue = 10000;

            edtkucuktansiyon.Properties.MinValue = 0;
            edtkucuktansiyon.Properties.MaxValue = 10000;

            if (Current.AktifMuayeneId > 0)
            {
                DateEditIzlemTarihi.DateTime = Current.AktifMuayene.MuayeneTarihi;
                DateEditIzlemTarihi.Enabled = false;
            }
        }

        protected override Entity CommandNew()
        {

              

            GebeBaslangic gb = new GebeBaslangic();
          
            gb.Hasta.Id = Current.AktifHastaId;
            gb.Hasta = Current.AktifHasta;
            gb.Doktor.Id = Current.AktifHasta.Doktor.Id;
            if (Current.AktifDoktorId != gb.Doktor.Id)
            {
                gb.VekilDoktor.Id = Current.AktifDoktorId;
                gb.VekilDoktor = Current.AktifDoktor;
            }
            gb.GebelikDurumu = myenum.GebelikDurumu.Basladi;
            if(Current.AktifMuayeneId>0)
            {
                gb.Muayene.Id = Current.AktifMuayeneId;
                gb.Muayene = Current.AktifMuayene;
            }
           
            if (Current.AktifRandevuId > 0)
            {
                gb.Randevu.Id = Current.AktifRandevuId;
                gb.Randevu = Current.AktifRandevu;
            }
            

            return gb;
        }

        protected override void CommandRead(long objId)
        {
            formEntity = SharpBullet.OAL.Persistence.Read<GebeBaslangic>(objId);
            GebeBaslangicEntity = (GebeBaslangic)formEntity;
            if (GebeBaslangicEntity.Hasta.Id > 0)
                GebeBaslangicEntity.Hasta = Persistence.Read<Hasta>(GebeBaslangicEntity.Hasta.Id);

            if (GebeBaslangicEntity.Muayene.Id > 0)
                GebeBaslangicEntity.Muayene = Persistence.Read<Muayene>(GebeBaslangicEntity.Muayene.Id);


        }

        public override void updatedata()
        {

            GebeBaslangicEntity.Muayene = Current.AktifMuayene;
            GebeBaslangicEntity.Muayene.Id =Current.AktifMuayeneId;
            GebeBaslangicEntity.AkrabaEvliligiVarmi = edtakrabaevliligivar.Checked;
            GebeBaslangicEntity.BeklenenDogumTarihi = edtbeklenendogumtarihi.DateTime;
            GebeBaslangicEntity.BeslenmeDanismanligiAldimi = edtbeslenmedanismanligialdimi.Checked;
            GebeBaslangicEntity.BuyukTansiyon = (byte)edtbuyuktansiyon.Value;
            GebeBaslangicEntity.DemirDestegiAldimi = edtdemirdestegialdi.Checked;
            GebeBaslangicEntity.DogumlaIlgiliKarar = (myenum.DogumlaIlgiliKarar)edtdogumlailgilikarar.Deger;
            GebeBaslangicEntity.EsininAkrabalikDerecesi = (myenum.AkrabalikDerece)ucAkrabalikDerecesi1.Deger;
            GebeBaslangicEntity.EsininKanGrubu = (myenum.KanGrubu)ucKanGrubu1.Deger;
            GebeBaslangicEntity.GebelikBildirimAciklamasi = edtgebelikbildirimaciklama.Text;
           
            GebeBaslangicEntity.GenelNot = edtgenelnot.Text;
            GebeBaslangicEntity.KucukTansiyon = (byte)edtkucuktansiyon.Value;
            GebeBaslangicEntity.Nabiz = (byte)edtnabiz.Value;
            GebeBaslangicEntity.PelvisDurumu = (myenum.PelvisDurumu)edtpelvisdurumu.Deger;
            GebeBaslangicEntity.SonAdetTarihi = edtsonadettarihi.DateTime;
            GebeBaslangicEntity.TetanozBagisikligiVarmi = edttetanozbagisikligivarmi.Checked;
            GebeBaslangicEntity.GebelikNo = Convert.ToByte(edtgebelikNo.EditValue);
            GebeBaslangicEntity.GebelikBildirimTarihi = dateEditGebelikBildirimTarihi.DateTime;
            GebeBaslangicEntity.IzlemTarihi = DateEditIzlemTarihi.DateTime;
            GebeBaslangicEntity.FizikselMuayeneNotlari= memoEditFizikselMuayeneNotlari.Text;
          
        }

        public override void showdata()
        {
            GebeBaslangicEntity = (GebeBaslangic)formEntity;

            edtakrabaevliligivar.Checked = GebeBaslangicEntity.AkrabaEvliligiVarmi;

            if (GebeBaslangicEntity.BeklenenDogumTarihi!=System.DateTime.MinValue)
                edtbeklenendogumtarihi.DateTime = GebeBaslangicEntity.BeklenenDogumTarihi;
 
            edtbeslenmedanismanligialdimi.Checked = GebeBaslangicEntity.BeslenmeDanismanligiAldimi;

            edtbuyuktansiyon.EditValue = GebeBaslangicEntity.BuyukTansiyon;

            edtdemirdestegialdi.Checked = GebeBaslangicEntity.DemirDestegiAldimi;

            edtdogumlailgilikarar.Deger = GebeBaslangicEntity.DogumlaIlgiliKarar;

            ucAkrabalikDerecesi1.Deger = GebeBaslangicEntity.EsininAkrabalikDerecesi;

            ucKanGrubu1.Deger = GebeBaslangicEntity.EsininKanGrubu;

            edtgebelikbildirimaciklama.Text = GebeBaslangicEntity.GebelikBildirimAciklamasi;

           

            edtgenelnot.Text = GebeBaslangicEntity.GenelNot;

            edtkucuktansiyon.EditValue = GebeBaslangicEntity.KucukTansiyon;

            edtnabiz.EditValue = GebeBaslangicEntity.Nabiz;

            edtpelvisdurumu.Deger = GebeBaslangicEntity.PelvisDurumu;

            edtsonadettarihi.DateTime = GebeBaslangicEntity.SonAdetTarihi;

            edttetanozbagisikligivarmi.Checked = GebeBaslangicEntity.TetanozBagisikligiVarmi;

            edtgebelikNo.EditValue=GebeBaslangicEntity.GebelikNo;
            dateEditGebelikBildirimTarihi.DateTime = GebeBaslangicEntity.GebelikBildirimTarihi;
            DateEditIzlemTarihi.DateTime=GebeBaslangicEntity.IzlemTarihi;
            memoEditFizikselMuayeneNotlari.Text = GebeBaslangicEntity.FizikselMuayeneNotlari;
        }

        private void edtsonadettarihi_EditValueChanged(object sender, EventArgs e)
        {
            //TODO:Son adet tarihine ben 280 gün ekledim kaç olacak soralım
            edtbeklenendogumtarihi.EditValue= edtsonadettarihi.DateTime.AddDays(280);
        }

        public override void formtamam()
        {
            Transaction.Instance.Join(
                        delegate()
                        {
                            object tarih = Transaction.Instance.ExecuteScalar("Select max(Tarih) from GebeSonuc where Hasta_Id=@prm0 and Aktif=1 and GebelikNo<@prm1", new object[] { Current.AktifHastaId, GebeBaslangicEntity.GebelikNo });
                            if (tarih != null && tarih!=System.DBNull.Value)
                                GebeBaslangicEntity.OncekiGebelikSonlanmaTarihi = (DateTime)tarih;
                            base.formtamam();
                            //if (Current.AktifMuayeneId > 0)
                            //    Muayene.UpdateMuayenedurumu(Current.AktifMuayeneId, myenum.MuayeneDurumu.MuayeneEdildi);
                           
                                if (Current.AktifRandevuId > 0)
                                {
                                    if (Convert.ToDateTime(GebeBaslangicEntity.EklemeTarihi.ToShortDateString())<Current.AktifRandevu.BasTarih)
                                        throw new Exception("İleri tarihli bir randevu işlem yapılamaz.");
                                    Takvim.UpdateTakvimDurumu(Current.AktifRandevuId, myenum.RandevuDurumu.Geldi);
                                }
                              int kayitlitakvimvarmi = Transaction.Instance.ExecuteScalarI(@"
                                    Select 
                                         count(Takvim.Id) 
                                    From  Takvim
                                    inner join TakvimSatiri as Ts on Ts.Takvim_Id=Takvim.Id  
                                    where takvim.Hasta_Id=@prm0 
                                    and   Takvim.RandevuDurumu=@prm4 
                                    and   Takvim.Aktif=1 
                                    and   Ts.IslemTuru=@prm1 
                                    and   Ts.IzlemTuru=@prm2 
                                    and   Takvim.BasTarih>@prm3", new object[] { Current.AktifHastaId, myenum.IslemTuru.Izlem.ToString(), myenum.IzlemTuru.Gebe_Izlemi.ToString(),System.DateTime.Today, myenum.RandevuDurumu.Verildi.ToString() });
                              if (kayitlitakvimvarmi == 0)
                              {
                                  Current.TakvimOlustur(myenum.IzlemTuru.Gebe_Izlemi, GebeBaslangicEntity.Hasta, GebeBaslangicEntity.SonAdetTarihi);
                              }
                            
                        }
           );
        }



    }
}


