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
    public partial class frmGebeSonuc : frmDialogBase
    {

        private GebeSonuc gebesonuc;

        public GebeSonuc GebeSonucEntity
        {
            get
            {
                if (gebesonuc == null)
                    gebesonuc = (GebeSonuc)CommandNew();
                else
                    gebesonuc = (GebeSonuc)formEntity;
                return gebesonuc;
            }
            set
            {
                gebesonuc = value;
            }
        }


        public frmGebeSonuc()
        {
            InitializeComponent();
            this.formState = mymodel.myenum.EditMode.emYeni;

            this.Load += new EventHandler(frmGebeSonuc_Load);
           
            InitializeForm();
           
        }

        public frmGebeSonuc(long Id, mymodel.myenum.EditMode formstate)
        {
            InitializeComponent();
            this.formState = formstate;

            this.Load += new EventHandler(frmGebeSonuc_Load);
            
            InitializeForm(Id, formstate);
            
        }

        void frmGebeSonuc_Load(object sender, EventArgs e)
        {
            edtgebelikNo.Properties.MinValue = 0;
            edtgebelikNo.Properties.MaxValue = 50;

            edtGebelikHaftaNo.Properties.MinValue = 0;
            edtGebelikHaftaNo.Properties.MaxValue = 100;


            edtcanlidogumadedi.Properties.MinValue = 0;
            edtcanlidogumadedi.Properties.MaxValue = 100;
            edtoludogumadedi.Properties.MinValue = 0;
            edtoludogumadedi.Properties.MaxValue = 100;
            spinEdit4.Properties.MinValue = 0;
            spinEdit4.Properties.MaxValue = 10000;
          
            if (Current.AktifMuayeneId > 0)
            {
                DateEditIzlemTarihi.DateTime = Current.AktifMuayene.MuayeneTarihi;
                DateEditIzlemTarihi.Enabled = false;
            }
            edtgebelikNo.Enabled = false;
        }




        protected override Entity CommandNew()
        {
            
            Condition[] con=new Condition[3];
            con[0].Field="Gebelikdurumu";
            con[0].Operator=Operator.Equal;
            con[0].Value=myenum.GebelikDurumu.Basladi.ToString();

            con[1].Field="Hasta_Id";
            con[1].Operator=Operator.Equal;
            con[1].Value=Current.AktifHastaId;

            con[2].Field="Aktif";
            con[2].Operator=Operator.Equal;
            con[2].Value=1;


            GebeBaslangic baslangic=Persistence.Read<GebeBaslangic>(con);

            if (baslangic == null)
            {
                int i = Transaction.Instance.ExecuteNonQuery(
                     "delete from gebesonuc Where Id in (select top 1 ID from gebesonuc where hasta_Id=@prm0 and aktif=1 order by eklemetarihi desc)", new object[] { Current.AktifHastaId });

                if (i>0)
                    i=Transaction.Instance.ExecuteNonQuery(
                        "Update gebebaslangic set GebelikDurumu='Basladi' Where Id in (select top 1 ID from gebebaslangic where  hasta_Id=@prm0 and GebelikDurumu='Bitti' order by eklemetarihi desc)", new object[] { Current.AktifHastaId });
                if (i > 0)
                    baslangic = Persistence.Read<GebeBaslangic>(con);

            }

            if (baslangic == null)
                throw new Exception("Gebe başlangıcı olmayan hastaya gebe sonucu girilemez");


            GebeSonuc gsonuc = new GebeSonuc();
            gsonuc.Hasta.Id = Current.AktifHastaId;
            gsonuc.Hasta = Current.AktifHasta;
            gsonuc.Doktor.Id = Current.AktifHasta.Doktor.Id;
            if (Current.AktifDoktorId != gsonuc.Doktor.Id)
            {
                gsonuc.VekilDoktor.Id = Current.AktifDoktorId;
                gsonuc.VekilDoktor = Current.AktifDoktor;
            }
            gsonuc.GebeBaslangic.Id = baslangic.Id;
            gsonuc.GebelikNo = baslangic.GebelikNo;
            gsonuc.GebelikHaftaNo =Convert.ToByte(DateTime.Now.Subtract(baslangic.GebelikBildirimTarihi).Days / 7);
            
            if (Current.AktifMuayeneId > 0)
            {
                gsonuc.Muayene.Id = Current.AktifMuayeneId;
                gsonuc.Muayene = Current.AktifMuayene;
            }
           
            if (Current.AktifRandevuId > 0)
            {
                gsonuc.Randevu.Id = Current.AktifRandevuId;
                gsonuc.Randevu = Current.AktifRandevu;
            }
            gsonuc.Tarih = System.DateTime.Today;

            return gsonuc;
            
            
        }

        protected override void CommandRead(long objId)
        {
            formEntity = SharpBullet.OAL.Persistence.Read<GebeSonuc>(objId);
            GebeSonucEntity =(mymodel.GebeSonuc)formEntity;

            if (GebeSonucEntity.Hasta.Id > 0)
                GebeSonucEntity.Hasta = Persistence.Read<Hasta>(GebeSonucEntity.Hasta.Id);

            if (GebeSonucEntity.Muayene.Id > 0)
                GebeSonucEntity.Muayene = Persistence.Read<Muayene>(GebeSonucEntity.Muayene.Id);
        }

       

        public override void updatedata()
        {
         
            
            GebeSonucEntity.Muayene = Current.AktifMuayene;
            GebeSonucEntity.Muayene.Id = Current.AktifMuayeneId;
            GebeSonucEntity.SonucNotlar = edtsonucnotlar.Text;
            GebeSonucEntity.GebelikHaftaNo = Convert.ToByte(edtGebelikHaftaNo.Value);
            GebeSonucEntity.GebelikNo = Convert.ToByte(edtgebelikNo.Value);
            GebeSonucEntity.GelisBicimi = (myenum.CocukGelisBicimi)edtgelisbicimi.Deger;
            GebeSonucEntity.OluDogumAdedi = Convert.ToByte(edtoludogumadedi.Value);
            GebeSonucEntity.CanliDogumAdedi = Convert.ToByte(edtcanlidogumadedi.Value);
            GebeSonucEntity.GebelikSonucu = (myenum.GebelikSonucu)edtgebeliksonucu.Deger;
            GebeSonucEntity.DogumYontemi = (myenum.DogumYontemi)edtdogumyontemi.Deger;
            GebeSonucEntity.DogumaYardimEden = (myenum.DogumaYardimEden)edtdogumayardimeden.Deger;
            GebeSonucEntity.DogumunYapildigiYer = (myenum.DogumunYapildigiYer)edtdogumungerceklestigiyer.Deger;
            GebeSonucEntity.Tarih =Convert.ToDateTime(edtsonuctarihi.EditValue);
            GebeSonucEntity.IzlemTarihi=DateEditIzlemTarihi.DateTime;
            GebeSonucEntity.Agirligi = spinEdit4.Value;
            int toplamcocuk = GebeSonucEntity.CanliDogumAdedi + GebeSonucEntity.OluDogumAdedi;
            if (toplamcocuk > 1)
                GebeSonucEntity.CogulDogummu = true;
            else
                GebeSonucEntity.CogulDogummu = false;
        }

        public override void showdata()
        {
             GebeSonucEntity = (GebeSonuc)formEntity;

             edtsonucnotlar.Text = GebeSonucEntity.SonucNotlar;
             edtgebelikNo.EditValue = GebeSonucEntity.GebelikNo;
             edtGebelikHaftaNo.EditValue = GebeSonucEntity.GebelikHaftaNo;
             edtgelisbicimi.Deger = GebeSonucEntity.GelisBicimi;
             edtoludogumadedi.EditValue = GebeSonucEntity.OluDogumAdedi;
             edtcanlidogumadedi.EditValue = GebeSonucEntity.CanliDogumAdedi;
             edtgebeliksonucu.Deger = GebeSonucEntity.GebelikSonucu;
             edtdogumyontemi.Deger = GebeSonucEntity.DogumYontemi;
             edtdogumayardimeden.Deger = GebeSonucEntity.DogumaYardimEden;
             edtdogumungerceklestigiyer.Deger = GebeSonucEntity.DogumunYapildigiYer;
             edtsonuctarihi.EditValue=GebeSonucEntity.Tarih;
             DateEditIzlemTarihi.DateTime = GebeSonucEntity.IzlemTarihi;
             spinEdit4.Value = GebeSonucEntity.Agirligi;
        }


        public override void formtamam()
        {
            Transaction.Instance.Join(
                       delegate()
                       {
                         
                           base.formtamam();
                           GebeBaslangic baslangic = Persistence.Read<GebeBaslangic>(GebeSonucEntity.GebeBaslangic.Id);
                           baslangic.GebelikSonuclanmaTarihi = GebeSonucEntity.Tarih;
                           baslangic.GebelikDurumu = myenum.GebelikDurumu.Bitti;
                           baslangic.Hasta = GebeSonucEntity.Hasta;
                           baslangic.Update();

                           //if (Current.AktifMuayeneId > 0)
                           //    Muayene.UpdateMuayenedurumu(Current.AktifMuayeneId, myenum.MuayeneDurumu.MuayeneEdildi);

                          
                         if (Current.AktifRandevuId > 0)
                         {
                            if (Convert.ToDateTime(GebeSonucEntity.EklemeTarihi.ToShortDateString())<Current.AktifRandevu.BasTarih)
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
                                    and   Takvim.BasTarih>@prm3"
                         , new object[] { Current.AktifHastaId, myenum.IslemTuru.Izlem.ToString(), myenum.IzlemTuru.Lohusa_Izlemi.ToString(),System.DateTime.Today,myenum.RandevuDurumu.Verildi.ToString() });
                         if (kayitlitakvimvarmi == 0)
                         {
                             Current.TakvimOlustur(myenum.IzlemTuru.Lohusa_Izlemi, GebeSonucEntity.Hasta, GebeSonucEntity.Tarih);
                         }

                           
                       }
          );
 
        }
    }
}






