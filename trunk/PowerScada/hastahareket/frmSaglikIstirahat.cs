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
using AHBS2010.Rapor;
using AHBS2010.Rapor.Raporlar;



namespace AHBS2010
{
    public partial class frmSaglikIstirahat : frmDialogBase
    {

        private SaglikIstirahat saglikistirahat;

        public SaglikIstirahat SaglikIstirahatEntity
        {
            get
            {
                if (saglikistirahat == null)
                    saglikistirahat = (SaglikIstirahat)CommandNew();
                else
                    saglikistirahat = (SaglikIstirahat)formEntity;
                return saglikistirahat;
            }
            set
            {
                saglikistirahat = value;
            }
        }

        public frmSaglikIstirahat()
        {
            InitializeComponent();
            this.formState = mymodel.myenum.EditMode.emYeni;
            InitDataControl();
            this.Load += new EventHandler(frmAnemnez_Load);

            InitializeForm();
        }

        void frmAnemnez_Load(object sender, EventArgs e)
        {
            DateEditRaporBasTarih.DateTime = System.DateTime.Today;
            SpinEditGunSayisi.Properties.MinValue = 0;
            SpinEditGunSayisi.Properties.MaxValue = 10000;
            ucEnumGoster1.ValueChanged += new EventHandler(ucEnumGoster1_ValueChanged);

            if (formEntity != null && formEntity.Id > 0)
                simpleButton1.Enabled = true;
            else
                simpleButton1.Enabled = false;
            if (SaglikIstirahatEntity != null && SaglikIstirahatEntity.Id > 0)
             SpinEditGunSayisi.Visible=SaglikIstirahatEntity.GunSayisi > 0;
        }

        void ucEnumGoster1_ValueChanged(object sender, EventArgs e)
        {
            if(ucEnumGoster1.Deger!=null)
            {
                myenum.RaporTuru rturu = (myenum.RaporTuru)ucEnumGoster1.Deger;
                if (rturu == myenum.RaporTuru.İş_Göremezlik_Raporu || rturu == myenum.RaporTuru.İstirahat_Raporu)
                {
                    SpinEditGunSayisi.Visible = true;
                    labelControl1.Visible = true;
                }
                else
                {
                    SpinEditGunSayisi.Visible = false;
                    SpinEditGunSayisi.EditValue=0;
                    labelControl1.Visible = false;
                }

            }
        }

        public frmSaglikIstirahat(long Id, mymodel.myenum.EditMode formstate)
        {
            InitializeComponent();
            this.formState = formstate;
            InitDataControl();
            this.Load += new EventHandler(frmAnemnez_Load);
            
            InitializeForm(Id, formstate);
        }

       

        public override void updatedata()
        {

            SaglikIstirahatEntity.GunSayisi =SpinEditGunSayisi.Value;
            SaglikIstirahatEntity.RaporBasTarih = DateEditRaporBasTarih.DateTime;
            SaglikIstirahatEntity.RaporTuru = (myenum.RaporTuru)ucEnumGoster1.Deger;
           
        }

        public override void showdata()
        {

            SpinEditGunSayisi.Value=Math.Round(SaglikIstirahatEntity.GunSayisi,0);
            DateEditRaporBasTarih.DateTime=SaglikIstirahatEntity.RaporBasTarih;
            ucEnumGoster1.Deger=SaglikIstirahatEntity.RaporTuru;
          
        }

        protected override Entity CommandNew()
        {
            SaglikIstirahat istirahat = new SaglikIstirahat();
            istirahat.Hasta.Id = Current.AktifHastaId;
            istirahat.Hasta = Current.AktifHasta;
            istirahat.Doktor.Id = Current.AktifHasta.Doktor.Id;
            if (Current.AktifDoktorId != istirahat.Doktor.Id)
            {
                istirahat.VekilDoktor.Id = Current.AktifDoktorId;
                istirahat.VekilDoktor = Current.AktifDoktor;
            }

            if (Current.AktifMuayeneId > 0)
            {
                istirahat.Muayene.Id = Current.AktifMuayeneId;
                istirahat.Muayene = Current.AktifMuayene;
            }
           
            if (Current.AktifRandevuId > 0)
            {
                istirahat.Randevu.Id = Current.AktifRandevuId;
                istirahat.Randevu = Current.AktifRandevu;
            }




            return istirahat;
       }

        protected override void CommandRead(long objId)
        {
            formEntity = SharpBullet.OAL.Persistence.Read<SaglikIstirahat>(objId);
            SaglikIstirahatEntity = (SaglikIstirahat)formEntity;
            if (SaglikIstirahatEntity.Hasta.Id > 0)
                SaglikIstirahatEntity.Hasta = Persistence.Read<Hasta>(SaglikIstirahatEntity.Hasta.Id);

            if (SaglikIstirahatEntity.Muayene.Id > 0)
                SaglikIstirahatEntity.Muayene = Persistence.Read<Muayene>(SaglikIstirahatEntity.Muayene.Id);
        }

        public override void formtamam()
        {
            base.formtamam();
            //if (Current.AktifMuayeneId > 0)
            //    Muayene.UpdateMuayenedurumu(Current.AktifMuayeneId, myenum.MuayeneDurumu.MuayeneEdildi);
           
                if (Current.AktifRandevuId > 0)
                {
                    if (Convert.ToDateTime(SaglikIstirahatEntity.EklemeTarihi.ToShortDateString())<Current.AktifRandevu.BasTarih)
                        throw new Exception("İleri tarihli bir randevu işlem yapılamaz.");
               
                    Takvim.UpdateTakvimDurumu(Current.AktifRandevuId, myenum.RandevuDurumu.Geldi);
                }
                simpleButton1.Enabled = true;
                if (MessageBox.Show("Rapor Dökümü istermisiniz", "Bilgi", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                {
                    simpleButton1_Click(null, null);
                }
                
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
          
                if (ucEnumGoster1.Deger != null)
                {
                    myenum.RaporTuru rprturu = (myenum.RaporTuru)ucEnumGoster1.Deger;

                    switch (rprturu)
                    {
                        case myenum.RaporTuru.SGK_Çalışabilir_Kağıdı:
                            SSKCalisabilirKagidi();
                            break;
                        case myenum.RaporTuru.İş_Göremezlik_Raporu:
                            GEciciIsGoremezlikRaporu();
                            break;
                        case myenum.RaporTuru.Sağlık_Raporu_İşe_Giriş:
                            IseGirisRaporu();
                            break;
                        case myenum.RaporTuru.Sağlık_Raporu_Ehliyet:
                            EhliyetRaporuDokumu();
                            break;
                        case myenum.RaporTuru.Sağlık_Raporu_Evlilik:
                            EvlilikRaporu();
                            break;
                        case myenum.RaporTuru.Sağlık_Raporu:
                            SaglikRaporu();
                            break;

                        case myenum.RaporTuru.İstirahat_Raporu:
                            IsTirahatRaporu();
                            break;
                        default:
                            break;
                    }
                }
           
        }

        public void EhliyetRaporuDokumu()
        {
            string sql = @"SELECT
                             M.MuayeneTarihi as MTarih
	                        ,M.SiraNo        as SiraNo 
	                        ,H.Adi+' '+H.Soyadi  as AdiSoyadi
	                        ,H.BabaAdi		 as BabaAdi
	                        ,H.AnneAdi		 as AnneAdi
	                        ,isnull(H.BeyanDogumTarihi,H.DogumTarihi) as DogumTarihi
	                        ,H.DogumYeri	 as DogumYeri
	                        ,H.BeyanAdresi	 as BeyanAdresi
	                        ,D.Adi+' '+D.Soyadi  as DAdiSoyadi
	                        ,D.Diplomano	 as Diplomano
	                        ,Convert(nvarchar(15), isnull(H.BeyanDogumTarihi,H.DogumTarihi),104)+' doğumlu '+ H.BabaAdi+' '+case when H.Cinsiyeti='Erkek' then 'oğlu,' else 'Kızı,' end+ cast(H.TckNo as nvarchar(100))+' T.C Kimlik Numaralı '+H.Adi+' '+H.Soyadi   
	                        +' 2918 sayılı karayolları trafik kanunun 41.inci maddesinin birici fıkrasının (C) bendine dayanılarak hazırlanan ve 26/09/2006 tarih ve 26301 sayılı resmi gazetede yayınlanarak yürürlüğe giren ""Sürücü Adayları ve Sürücülerde Aranacak sağlık Şartları ile Muayenelerine Dair Tönetmelik"" te belirtilen hususlara uygun olarak muayenesi yapılmış olup; ""Sürücü Belgesi almasını engelleyici bir sağlık problemi bulunmamaktadır."" şeklinde karar verilmiştir.' as Kelime " +
                                @" 
                        FROM SaglikIstirahat S 
                        INNER JOIN Hasta AS H   ON H.Id=S.Hasta_Id and H.Aktif=1
                        INNER JOIN Muayene as M  ON M.Hasta_Id=H.Id and M.Id=" + Current.AktifMuayeneId + " and M.Aktif=1 " +
                    @"  INNER JOIN Doktor  as D  ON D.Id=dbo.iszero(M.VekilDoktor_Id,M.Doktor_Id)
                        WHERE
                        S.Id=@prm0  and RaporTuru='@prm1'  and S.Aktif=1";

            sql = sql.Replace("@prm0", formEntity.Id.ToString());
            sql = sql.Replace("@prm1", ucEnumGoster1.Deger.ToString());
            

            EhliyetRaporu Ehliyet = new EhliyetRaporu();
            Ehliyet.DataSource =Transaction.Instance.ExecuteSql(sql); 
            Ehliyet.DataMember = "Table";
            Ehliyet.ShowPreview();
        }

        public void EvlilikRaporu()
        {
             string sql = @"SELECT
                             M.MuayeneTarihi as MTarih
	                        ,M.SiraNo        as SiraNo 
	                        ,H.Adi			 as Adi
	                        ,H.Soyadi		 as Soyadi
							,H.TckNo		 as TcKNo
	                        ,H.BabaAdi		 as BabaAdi
	                        ,H.AnneAdi		 as AnneAdi
	                        ,datepart(yy,isnull(H.BeyanDogumTarihi,H.DogumTarihi)) as DogumYili
	                        ,H.DogumYeri	 as DogumYeri
	                        ,H.BeyanAdresi	 as BeyanAdresi
	                        ,D.Adi+' '+D.Soyadi  as DAdiSoyadi
	                        ,D.Diplomano	 as Diplomano
	                        ,M.ProtokolNo	 as ProtokolNo
                        FROM SaglikIstirahat S 
                        INNER JOIN Hasta AS H   ON H.Id=S.Hasta_Id and H.Aktif=1
                        INNER JOIN Muayene as M  ON M.Hasta_Id=H.Id and M.Id="+Current.AktifMuayeneId+" and M.Aktif=1 "+
                    @"  INNER JOIN Doktor  as D  ON D.Id=dbo.iszero(M.VekilDoktor_Id,M.Doktor_Id)
                        WHERE
                        S.Id=@prm0  and RaporTuru='@prm1'  and S.Aktif=1" ;

            sql=sql.Replace("@prm0",formEntity.Id.ToString());
            sql = sql.Replace("@prm1", ucEnumGoster1.Deger.ToString());


            EvlilikRaporu Evlilik = new EvlilikRaporu();
            Evlilik.DataSource = Transaction.Instance.ExecuteSql(sql);
            Evlilik.DataMember = "Table";
            Evlilik.ShowPreview();
        }

        public void SaglikRaporu()
        {
            string sql = @" SELECT
	                         GETDATE()									AS RAPORTARIHI
                            ,H.DogumTarihi								AS DOGUMTARIHI
                            ,M.ProtokolNo								AS PROTOKOLNO
                            ,H.KurumTipi								AS KURUMTIPI
	                        ,H.TckNo									AS TCKIMLIKNO
                            ,H.Adi										AS ADI
                            ,H.Soyadi									AS SOYADI
                            ,H.Cinsiyeti								AS CINSIYET 
                            ,ISNULL(H.BeyanDogumTarihi,H.DogumTarihi)   AS DOGUMTARIHI
                            ,H.KurumTipi								AS KURUMTIPI
                            ,D.Adi+' '+D.Soyadi							AS DADISOYADI
                            ,D.Diplomano								AS DIPLOMANO
                        FROM SaglikIstirahat S 
                        INNER JOIN Hasta AS H   ON H.Id=S.Hasta_Id and H.Aktif=1
                        INNER JOIN Muayene as M  ON M.Hasta_Id=H.Id and M.Id=" + Current.AktifMuayeneId + " and M.Aktif=1 " +
                        @"  INNER JOIN Doktor  as D  ON D.Id=dbo.iszero(M.VekilDoktor_Id,M.Doktor_Id)
                        WHERE
                        S.Id=@prm0  and RaporTuru='@prm1'  and S.Aktif=1";

            sql = sql.Replace("@prm0", formEntity.Id.ToString());
            sql = sql.Replace("@prm1", ucEnumGoster1.Deger.ToString());


            SaglikRaporu saglikraporu = new SaglikRaporu();
            saglikraporu.DataSource = Transaction.Instance.ExecuteSql(sql);
            saglikraporu.DataMember = "Table";
            saglikraporu.ShowPreview();
        }

        public void IseGirisRaporu()
        {
            string sql = @"SELECT
                             M.MuayeneTarihi as MTarih
	                        ,H.Adi			 as Adi
	                        ,H.Soyadi		 as Soyadi
							,H.TckNo		 as TcKNo
							,H.Cinsiyeti	 as Cinsiyeti
	                        ,datediff(yy,isnull(H.BeyanDogumTarihi,H.DogumTarihi),getdate()) as Yas
	                        ,H.KurumTipi	 as Kurumu
	                        ,H.BeyanAdresi	 as BeyanAdresi
	                        ,D.Adi+' '+D.Soyadi  as DAdiSoyadi
	                        ,D.Diplomano	 as Diplomano
	                        ,M.ProtokolNo	 as ProtokolNo
                        FROM SaglikIstirahat S 
                        INNER JOIN Hasta AS H   ON H.Id=S.Hasta_Id and H.Aktif=1
                        INNER JOIN Muayene as M  ON M.Hasta_Id=H.Id and M.Id=" + Current.AktifMuayeneId + " and M.Aktif=1 " +
                    @"  INNER JOIN Doktor  as D  ON D.Id=dbo.iszero(M.VekilDoktor_Id,M.Doktor_Id)
                        WHERE
                        S.Id=@prm0  and RaporTuru='@prm1'  and S.Aktif=1";

            sql = sql.Replace("@prm0", formEntity.Id.ToString());
            sql = sql.Replace("@prm1", ucEnumGoster1.Deger.ToString());


            IseGirisRaporu Evlilik = new IseGirisRaporu();
            Evlilik.DataSource = Transaction.Instance.ExecuteSql(sql);
            Evlilik.DataMember = "Table";
            Evlilik.ShowPreview();
        }

        public void SSKCalisabilirKagidi()
        {
            string sql = @"SELECT
                                H.TckNo			 as TcKNo
                                ,H.KurumSicilNo		 as SicilNo
                                ,H.Adi+' '+H.Soyadi	 as Adi
                                ,M.ProtokolNo	 as ProtokolNo
                                ,M.MuayeneTarihi as MTarih
                                ,D.Adi+' '+D.Soyadi  as DAdiSoyadi
                                ,D.Diplomano	 as Diplomano
                                ,M.EklemeTarihi	 as EklemeTarihi
                        FROM SaglikIstirahat S 
                        INNER JOIN Hasta AS H   ON H.Id=S.Hasta_Id and H.Aktif=1
                        INNER JOIN Muayene as M  ON M.Hasta_Id=H.Id and M.Id=" + Current.AktifMuayeneId + " and M.Aktif=1 " +
                    @"  INNER JOIN Doktor  as D  ON D.Id=dbo.iszero(M.VekilDoktor_Id,M.Doktor_Id)
                        WHERE
                        S.Id=@prm0  and RaporTuru='@prm1'  and S.Aktif=1";

            sql = sql.Replace("@prm0", formEntity.Id.ToString());
            sql = sql.Replace("@prm1", ucEnumGoster1.Deger.ToString());


            SSKCalisabilirKagidiRaporu sskcalisabilirkagidi = new SSKCalisabilirKagidiRaporu();
            sskcalisabilirkagidi.DataSource = Transaction.Instance.ExecuteSql(sql);
            sskcalisabilirkagidi.DataMember = "Table";
            sskcalisabilirkagidi.ShowPreview();
        }


        public void GEciciIsGoremezlikRaporu()
        {
            string sql = @"SELECT
                               H.TckNo				 as TcKNo
                                ,H.KurumSicilNo		 as SicilNo
                                ,H.Adi+' '+H.Soyadi	 as AdiSoyadi
								,H.BeyanAdresi		 as BeyanAdresi
                                ,H.KurumSicilNo		 as KurumSicilNo
                                ,M.ProtokolNo		 as ProtokolNo
                                ,M.MuayeneTarihi	 as MTarih
                                ,D.Adi+' '+D.Soyadi  as DAdiSoyadi
                                ,D.Diplomano		 as Diplomano
                                ,M.EklemeTarihi		 as EklemeTarihi
								,S.RaporBasTarih	 as RaporBasTarih
								,dateadd(dd,S.GunSayisi,S.RaporBasTarih) as RaporBitTarih
                        FROM SaglikIstirahat S 
                        INNER JOIN Hasta AS H   ON H.Id=S.Hasta_Id and H.Aktif=1
                        INNER JOIN Muayene as M  ON M.Hasta_Id=H.Id and M.Id=" + Current.AktifMuayeneId + " and M.Aktif=1 " +
                    @"  INNER JOIN Doktor  as D  ON D.Id=dbo.iszero(M.VekilDoktor_Id,M.Doktor_Id)
                        WHERE
                        S.Id=@prm0  and RaporTuru='@prm1'  and S.Aktif=1";

            sql = sql.Replace("@prm0", formEntity.Id.ToString());
            sql = sql.Replace("@prm1", ucEnumGoster1.Deger.ToString());


            GeciciIsGormezlikRaporu geciciisgoremezlirpr = new GeciciIsGormezlikRaporu();
            geciciisgoremezlirpr.DataSource = Transaction.Instance.ExecuteSql(sql);
            geciciisgoremezlirpr.DataMember = "Table";
            geciciisgoremezlirpr.ShowPreview();
        }


        public void IsTirahatRaporu()
        {
            string sql = @" SELECT
	                         GETDATE()									AS RAPORTARIHI
                            ,H.DogumTarihi								AS DOGUMTARIHI
                            ,M.ProtokolNo								AS PROTOKOLNO
                            ,H.KurumTipi								AS KURUMTIPI
	                        ,H.TckNo									AS TCKIMLIKNO
                            ,H.Adi										AS ADI
                            ,H.Soyadi									AS SOYADI
                            ,H.Cinsiyeti								AS CINSIYET 
                            ,ISNULL(H.BeyanDogumTarihi,H.DogumTarihi)   AS DOGUMTARIHI
                            ,H.KurumTipi								AS KURUMTIPI
                            ,D.Adi+' '+D.Soyadi							AS DADISOYADI
                            ,D.Diplomano								AS DIPLOMANO
                            ,(SELECT kodu FROM Teshis WHERE Teshis.Id=MT.Teshis_Id) AS TANIKODU
                            ,(SELECT adi FROM Teshis WHERE Teshis.Id=MT.Teshis_Id)  AS TANIADI
                            ,S.GunSayisi											AS GUNSAYISI
                            ,S.RaporBasTarih										AS ISTIRAHATBASTARIHI
                            ,DATEADD(DD,S.GunSayisi,S.RaporBasTarih)				AS ISTIRAHATBITTARIHI								
                        FROM SaglikIstirahat S 
                        INNER JOIN Hasta AS H   ON H.Id=S.Hasta_Id and H.Aktif=1
                        INNER JOIN Muayene as M  ON M.Hasta_Id=H.Id and M.Id=" + Current.AktifMuayeneId + " and M.Aktif=1 " +
                     @" INNER JOIN Doktor  as D  ON D.Id=dbo.iszero(M.VekilDoktor_Id,M.Doktor_Id)
                        INNER JOIN MuayeneTeshis MT ON MT.Muayene_Id=M.Id
                        WHERE
                        S.Id=@prm0  and RaporTuru='@prm1'  and S.Aktif=1";
            sql = sql.Replace("@prm0", formEntity.Id.ToString());
            sql = sql.Replace("@prm1", ucEnumGoster1.Deger.ToString());


            IstirahatRaporu istirahatraporu = new IstirahatRaporu();
            istirahatraporu.DataSource = Transaction.Instance.ExecuteSql(sql);
            istirahatraporu.DataMember = "Table";
            istirahatraporu.ShowPreview();
        }
    }
}