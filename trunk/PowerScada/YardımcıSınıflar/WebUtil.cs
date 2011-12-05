using System;
using System.Collections.Generic;
using System.Text;
using wsAh30;
using wsAh30.rMvs;
using wsAh30.rSozluk;
using mymodel;
using SharpBullet.OAL;
using System.Data;
using System.Windows.Forms;
using wsAh30.rHastaBilgi;
using AHBS2010.TUIKBilgi;
using System.IO;
using AHBS2010.LocalLab;

namespace AHBS2010
{
    public static class WebUtil
    {
        public static bool tahlilal(string barkodd,string hastatckno, bool tekrarmi)
        {
            if (barkodd.Length > 0)
            {
                MuayeneTetkik[] mtetkik = Persistence.ReadList<MuayeneTetkik>(@"select * from MuayeneTetkik where barkod=@prm0", barkodd);
                if (mtetkik == null)
                    return false;
                if (mtetkik.Length == 0)
                    return false;

                if (!tekrarmi)
                {
                    if (System.IO.File.Exists(Current.pdfklasor + "\\" + hastatckno + "_" + barkodd + ".pdf"))
                    return true;
                }

                LabSoapClient lsc = new LabSoapClient();
                Dosya[] dosya = new Dosya[1];
                string sSonuc = "";
                string sSonucAciklama = "";

                try
                {
                    dosya = lsc.LabTetkikGetir(Current.AktifDoktor.TckNo.ToString(),
                            Current.AktifDoktor.TckNo.ToString(),
                            Current.AktifDoktor.WebServisSifre, "", barkodd,
                            hastatckno, "", "",
                            out sSonuc, out sSonucAciklama);
                    if (dosya.Length == 0)
                    {
                        foreach (MuayeneTetkik mt in mtetkik)
                        {
                            mt.Sonuc = sSonucAciklama;
                            mt.Update();
                        } 
                        //MessageBox.Show(Current.pdfklasor + "\\" + Current.AktifHasta.TckNo.ToString() + "_" + barkodd +
                        //    ".pdf dosyası bilgisayarınızda bulunamadı.\nMerkezi sistemden tekrar istendi ancak çekilemedi.\nKarşı sistemden gelen mesaj:\n\n-" + sSonucAciklama + "-");
                        return false;
                    }

                    foreach (MuayeneTetkik mt in mtetkik)
                    {
                        mt.Sonuc = "Geldi";
                        mt.DonusTarihi = DateTime.Now;
                        mt.Update();
                    }

                    //int i = 0;
                    //foreach (Dosya item in dosya)
                    //{
                        //i++;
                        byte[] encodedDataAsBytes = System.Convert.FromBase64String(dosya[0].dosya);
                        string pdfstr = System.Text.Encoding.GetEncoding("ISO-8859-9").GetString(encodedDataAsBytes);
                        TextWriter writer = new StreamWriter(Current.pdfklasor + "\\" + hastatckno + "_" + barkodd
                            + ".pdf", false, Encoding.GetEncoding("ISO-8859-9"));
                        writer.Write(pdfstr);
                        writer.Flush();
                        writer.Close();
                        writer.Dispose();
                   // }
                    if (System.IO.File.Exists(Current.pdfklasor + "\\" + hastatckno + "_" + barkodd + ".pdf"))
                        return true;
                    else
                        return false;

                }

                catch
                {
                    return false;
                }
            }
            return true;

            #region bakanlik
            ///////////////////////////////////////////////////////////////////////////////////////////////////////

            //CLaboratuvar lab = new CLaboratuvar();
            //string rMesaj; int Sonuc;

            //LaboratuvarHastaTetkikListeleCevap labsonuc = lab.fHastaTetkikListele(
            //    "P",
            //    Current.AktifDoktor.TckNo.ToString(),
            //    Current.AktifDoktor.TckNo.ToString(),
            //    Current.AktifDoktor.WebServisSifre,
            //    Current.AktifDoktor.Adi,
            //    Current.AktifDoktor.Soyadi,
            //    0,
            //    33,
            //    Current.AktifHasta.TckNo.ToString(),
            //    Current.AktifHasta.Adi,
            //    Current.AktifHasta.Soyadi,
            //    Current.AktifHasta.Cinsiyeti.ToString()[0].ToString(),
            //    Current.AktifHasta.BeyanCinsiyeti.ToString()[0].ToString(),
            //    Current.AktifHasta.BeyanDogumTarihi.ToString("yyyyMMdd"),
            //    Current.AktifHasta.DogumTarihi.ToString("yyyyMMdd"),
            //    out rMesaj,
            //    out Sonuc
            //    );

            //LaboratuvarTetkikGetirCevap tetkiksonuc = lab.fTetkikGetir
            //    (
            //     "P",
            //    Current.AktifDoktor.TckNo.ToString(),
            //    Current.AktifDoktor.TckNo.ToString(),
            //    Current.AktifDoktor.WebServisSifre,
            //    Current.AktifDoktor.Adi,
            //    Current.AktifDoktor.Soyadi,
            //    0,
            //    33,
            //    "100827000222",
            //    "170011",
            //    "28e6eeb2-a39b-479c-b201-8a71837feeb9",
            //    "2681",
            //    "KIRKLARELİ HALK SAĞLIĞI LABORATUARI",

            //    out rMesaj,
            //    out Sonuc

            //);
            #endregion bakanlik
        }


        public static DataTable AktifHekimTumHastaOzetGetir(string calismatur)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string[] HastaTckNo;
                string[] HastaAd;
                string[] HastaSoyad;

                CMvs mv = new CMvs();
                int sonuc;
                string mesaj = "";

                Current.globalresmessage = mv.fHastaHekimNoIle(
                calismatur,
                Current.AktifDoktor.TckNo.ToString(),
                Current.AktifDoktor.TckNo.ToString(),
                Current.AktifDoktor.WebServisSifre,
                Current.AktifDoktor.Adi,
                Current.AktifDoktor.Soyadi,
                Current.AktifDoktor.TckNo.ToString(),
                Current.AktifDoktor.Adi,
                Current.AktifDoktor.Soyadi,
                "",
                out HastaTckNo,
                out HastaAd,
                out HastaSoyad,
                out sonuc
                );

                //wsAh30.rMvs.HASTASORGUBILESEN[] gezicihastalarim = mv.fHastaHekimNoIleGezici(
                //calismatur,
                //Current.AktifDoktor.TckNo.ToString(),
                //Current.AktifDoktor.TckNo.ToString(),
                //Current.AktifDoktor.WebServisSifre,
                //Current.AktifDoktor.Adi,
                //Current.AktifDoktor.Soyadi,
                //Current.AktifDoktor.TckNo.ToString(),
                //Current.AktifDoktor.Adi,
                //Current.AktifDoktor.Soyadi,
                //Current.AktifDoktor.Diplomano,
                //out mesaj,
                //out sonuc);  

                if (HastaTckNo == null)
                {
                    MessageBox.Show(Current.AktifDoktor.Adi + " " +
                        Current.AktifDoktor.Soyadi + " (" +
                        Current.AktifDoktor.TckNo.ToString() + ") Doktora ait hasta listesi bulunamadı ya da bakanlıkla bağlantıda bir problem çıktı.\n Lütfen bir süre sonra tekrar deneyiniz \n" + Current.globalresmessage);
                    return null;
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("HastaTckNo");
                dt.Columns.Add("HastaAd");
                dt.Columns.Add("HastaSoyad");
                dt.Columns.Add("Gezici");

                //normal hastalar
                for (int i = 0; i < HastaTckNo.Length; i++)
                {
                    DataRow row = dt.NewRow();

                    row["HastaTckNo"] = HastaTckNo[i];
                    row["HastaAd"] = HastaAd[i];
                    row["HastaSoyad"] = HastaSoyad[i];
                    row["Gezici"] = false;
                    dt.Rows.Add(row);
                }

                //gezici olarak gidilen hastalar
                //foreach (var item in gezicihastalarim)
                //{
                //    DataRow[] foundRows = dt.Select("HastaTckNo=" + item.HASTA_REF.TCKIMLIK_NO);
                //    if (foundRows.Length>0)
                //        foundRows[0]["Gezici"] = true;
                //}
                return dt;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public static List<HASTAKAYITBILGISI> AktifHekimTumHastaTumBilgiGetir()
        {
            string mesaj = "";
            string[] HastaTckNo;
            string[] HastaAd;
            string[] HastaSoyad;
            int sonuc;
            CMvs mv = new CMvs();
            Current.globalresmessage = mv.fHastaHekimNoIle(
            "P",
            Current.AktifDoktor.TckNo.ToString(),
            Current.AktifDoktor.TckNo.ToString(),
            Current.AktifDoktor.WebServisSifre,
            Current.AktifDoktor.Adi,
            Current.AktifDoktor.Soyadi,
            Current.AktifDoktor.TckNo.ToString(),
            Current.AktifDoktor.Adi,
            Current.AktifDoktor.Soyadi,
            "",
            out HastaTckNo,
            out HastaAd,
            out HastaSoyad,
            out sonuc
            );

            List<HASTAKAYITBILGISI> hastalistesi = new List<HASTAKAYITBILGISI>();
            for (int i = 0; i < HastaTckNo.Length; i++)
            {
                HASTAKAYITBILGISI hasta = mv.fHastaKimlikNoIle("P",
                  Current.AktifDoktor.TckNo.ToString(),
                  Current.AktifDoktor.TckNo.ToString(),
                  Current.AktifDoktor.WebServisSifre,
                  Current.AktifDoktor.Adi,
                  Current.AktifDoktor.Soyadi,
                  HastaTckNo[i],
                  HastaAd[i],
                  HastaSoyad[i],
                  out mesaj,
                  out sonuc);

                hastalistesi.Add(hasta);
                //BenimEntitylereBindEt(hasta);
            }
            return hastalistesi;
        }

        private static void BenimEntitylereBindEt(HASTAKAYITBILGISI bakanlikhasta)
        {
            Hasta Localhasta = new Hasta();

            BebekIzleme LocalBebeklikIzleme = new BebekIzleme();

            List<MuayeneTeshis> LocalMuayeneTeshisler = new List<MuayeneTeshis>();


            if (bakanlikhasta.HASTA_ALERJI_BILGI.Length != 0)
            {
                foreach (HASTA_ALERJI bakanlikhastaAlerji in bakanlikhasta.HASTA_ALERJI_BILGI)
                {
                    MuayeneTeshis LocalMuayeneTeshis = new MuayeneTeshis();

                    LocalMuayeneTeshis.Teshis.Id = Transaction.Instance.ExecuteScalarL("Select Id from Teshis where Kodu = @prm0", bakanlikhastaAlerji.ALERJI_TIP.AlerjiKod);

                    LocalMuayeneTeshis.Alerjikmi = true;

                    //TODO : Muayene ve Hasta ile İlişki Nasıl Kurulacak.

                    LocalMuayeneTeshisler.Add(LocalMuayeneTeshis);
                }
            }



            #region bakanhasta.HASTA_KAYIT_KIMLIK_BILGI.BEYAN Propert si ile ilgili alanları Bizim Localdeki Hasta Entity sinin ilgili
            if (bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI != null)
            {
                Localhasta.Doktor.Id = Transaction.Instance.ExecuteScalarL("Select Id from Doktor where TckNo = @prm0", long.Parse(bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.AILE_HEKIMI.TCKIMLIK_NO));


                Localhasta.AnneAdi = bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.ANNE_AD;
                Localhasta.BabaAdi = bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.BABA_AD;
                Localhasta.BeyanCinsiyeti = (myenum.Cinsiyet)Enum.Parse(typeof(myenum.Cinsiyet), bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.BEYAN_CINSIYET.CinsiyetAd);
                Localhasta.BeyanDogumTarihi = Convert.ToDateTime(bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.BEYAN_DOGUM_TARIHI);
                Localhasta.Cinsiyeti = (myenum.Cinsiyet)Enum.Parse(typeof(myenum.Cinsiyet), bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.CINSIYET.CinsiyetAd);
                Localhasta.DogumTarihi = Convert.ToDateTime(bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.DOGUM_TARIHI);
                Localhasta.Adi = bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.HASTAKIMLIK.AD;
                Localhasta.Soyadi = bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.HASTAKIMLIK.SOYAD;
                Localhasta.TckNo = long.Parse(bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.HASTAKIMLIK.TCKIMLIK_NO);
            }
            #endregion

        }

        public static DataTable BakanliktanButunHastaBilgileriGetir()
        {
            string mesaj = "";
            int sonuc;
            string[] HastaTckNo;
            string[] HastaAd;
            string[] HastaSoyad;
            CMvs mv = new CMvs();
            Current.globalresmessage = mv.fHastaHekimNoIle(
                "P",
                Current.AktifDoktor.TckNo.ToString(),
                Current.AktifDoktor.TckNo.ToString(),
                Current.AktifDoktor.WebServisSifre,
                Current.AktifDoktor.Adi,
                Current.AktifDoktor.Soyadi,
                Current.AktifDoktor.TckNo.ToString(),
                Current.AktifDoktor.Adi,
                Current.AktifDoktor.Soyadi,
                "",
                out HastaTckNo,
                out HastaAd,
                out HastaSoyad,
                out sonuc
                );

            DataTable dataschema = new DataTable();

            dataschema.Columns.Add("HASTA_ALERJI_BILGI0_ALERJI_ACIKLAMA");
            dataschema.Columns.Add("HASTA_ALERJI_BILGI0_ALERJI_TIP_AlerjiAd");
            dataschema.Columns.Add("HASTA_ALERJI_BILGI0_ALERJI_TIP_AlerjiKod");
            dataschema.Columns.Add("HASTA_ALERJI_BILGI0_ALERJI_TIP_KodSistemAd");
            dataschema.Columns.Add("HASTA_ALERJI_BILGI0_ALERJI_TIP_KodSistemKod");
            dataschema.Columns.Add("HASTA_ALERJI_BILGI0_ALERJIDEVAMEDIYORMU");

            dataschema.Columns.Add("HASTA_BEBEKLIK_BILGI_AGIRLIK_TIP_AgirlikBirimAd");
            dataschema.Columns.Add("HASTA_BEBEKLIK_BILGI_AGIRLIK_TIP_AgirlikBirimKod");
            dataschema.Columns.Add("HASTA_BEBEKLIK_BILGI_AGIRLIK_TIP_KodSistemAd");
            dataschema.Columns.Add("HASTA_BEBEKLIK_BILGI_AGIRLIK_TIP_KodSistemKod");
            dataschema.Columns.Add("HASTA_BEBEKLIK_BILGI_DOGUM_AGIRLIK");
            dataschema.Columns.Add("HASTA_BEBEKLIK_BILGI_DOGUM_BASCEVRE");
            dataschema.Columns.Add("HASTA_BEBEKLIK_BILGI_DOGUM_BOY");
            dataschema.Columns.Add("HASTA_BEBEKLIK_BILGI_DOGUM_KOMPLIKASYONU_VARMI");
            dataschema.Columns.Add("HASTA_BEBEKLIK_BILGI_EK_GIDAYA_BASLAMA_AYI");
            dataschema.Columns.Add("HASTA_BEBEKLIK_BILGI_FENIL_KAN_ALINDIMI");
            dataschema.Columns.Add("HASTA_BEBEKLIK_BILGI_UZUNLUK_BIRIM");
            dataschema.Columns.Add("HASTA_BEBEKLIK_BILGI_FENIL_UZUNLUK_BIRIM");

            dataschema.Columns.Add("HASTA_DURUM_BILGI_HastaKayitDurumAd");
            dataschema.Columns.Add("HASTA_DURUM_BILGI_HastaKayitDurumKod");
            dataschema.Columns.Add("HASTA_DURUM_BILGI_KodSistemAd");
            dataschema.Columns.Add("HASTA_DURUM_BILGI_KodSistemKod");

            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ACIL_ILETISIM0_ILETISIM_BILGILERI_DURUM");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ACIL_ILETISIM0_ILETISIM_BILGILERI_ILETISIM_DEGER");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ACIL_ILETISIM0_ILETISIM_BILGILERI_ILETISIM_TIP_IletisimTipAd");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ACIL_ILETISIM0_ILETISIM_BILGILERI_ILETISIM_TIP_IletisimTipKod");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ACIL_ILETISIM0_ILETISIM_BILGILERI_ILETISIM_TIP_KodSistemAd");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ACIL_ILETISIM0_ILETISIM_BILGILERI_ILETISIM_TIP_KodSistemKod");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ACIL_ILETISIM0_KISI_AD_SOYAD");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ADRES_ACIK");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ADRES_TIP");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_DURUM");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_IL_IlAd");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_IL_IlKod");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_IL_IlKodSpecified");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_IL_KodSistemAd");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_IL_KodSistemKod");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ILCE_IlceAd");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ILCE_IlceKod");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ILCE_IlceKodSpecified");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ILCE_KodSistemAd");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ILCE_KodSistemKod");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_MAHALLE_KodSistemAd");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_MAHALLE_KodSistemKod");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_MAHALLE_MahalleAd");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_MAHALLE_MahalleKod");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_POSTA_KODU_KodSistemAd");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_POSTA_KODU_KodSistemKod");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_POSTA_KODU_PostaKoduAd");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_POSTA_KODU_PostaKoduKod");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ULKE_KodSistemAd");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ULKE_KodSistemKod");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ULKE_UlkeAd");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ULKE_UlkeKod");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_TIP0_DURUM");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_TIP0_ILETISIM_DEGER");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_TIP0_ILETISIM_TIP_IletisimTipAd");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_TIP0_ILETISIM_TIP_IletisimTipKod");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_TIP0_ILETISIM_TIP_KodSistemAd");
            dataschema.Columns.Add("HASTA_ILETISIM_BILGI_HASTA_ILETISIM_TIP0_ILETISIM_TIP_KodSistemKod");

            dataschema.Columns.Add("HASTA_KAYIT_KIMLIK_BILGI_AILE_HEKIMI_AD");
            dataschema.Columns.Add("HASTA_KAYIT_KIMLIK_BILGI_AILE_HEKIMI_DIPLOMA_NO");
            dataschema.Columns.Add("HASTA_KAYIT_KIMLIK_BILGI_AILE_HEKIMI_SOYAD");
            dataschema.Columns.Add("HASTA_KAYIT_KIMLIK_BILGI_AILE_HEKIMI_TCKIMLIK_NO");
            dataschema.Columns.Add("HASTA_KAYIT_KIMLIK_BILGI_ANNE_AD");
            dataschema.Columns.Add("HASTA_KAYIT_KIMLIK_BILGI_BABA_AD");
            dataschema.Columns.Add("HASTA_KAYIT_KIMLIK_BILGI_BEYAN_CINSIYET");
            dataschema.Columns.Add("HASTA_KAYIT_KIMLIK_BILGI_BEYAN_DOGUM_TARIHI");
            dataschema.Columns.Add("HASTA_KAYIT_KIMLIK_BILGI_CINSIYET");
            dataschema.Columns.Add("HASTA_KAYIT_KIMLIK_BILGI_DOGUM_TARIHI");
            dataschema.Columns.Add("HASTA_KAYIT_KIMLIK_BILGI_HASTAKIMLIK_AD");
            dataschema.Columns.Add("HASTA_KAYIT_KIMLIK_BILGI_HASTAKIMLIK_SOYAD");
            dataschema.Columns.Add("HASTA_KAYIT_KIMLIK_BILGI_HASTAKIMLIK_TCKIMLIK_NO");
            dataschema.Columns.Add("HASTA_KAYIT_KIMLIK_BILGI_ID");

            dataschema.Columns.Add("HASTA_SOSYAL_EGITIM_BILGI_EGITIM_DURUM_EgitimDurumAd");
            dataschema.Columns.Add("HASTA_SOSYAL_EGITIM_BILGI_EGITIM_DURUM_EgitimDurumKod");
            dataschema.Columns.Add("HASTA_SOSYAL_EGITIM_BILGI_EGITIM_DURUM_KodSistemAd");
            dataschema.Columns.Add("HASTA_SOSYAL_EGITIM_BILGI_EGITIM_DURUM_KodSistemKod");
            dataschema.Columns.Add("HASTA_SOSYAL_EGITIM_BILGI_GEZICIHIZMETALIYORMU");
            dataschema.Columns.Add("HASTA_SOSYAL_EGITIM_BILGI_KAN_GRUB");
            dataschema.Columns.Add("HASTA_SOSYAL_EGITIM_BILGI_MEDENI_HAL_KodSistemAd");
            dataschema.Columns.Add("HASTA_SOSYAL_EGITIM_BILGI_MEDENI_HAL_KodSistemKod");
            dataschema.Columns.Add("HASTA_SOSYAL_EGITIM_BILGI_MEDENI_HAL_MedeniHalAd");
            dataschema.Columns.Add("HASTA_SOSYAL_EGITIM_BILGI_MEDENI_HAL_MedeniHalKod");
            dataschema.Columns.Add("HASTA_SOSYAL_EGITIM_BILGI_MESLEK");
            dataschema.Columns.Add("HASTA_SOSYAL_EGITIM_BILGI_SOSYAL_GUVENLIK_KURUM_KodSistemAd");
            dataschema.Columns.Add("HASTA_SOSYAL_EGITIM_BILGI_SOSYAL_GUVENLIK_KURUM_KodSistemKod");
            dataschema.Columns.Add("HASTA_SOSYAL_EGITIM_BILGI_SOSYAL_GUVENLIK_KURUM_SosyalGuvenlikKurumAd");
            dataschema.Columns.Add("HASTA_SOSYAL_EGITIM_BILGI_SOSYAL_GUVENLIK_KURUM_SosyalGuvenlikKurumKod");
            dataschema.Columns.Add("HASTA_SOSYAL_EGITIM_BILGI_UYRUGU");

            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_ACIKLAMA");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_AILE_BILGI_AKRABA_TIP_AkrabaAd");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_AILE_BILGI_AKRABA_TIP_AkrabaKod");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_AILE_BILGI_AKRABA_TIP_KodSistemAd");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_AILE_BILGI_AKRABA_TIP_KodSistemKod");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_AILE_BILGI_DOGUM_TARIH");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_AILE_BILGI_OLUM_TARIH");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_AILE_BILGI_YASAM_DURUM");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_GENETIKHASTALIKVARMI");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_GERCEKLESME_YASI");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_GERCEKLESME_YASISpecified");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_GIZLILIK_TIP_GizlilikAd");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_GIZLILIK_TIP_GizlilikKod");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_GIZLILIK_TIP_KodSistemAd");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_GIZLILIK_TIP_KodSistemKod");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_KESINLIK_DERECESI");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_TANI_KodSistemAd");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_TANI_KodSistemKod");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_TANI_TaniAd");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_TANI_TaniKod");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_TANI_TaniReferans");

            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_NEDEN");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_RISK");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_RISKSpecified");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_TANI_KodSistemAd");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_TANI_KodSistemKod");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_TANI_TaniAd");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_TANI_TaniReferans");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_YAS");
            dataschema.Columns.Add("HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_YASSpecified");

            dataschema.Columns.Add("HASTA_VELI_BILGI0_VELI_AD_SOYAD");
            dataschema.Columns.Add("HASTA_VELI_BILGI0_VELI_ADRES");
            dataschema.Columns.Add("HASTA_VELI_BILGI0_VELI_DURUM");
            dataschema.Columns.Add("HASTA_VELI_BILGI0_VELI_TELEFON");
            dataschema.Columns.Add("HASTA_VELI_BILGI0_VELI_TIP_AKRABA_AkrabaAd");
            dataschema.Columns.Add("HASTA_VELI_BILGI0_VELI_TIP_AKRABA_AkrabaKod");
            dataschema.Columns.Add("HASTA_VELI_BILGI0_VELI_TIP_AKRABA_KodSistemAd");
            dataschema.Columns.Add("HASTA_VELI_BILGI0_VELI_TIP_AKRABA_KodSistemKod");
            dataschema.Columns.Add("HASTA_VELI_BILGI0_VELI_TIP_ORGANIZASYON");

            for (int i = 0; i < HastaTckNo.Length; i++)
            {

                HASTAKAYITBILGISI hasta = mv.fHastaKimlikNoIle("P",
                      Current.AktifDoktor.TckNo.ToString(),
                      Current.AktifDoktor.TckNo.ToString(),
                      Current.AktifDoktor.WebServisSifre,
                      Current.AktifDoktor.Adi,
                      Current.AktifDoktor.Soyadi,
                      HastaTckNo[i],
                      HastaAd[i],
                      HastaSoyad[i],
                      out mesaj,
                      out sonuc);
                if (hasta == null)
                    continue;

                DataRow row = dataschema.NewRow();

                if (hasta.HASTA_ALERJI_BILGI != null && hasta.HASTA_ALERJI_BILGI.Length != 0)
                {
                    row["HASTA_ALERJI_BILGI0_ALERJI_ACIKLAMA"] = hasta.HASTA_ALERJI_BILGI[0].ALERJI_ACIKLAMA;
                    row["HASTA_ALERJI_BILGI0_ALERJI_ALERJI_TIP_AlerjiAd"] = hasta.HASTA_ALERJI_BILGI[0].ALERJI_TIP.AlerjiAd;
                    row["HASTA_ALERJI_BILGI0_ALERJI_ALERJI_TIP_AlerjiKod"] = hasta.HASTA_ALERJI_BILGI[0].ALERJI_TIP.AlerjiKod;
                    row["HASTA_ALERJI_BILGI0_ALERJI_ALERJI_TIP_KodSistemAd"] = hasta.HASTA_ALERJI_BILGI[0].ALERJI_TIP.KodSistemAd;
                    row["HASTA_ALERJI_BILGI0_ALERJI_ALERJI_TIP_KodSistemKod"] = hasta.HASTA_ALERJI_BILGI[0].ALERJI_TIP.KodSistemKod;
                    row["HASTA_ALERJI_BILGI0_ALERJI_ALERJIDEVAMEDIYORMU"] = hasta.HASTA_ALERJI_BILGI[0].ALERJIDEVAMEDIYORMU;

                }

                if (hasta.HASTA_BEBEKLIK_BILGI != null)
                {
                    if (hasta.HASTA_BEBEKLIK_BILGI.AGIRLIK_TIP != null)
                    {
                        row["HASTA_BEBEKLIK_BILGI_AGIRLIK_TIP_AgirlikBirimAd"] = hasta.HASTA_BEBEKLIK_BILGI.AGIRLIK_TIP.AgirlikBirimAd;
                        row["HASTA_BEBEKLIK_BILGI_AGIRLIK_TIP_AgirlikBirimKod"] = hasta.HASTA_BEBEKLIK_BILGI.AGIRLIK_TIP.AgirlikBirimKod;
                        row["HASTA_BEBEKLIK_BILGI_AGIRLIK_TIP_KodSistemAd"] = hasta.HASTA_BEBEKLIK_BILGI.AGIRLIK_TIP.KodSistemAd;
                        row["HASTA_BEBEKLIK_BILGI_AGIRLIK_TIP_KodSistemKod"] = hasta.HASTA_BEBEKLIK_BILGI.AGIRLIK_TIP.KodSistemKod;
                    }
                    row["HASTA_BEBEKLIK_BILGI_DOGUM_AGIRLIK"] = hasta.HASTA_BEBEKLIK_BILGI.DOGUM_AGIRLIK;
                    row["HASTA_BEBEKLIK_BILGI_DOGUM_BASCEVRE"] = hasta.HASTA_BEBEKLIK_BILGI.DOGUM_BASCEVRE;
                    row["HASTA_BEBEKLIK_BILGI_DOGUM_BOY"] = hasta.HASTA_BEBEKLIK_BILGI.DOGUM_BOY;
                    row["HASTA_BEBEKLIK_BILGI_DOGUM_KOMPLIKASYONU_VARMI"] = hasta.HASTA_BEBEKLIK_BILGI.DOGUM_KOMPLIKASYONU_VARMI;
                    row["HASTA_BEBEKLIK_BILGI_EK_GIDAYA_BASLAMA_AYI"] = hasta.HASTA_BEBEKLIK_BILGI.EK_GIDAYA_BASLAMA_AYI;
                    row["HASTA_BEBEKLIK_BILGI_FENIL_KAN_ALINDIMI"] = hasta.HASTA_BEBEKLIK_BILGI.FENIL_KAN_ALINDIMI;
                    row["HASTA_BEBEKLIK_BILGI_FENIL_UZUNLUK_BIRIM"] = hasta.HASTA_BEBEKLIK_BILGI.UZUNLUK_BIRIM;
                }

                if (hasta.HASTA_DURUM_BILGI != null)
                {
                    row["HASTA_DURUM_BILGI_HastaKayitDurumAd"] = hasta.HASTA_DURUM_BILGI.HastaKayitDurumAd;
                    row["HASTA_DURUM_BILGI_HastaKayitDurumKod"] = hasta.HASTA_DURUM_BILGI.HastaKayitDurumKod;
                    row["HASTA_DURUM_BILGI_KodSistemAd"] = hasta.HASTA_DURUM_BILGI.KodSistemAd;
                    row["HASTA_DURUM_BILGI_KodSistemKod"] = hasta.HASTA_DURUM_BILGI.KodSistemKod;

                }

                if (hasta.HASTA_ILETISIM_BILGI != null)
                {
                    if (hasta.HASTA_ILETISIM_BILGI.HASTA_ACIL_ILETISIM != null)
                    {
                        row["HASTA_ILETISIM_BILGI_HASTA_ACIL_ILETISIM0_ILETISIM_BILGILERI_DURUM"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ACIL_ILETISIM[0].ILETISIM_BILGILERI.DURUM;
                        row["HASTA_ILETISIM_BILGI_HASTA_ACIL_ILETISIM0_ILETISIM_BILGILERI_ILETISIM_DEGER"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ACIL_ILETISIM[0].ILETISIM_BILGILERI.ILETISIM_DEGER;
                        row["HASTA_ILETISIM_BILGI_HASTA_ACIL_ILETISIM0_ILETISIM_BILGILERI_ILETISIM_TIP_IletisimTipAd"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ACIL_ILETISIM[0].ILETISIM_BILGILERI.ILETISIM_TIP.IletisimTipAd;
                        row["HASTA_ILETISIM_BILGI_HASTA_ACIL_ILETISIM0_ILETISIM_BILGILERI_ILETISIM_TIP_IletisimTipKod"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ACIL_ILETISIM[0].ILETISIM_BILGILERI.ILETISIM_TIP.IletisimTipKod;
                        row["HASTA_ILETISIM_BILGI_HASTA_ACIL_ILETISIM0_ILETISIM_BILGILERI_ILETISIM_TIP_KodSistemAd"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ACIL_ILETISIM[0].ILETISIM_BILGILERI.ILETISIM_TIP.KodSistemAd;
                        row["HASTA_ILETISIM_BILGI_HASTA_ACIL_ILETISIM0_ILETISIM_BILGILERI_ILETISIM_TIP_KodSistemKod"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ACIL_ILETISIM[0].ILETISIM_BILGILERI.ILETISIM_TIP.KodSistemKod;
                        row["HASTA_ILETISIM_BILGI_HASTA_ACIL_ILETISIM0_KISI_AD_SOYAD"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ACIL_ILETISIM[0].KISI_AD_SOYAD;
                    }
                    if (hasta.HASTA_ILETISIM_BILGI.HASTA_ACIL_ILETISIM != null)
                    {
                        if (hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR != null)
                        {
                            if (hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR != null)
                            {
                                row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_IL_IlAd"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].IL.IlAd;
                                row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_IL_IlKod"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].IL.IlKod;
                                row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_IL_IlKodSpecified"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].IL.IlKodSpecified;
                                row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_IL_KodSistemAd"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].IL.KodSistemAd;
                                row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_IL_KodSistemKod"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].IL.KodSistemKod;
                                row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ILCE_IlceAd"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].ILCE.IlceAd;
                                row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ILCE_IlceKod"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].ILCE.IlceKod;
                                row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ILCE_IlceKodSpecified"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].ILCE.IlceKodSpecified;
                                row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ILCE_KodSistemAd"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].ILCE.KodSistemAd;
                                row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ILCE_KodSistemKod"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].ILCE.KodSistemKod;
                                row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_MAHALLE_KodSistemAd"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].MAHALLE.KodSistemAd;
                                row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_MAHALLE_KodSistemKod"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].MAHALLE.KodSistemKod;
                                row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_MAHALLE_MahalleAd"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].MAHALLE.MahalleAd;
                                row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_MAHALLE_MahalleKod"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].MAHALLE.MahalleKod;
                                if (hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].POSTA_KODU != null)
                                {
                                    row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_POSTA_KODU_KodSistemAd"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].POSTA_KODU.KodSistemAd;
                                    row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_POSTA_KODU_KodSistemKod"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].POSTA_KODU.KodSistemKod;
                                    row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_POSTA_KODU_PostaKoduAd"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].POSTA_KODU.PostaKoduAd;
                                    row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_POSTA_KODU_PostaKoduKod"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].POSTA_KODU.PostaKoduKod;
                                }
                                if (hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].ULKE != null)
                                {
                                    row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ULKE_KodSistemAd"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].ULKE.KodSistemAd;
                                    row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ULKE_KodSistemKod"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].ULKE.KodSistemKod;
                                    row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ULKE_UlkeAd"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].ULKE.UlkeAd;
                                    row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_ADR0_ULKE_UlkeKod"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR[0].ULKE.UlkeKod;
                                }
                            }
                        }
                    }
                    if (hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_TIP != null)
                    {
                        row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_TIP0_DURUM"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_TIP[0].DURUM;
                        row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_TIP0_ILETISIM_DEGER"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_TIP[0].ILETISIM_DEGER;
                        row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_TIP0_ILETISIM_TIP_IletisimTipAd"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_TIP[0].ILETISIM_TIP.IletisimTipAd;
                        row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_TIP0_ILETISIM_TIP_IletisimTipKod"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_TIP[0].ILETISIM_TIP.IletisimTipKod;
                        row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_TIP0_ILETISIM_TIP_KodSistemAd"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_TIP[0].ILETISIM_TIP.KodSistemAd;
                        row["HASTA_ILETISIM_BILGI_HASTA_ILETISIM_TIP0_ILETISIM_TIP_KodSistemKod"] = hasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_TIP[0].ILETISIM_TIP.KodSistemKod;
                    }

                }

                if (hasta.HASTA_KAYIT_KIMLIK_BILGI != null)
                {
                    row["HASTA_KAYIT_KIMLIK_BILGI_AILE_HEKIMI_AD"] = hasta.HASTA_KAYIT_KIMLIK_BILGI.AILE_HEKIMI.AD;
                    row["HASTA_KAYIT_KIMLIK_BILGI_AILE_HEKIMI_DIPLOMA_NO"] = hasta.HASTA_KAYIT_KIMLIK_BILGI.AILE_HEKIMI.DIPLOMA_NO;
                    row["HASTA_KAYIT_KIMLIK_BILGI_AILE_HEKIMI_SOYAD"] = hasta.HASTA_KAYIT_KIMLIK_BILGI.AILE_HEKIMI.SOYAD;
                    row["HASTA_KAYIT_KIMLIK_BILGI_AILE_HEKIMI_TCKIMLIK_NO"] = hasta.HASTA_KAYIT_KIMLIK_BILGI.AILE_HEKIMI.TCKIMLIK_NO;
                    row["HASTA_KAYIT_KIMLIK_BILGI_ANNE_AD"] = hasta.HASTA_KAYIT_KIMLIK_BILGI.ANNE_AD;
                    row["HASTA_KAYIT_KIMLIK_BILGI_BABA_AD"] = hasta.HASTA_KAYIT_KIMLIK_BILGI.BABA_AD;
                    row["HASTA_KAYIT_KIMLIK_BILGI_BEYAN_CINSIYET"] = hasta.HASTA_KAYIT_KIMLIK_BILGI.BEYAN_CINSIYET.CinsiyetAd;
                    row["HASTA_KAYIT_KIMLIK_BILGI_BEYAN_DOGUM_TARIHI"] = hasta.HASTA_KAYIT_KIMLIK_BILGI.BEYAN_DOGUM_TARIHI;
                    row["HASTA_KAYIT_KIMLIK_BILGI_BEYAN_CINSIYET"] = hasta.HASTA_KAYIT_KIMLIK_BILGI.CINSIYET.CinsiyetAd;
                    row["HASTA_KAYIT_KIMLIK_BILGI_DOGUM_TARIHI"] = hasta.HASTA_KAYIT_KIMLIK_BILGI.DOGUM_TARIHI;
                    row["HASTA_KAYIT_KIMLIK_BILGI_HASTAKIMLIK_AD"] = hasta.HASTA_KAYIT_KIMLIK_BILGI.HASTAKIMLIK.AD;
                    row["HASTA_KAYIT_KIMLIK_BILGI_HASTAKIMLIK_SOYAD"] = hasta.HASTA_KAYIT_KIMLIK_BILGI.HASTAKIMLIK.SOYAD;
                    row["HASTA_KAYIT_KIMLIK_BILGI_HASTAKIMLIK_TCKIMLIK_NO"] = hasta.HASTA_KAYIT_KIMLIK_BILGI.HASTAKIMLIK.TCKIMLIK_NO;
                    row["HASTA_KAYIT_KIMLIK_BILGI_ID"] = hasta.HASTA_KAYIT_KIMLIK_BILGI.ID;

                }


                if (hasta.HASTA_SOSYAL_EGITIM_BILGI != null)
                {
                    if (hasta.HASTA_SOSYAL_EGITIM_BILGI.EGITIM_DURUM != null)
                    {
                        row["HASTA_SOSYAL_EGITIM_BILGI_EGITIM_DURUM_EgitimDurumAd"] = hasta.HASTA_SOSYAL_EGITIM_BILGI.EGITIM_DURUM.EgitimDurumAd;
                        row["HASTA_SOSYAL_EGITIM_BILGI_EGITIM_DURUM_EgitimDurumKod"] = hasta.HASTA_SOSYAL_EGITIM_BILGI.EGITIM_DURUM.EgitimDurumKod;
                        row["HASTA_SOSYAL_EGITIM_BILGI_EGITIM_DURUM_KodSistemAd"] = hasta.HASTA_SOSYAL_EGITIM_BILGI.EGITIM_DURUM.KodSistemAd;
                        row["HASTA_SOSYAL_EGITIM_BILGI_EGITIM_DURUM_KodSistemKod"] = hasta.HASTA_SOSYAL_EGITIM_BILGI.EGITIM_DURUM.KodSistemKod;
                    }
                    row["HASTA_SOSYAL_EGITIM_BILGI_GEZICIHIZMETALIYORMU"] = hasta.HASTA_SOSYAL_EGITIM_BILGI.GEZICIHIZMETALIYORMU;
                    row["HASTA_SOSYAL_EGITIM_BILGI_KAN_GRUB"] = hasta.HASTA_SOSYAL_EGITIM_BILGI.KAN_GRUB;
                    if (hasta.HASTA_SOSYAL_EGITIM_BILGI.MEDENI_HAL != null)
                    {
                        row["HASTA_SOSYAL_EGITIM_BILGI_MEDENI_HAL_KodSistemAd"] = hasta.HASTA_SOSYAL_EGITIM_BILGI.MEDENI_HAL.KodSistemAd;
                        row["HASTA_SOSYAL_EGITIM_BILGI_MEDENI_HAL_KodSistemKod"] = hasta.HASTA_SOSYAL_EGITIM_BILGI.MEDENI_HAL.KodSistemKod;
                        row["HASTA_SOSYAL_EGITIM_BILGI_MEDENI_HAL_MedeniHalAd"] = hasta.HASTA_SOSYAL_EGITIM_BILGI.MEDENI_HAL.MedeniHalAd;
                        row["HASTA_SOSYAL_EGITIM_BILGI_MEDENI_HAL_MedeniHalKod"] = hasta.HASTA_SOSYAL_EGITIM_BILGI.MEDENI_HAL.MedeniHalKod;
                    }
                    row["HASTA_SOSYAL_EGITIM_BILGI_MESLEK"] = hasta.HASTA_SOSYAL_EGITIM_BILGI.MESLEK;
                    if (hasta.HASTA_SOSYAL_EGITIM_BILGI.SOSYAL_GUVENLIK_KURUM != null)
                    {
                        row["HASTA_SOSYAL_EGITIM_BILGI_SOSYAL_GUVENLIK_KURUM_KodSistemAd"] = hasta.HASTA_SOSYAL_EGITIM_BILGI.SOSYAL_GUVENLIK_KURUM.KodSistemAd;
                        row["HASTA_SOSYAL_EGITIM_BILGI_SOSYAL_GUVENLIK_KURUM_KodSistemKod"] = hasta.HASTA_SOSYAL_EGITIM_BILGI.SOSYAL_GUVENLIK_KURUM.KodSistemKod;
                        row["HASTA_SOSYAL_EGITIM_BILGI_SOSYAL_GUVENLIK_KURUM_SosyalGuvenlikKurumAd"] = hasta.HASTA_SOSYAL_EGITIM_BILGI.SOSYAL_GUVENLIK_KURUM.SosyalGuvenlikKurumAd;
                        row["HASTA_SOSYAL_EGITIM_BILGI_SOSYAL_GUVENLIK_KURUM_SosyalGuvenlikKurumKod"] = hasta.HASTA_SOSYAL_EGITIM_BILGI.SOSYAL_GUVENLIK_KURUM.SosyalGuvenlikKurumKod;
                    }
                    if (hasta.HASTA_SOSYAL_EGITIM_BILGI.UYRUGU != null)
                        row["HASTA_SOSYAL_EGITIM_BILGI_UYRUGU"] = hasta.HASTA_SOSYAL_EGITIM_BILGI.UYRUGU.UyrukAd;

                }

                if (hasta.HASTA_SOYGECMIS_BILGI != null)
                {
                    if (hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI != null)
                    {
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_ACIKLAMA"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].ACIKLAMA;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_AILE_BILGI_AKRABA_TIP_AkrabaAd"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].AILE_BILGI.AKRABA_TIP.AkrabaAd;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_AILE_BILGI_AKRABA_TIP_AkrabaKod"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].AILE_BILGI.AKRABA_TIP.AkrabaKod;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_AILE_BILGI_AKRABA_TIP_KodSistemAd"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].AILE_BILGI.AKRABA_TIP.KodSistemAd;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_AILE_BILGI_AKRABA_TIP_KodSistemKod"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].AILE_BILGI.AKRABA_TIP.KodSistemKod;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_AILE_BILGI_DOGUM_TARIH"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].AILE_BILGI.DOGUM_TARIH;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_AILE_BILGI_OLUM_TARIH"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].AILE_BILGI.OLUM_TARIH;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_AILE_BILGI_YASAM_DURUM"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].AILE_BILGI.YASAM_DURUM;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_GENETIKHASTALIKVARMI"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].GENETIKHASTALIKVARMI;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_GERCEKLESME_YASI"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].GERCEKLESME_YASI;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_GERCEKLESME_YASISpecified"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].GERCEKLESME_YASISpecified;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_GIZLILIK_TIP_GizlilikAd"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].GIZLILIK_TIP.GizlilikAd;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_GIZLILIK_TIP_GizlilikKod"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].GIZLILIK_TIP.GizlilikKod;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_GIZLILIK_TIP_KodSistemAd"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].GIZLILIK_TIP.KodSistemAd;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_GIZLILIK_TIP_KodSistemKod"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].GIZLILIK_TIP.KodSistemKod;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_KESINLIK_DERECESI"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].KESINLIK_DERECESI;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_TANI_KodSistemAd"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].TANI.KodSistemAd;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_TANI_KodSistemKod"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].TANI.KodSistemKod;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_TANI_TaniAd"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].TANI.TaniAd;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_TANI_TaniKod"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].TANI.TaniKod;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_HASTALIK_BILGI0_TANI_TaniReferans"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_HASTALIK_BILGI[0].TANI.TaniReferans;
                    }
                    if (hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_RISK_BILGI != null)
                    {
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_NEDEN"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_RISK_BILGI[0].NEDEN;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_RISK"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_RISK_BILGI[0].RISK;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_RISKSpecified"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_RISK_BILGI[0].RISKSpecified;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_TANI_KodSistemAd"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_RISK_BILGI[0].TANI.KodSistemAd;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_TANI_KodSistemKod"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_RISK_BILGI[0].TANI.KodSistemKod;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_TANI_TaniAd"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_RISK_BILGI[0].TANI.TaniAd;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_TANI_TaniKod"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_RISK_BILGI[0].TANI.TaniKod;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_TANI_TaniReferans"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_RISK_BILGI[0].TANI.TaniReferans;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_YAS"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_RISK_BILGI[0].YAS;
                        row["HASTA_SOYGECMIS_BILGI_HASTA_SOYGECMIS_RISK_BILGI0_YASSpecified"] = hasta.HASTA_SOYGECMIS_BILGI.HASTA_SOYGECMIS_RISK_BILGI[0].YASSpecified;
                    }

                }

                if (hasta.HASTA_VELI_BILGI != null && hasta.HASTA_VELI_BILGI.Length != 0)
                {
                    row["HASTA_VELI_BILGI0_VELI_AD_SOYAD"] = hasta.HASTA_VELI_BILGI[0].VELI_AD_SOYAD;
                    row["HASTA_VELI_BILGI0_VELI_ADRES"] = hasta.HASTA_VELI_BILGI[0].VELI_ADRES;
                    row["HASTA_VELI_BILGI0_VELI_DURUM"] = hasta.HASTA_VELI_BILGI[0].VELI_DURUM;
                    row["HASTA_VELI_BILGI0_VELI_TELEFON"] = hasta.HASTA_VELI_BILGI[0].VELI_TELEFON;
                    row["HASTA_VELI_BILGI0_VELI_TIP_AKRABA_AkrabaAd"] = hasta.HASTA_VELI_BILGI[0].VELI_TIP.AKRABA.AkrabaAd;
                    row["HASTA_VELI_BILGI0_VELI_TIP_AKRABA_AkrabaKod"] = hasta.HASTA_VELI_BILGI[0].VELI_TIP.AKRABA.AkrabaKod;
                    row["HASTA_VELI_BILGI0_VELI_TIP_AKRABA_KodSistemAd"] = hasta.HASTA_VELI_BILGI[0].VELI_TIP.AKRABA.KodSistemAd;
                    row["HASTA_VELI_BILGI0_VELI_TIP_AKRABA_KodSistemKod"] = hasta.HASTA_VELI_BILGI[0].VELI_TIP.AKRABA.KodSistemKod;
                    row["HASTA_VELI_BILGI0_VELI_TIP_ORGANIZASYON"] = hasta.HASTA_VELI_BILGI[0].VELI_TIP.ORGANIZASYON;

                }

                dataschema.Rows.Add(row);
            }

            return dataschema;

        }

        public static string[] DoktorSorgula(string hastatckno)
        {
            CIslemler islem = new CIslemler();
            int sonuc;
            string SaglikPersonelKimlikNo = "0";
            string SaglikPersonelAd;
            string SaglikPersonelSoyadi;
            string Lokasyon;

            string rcode = islem.fHastaHekimBilgisiGetir(
                "P",
                Current.AktifDoktor.TckNo.ToString(),
                Current.AktifDoktor.TckNo.ToString(),
                Current.AktifDoktor.WebServisSifre,
                Current.AktifDoktor.Adi,
                Current.AktifDoktor.Soyadi,
                DateTime.Today.ToString("yymmdd"),
                hastatckno.ToString(),
                out SaglikPersonelKimlikNo,
                out SaglikPersonelAd,
                out SaglikPersonelSoyadi,
                out Lokasyon,
                out sonuc
                );

            string[] result = new string[6];
            result[0] = SaglikPersonelKimlikNo;
            result[1] = SaglikPersonelAd;
            result[2] = SaglikPersonelSoyadi;
            result[3] = Lokasyon;
            result[4] = sonuc.ToString();
            result[5] = rcode;

            return result;

        }

        public static void TumKodlariGetirXMLeYaz()
        {
            CUtil myutil = new CUtil();
            Service service = new Service();
            string str;
            str = service.SistemKodunaGoreGetir("1572FCEE-2E3D-4500-9BA4-743BE9A581A7"); myutil.LogToFile("ICD10.xml", str);
            str = service.SistemKodunaGoreGetir("AF7BB2C3-3AEF-433A-BD0A-EA7416D3D586"); myutil.LogToFile("IlacKodlari.xml", str);
            str = service.SistemKodunaGoreGetir("f85fc6de-b865-4e83-a4c0-ab1c5a07422c"); myutil.LogToFile("ButTurleri.xml", str);
            str = service.SistemKodunaGoreGetir("6B3CA76A-D43D-46F0-9161-E298DB78ABE1"); myutil.LogToFile("ButKodlari.xml", str);
            str = service.SistemKodunaGoreGetir("5EE0AB29-6B92-4356-B287-6CF93E052362"); myutil.LogToFile("TetkikKodlari.xml", str);
            str = service.SistemKodunaGoreGetir("60729d0d-c272-4521-8ed7-652780c1f71d"); myutil.LogToFile("KurumTurleri.xml", str);
            str = service.SistemKodunaGoreGetir("c9dbe1cb-57cb-48fb-bdd3-d622e0e304c6"); myutil.LogToFile("Kurumlar.xml", str);
            str = service.SistemKodunaGoreGetir("43B06D1E-A7D2-4920-A4E7-6534F6C1D199"); myutil.LogToFile("Klinikler.xml", str);
            str = service.SistemKodunaGoreGetir("B5DDD47A-A01C-4944-8117-993A97ABFE0F"); myutil.LogToFile("KlinikDokumanTipi.xml", str);
            str = service.SistemKodunaGoreGetir("BD8C6F17-430B-4F90-83E2-E0276052384C"); myutil.LogToFile("Asi.xml", str);
            str = service.SistemKodunaGoreGetir("1c1ba2a9-01e1-46c7-8b38-44"); myutil.LogToFile("TakvimBebekIzlem.xml", str);
            str = service.SistemKodunaGoreGetir("377153f3-8de1-4515-9833-746bf81b041b"); myutil.LogToFile("TakvimAsi.xml", str);
            str = service.SistemKodunaGoreGetir("4259c680-ef30-4243-ac52-019c5a7e71ed"); myutil.LogToFile("TakvimGebeIzlem.xml", str);
            str = service.SistemKodunaGoreGetir("9416085f-6a12-470a-bc19-66ee19293768"); myutil.LogToFile("TakvimCocukIzlem.xml", str);
            str = service.SistemKodunaGoreGetir("ac0eea4a-3fbb-4946-ac43-543fcff3fdd8"); myutil.LogToFile("MalzemeKodlari.xml", str);
            str = service.SistemKodunaGoreGetir("ED350183-F2E8-415B-BCDB-1FA10EF627D8"); myutil.LogToFile("MudahaleKodlari.xml", str);
            str = service.SistemKodunaGoreGetir("43B06D1E-A7D2-4920-A4E7-6534F6C1D100"); myutil.LogToFile("UyrukKodlari.xml", str);
            str = service.SistemKodunaGoreGetir("ISO 3166"); myutil.LogToFile("Ulkeler.xml", str);
            str = service.SistemKodunaGoreGetir("c5a8d278-daa8-4774-a390-ab444e02db32"); myutil.LogToFile("Ulkeler2.xml", str);
            str = service.SistemKodunaGoreGetir("512d0cb3-d0b3-487c-ab1e-1343fc7ff611"); myutil.LogToFile("Meslekler.xml", str);
            str = service.SistemKodunaGoreGetir("526CB860-9DD3-4CDC-8888-C10135AFED4F"); myutil.LogToFile("ATCKodlari.xml", str);
            str = service.SistemKodunaGoreGetir("94e988ab-c1c8-46ea-af0d-465699607091"); myutil.LogToFile("AtcIlaclar.xml", str);
            str = service.SistemKodunaGoreGetir("47BA9D19-7639-4E83-8C2B-A81ED2F0F578"); myutil.LogToFile("IlacDozBirimleri.xml", str);
            str = service.SistemKodunaGoreGetir("A4F5E158-866F-42B4-95B5-358BF4B26389"); myutil.LogToFile("KanGrublari.xml", str);
            str = service.SistemKodunaGoreGetir("701829ce-43c2-4dee-bfa7-ae2609c11d66"); myutil.LogToFile("Parametreler.xml", str);
            str = service.SistemKodunaGoreGetir("6500cac4-fee0-507d-e044-00144f26688f"); myutil.LogToFile("MeslekHastaUyari.xml", str);
            str = service.SistemKodunaGoreGetir("928d1201-5dba-4a1b-9e0b-f00653e8af4b"); myutil.LogToFile("Uzmanliklar.xml", str);
            str = service.SistemKodunaGoreGetir("b1c79345-d10b-4073-8ccb-9c9d3498eecc"); myutil.LogToFile("OlasiTaniKriterleri.xml", str);
            str = service.SistemKodunaGoreGetir("7b4af4a7-8e6b-4382-b50b-60d1a37f6812"); myutil.LogToFile("EnfeksiyonEtkenliTaniKriterleri.xml", str);
            str = service.SistemKodunaGoreGetir("646bff5d-73da-4349-8769-05ed48b18020"); myutil.LogToFile("TumorYerleri.xml", str);
            str = service.SistemKodunaGoreGetir("3A15B5BA-FD09-41EE-9396-C8C8F5F8DFCC"); myutil.LogToFile("SosyalGuvenlikKurumu.xml", str);
            str = service.SistemKodunaGoreGetir("2AFE8407-6DEB-4E7D-A2FB-76ECEFCDA4DF"); myutil.LogToFile("OgrenimDurumu.xml", str);
            str = service.SistemKodunaGoreGetir("f8f58940-1a33-480e-8e90-7eb464215166"); myutil.LogToFile("YakinlikKodlari.xml", str);
            str = service.SistemKodunaGoreGetir("f2176c6d-87c0-40d5-af31-a5ebda73d31d"); myutil.LogToFile("KesinTaniKriterleri.xml", str);
            str = service.SistemKodunaGoreGetir("3d78fed3-66ff-444e-b9eb-4ed3f05ddb06"); myutil.LogToFile("Histoloji.xml", str);
            str = service.SistemKodunaGoreGetir("66D5BACE-C604-42BE-BE4A-5BD61A06C574"); myutil.LogToFile("MERNIS.xml", str);
            str = service.SistemKodunaGoreGetir("2.16.840.1.113883.6.1"); myutil.LogToFile("LOINCKODLARI.xml", str);
            str = service.SistemKodunaGoreGetir("41F3264B-D590-4BA8-8B22-4F138950F467"); myutil.LogToFile("ap_Yontemleri.xml", str);
            str = service.SistemKodunaGoreGetir("5AF472F6-9867-4183-8151-8F14564B0D16"); myutil.LogToFile("HastaKayitTalepTuru.xml", str);
            str = service.SistemKodunaGoreGetir("78F2300E-B242-43E3-B124-5B9BA2FD60B2"); myutil.LogToFile("HastaKayitDonusTuru.xml", str);
            str = service.SistemKodunaGoreGetir("742CEA72-08B0-4D76-A2F5-979676ED924F"); myutil.LogToFile("HastaKayitdurumu.xml", str);
            str = service.SistemKodunaGoreGetir("E31DF2DC-7F0A-468F-BA12-FFC2719C5298"); myutil.LogToFile("IlisikKesmenedenIzamanKisitli.xml", str);
            str = service.SistemKodunaGoreGetir("B8E5BF84-0D3D-4C50-8C57-E269CDAA8484"); myutil.LogToFile("IlisikKesmenedenIzamanKisitsiz.xml", str);
            str = service.SistemKodunaGoreGetir("0D38AC2D-696A-4FD4-BEEB-D707076B4F31"); myutil.LogToFile("Cinsiyet.xml", str);
            str = service.SistemKodunaGoreGetir("F72C59A7-70D5-4C62-B3E8-3B426521D605"); myutil.LogToFile("PelvisDurumu.xml", str);
            str = service.SistemKodunaGoreGetir("2C8695F0-8F96-430B-BC98-0400E0DB4F56"); myutil.LogToFile("GizlilikDerecesi.xml", str);
            str = service.SistemKodunaGoreGetir("5F2DFFE8-C99A-47E4-8252-93F334C85AE8"); myutil.LogToFile("GelisBicimi.xml", str);
            str = service.SistemKodunaGoreGetir("F95D11EE-F985-45C2-A94C-7F632358FCCF"); myutil.LogToFile("DogumYontemi.xml", str);
            str = service.SistemKodunaGoreGetir("6CB236DD-52D9-4337-8756-E5631D7E9B3D"); myutil.LogToFile("GebelikSonlanmaDurumu.xml", str);
            str = service.SistemKodunaGoreGetir("b781820f-483a-4de4-8934-788af91fa531"); myutil.LogToFile("IlacTedarikcileri.xml", str);
            str = service.SistemKodunaGoreGetir("C8F78916-96CD-43B5-810A-E56302F4B6E1"); myutil.LogToFile("MedeniHali.xml", str);
            str = service.SistemKodunaGoreGetir("017EC9A9-DA42-44B7-957A-3404E4DD9611"); myutil.LogToFile("YakinlikDerecesi.xml", str);
            str = service.SistemKodunaGoreGetir("4CAE078C-E236-4CEA-8DF6-38FDD9843789"); myutil.LogToFile("RaporTuru.xml", str);
            str = service.SistemKodunaGoreGetir("DAEEB685-593E-4774-99DE-C69C17DA6395"); myutil.LogToFile("DogumaYardimEden.xml", str);
            str = service.SistemKodunaGoreGetir("317BC05E-1FB1-4346-BAD4-B44F8062A8AC"); myutil.LogToFile("DogumunYapildigiYer.xml", str);
            str = service.SistemKodunaGoreGetir("B2F5264B-3C1C-4729-BB18-7546B496161A"); myutil.LogToFile("IletisimTipi.xml", str);
            str = service.SistemKodunaGoreGetir("CF5924B0-DF84-4438-BC93-597B4E71B3B1"); myutil.LogToFile("AdresTipi.xml", str);
            str = service.SistemKodunaGoreGetir("B3A8369F-4AE9-4D4D-A855-E5E1ADCCB0F4"); myutil.LogToFile("UzunlukBirimi.xml", str);
            str = service.SistemKodunaGoreGetir("30E2300A-218B-4009-AE46-908299201F1C"); myutil.LogToFile("AgirlikBirimi.xml", str);
            str = service.SistemKodunaGoreGetir("B052F8A2-B5C0-4609-BE3E-A096C42DC20B"); myutil.LogToFile("AlerjiKodlari.xml", str);
            str = service.SistemKodunaGoreGetir("ED6657E4-2A20-4BE3-B300-2AA043F40A0B"); myutil.LogToFile("IlacKullanimSekli.xml", str);
            str = service.SistemKodunaGoreGetir("3763F41D-9B20-45BD-8765-E7FF1789D7F3"); myutil.LogToFile("SaglikKurumlariDurumu.xml", str);
            str = service.SistemKodunaGoreGetir("2.16.840.1.113883.5.25"); myutil.LogToFile("HL7ClinicalDocument.xml", str);
            str = service.SistemKodunaGoreGetir("e469815c-5127-4ca1-ba75-3cae424dbb9c"); myutil.LogToFile("Adresler.xml", str);
            str = service.SistemKodunaGoreGetir("eb72bf5a-d70e-407e-9483-c4f9922743dc"); myutil.LogToFile("Persentil2.xml", str);
            str = service.SistemKodunaGoreGetir("BK62FA79-04AG-2EK0-1986-SU1B9A8T4443"); myutil.LogToFile("BedenKitleEndeksi.xml", str);
            str = service.SistemKodunaGoreGetir("2K26SAET-1YN0-9MCK-8MAK-38AA44ELNPBN"); myutil.LogToFile("BelCevresiSiniflandirmasi.xml", str);
            str = service.SistemKodunaGoreGetir("S34M4NBO-1030-3353-M44A-12AB45JK67PT"); myutil.LogToFile("BelKalcaOrani.xml", str);
        }

        public static HASTAKAYITBILGISI getBakanlikHastaBilgiDetay(string calismatur, long tckno, string adi, string soyadi)
        {
            int sonuc;
            string mesaj = "";
            CMvs mv = new CMvs();

            HASTAKAYITBILGISI result = mv.fHastaKimlikNoIle(calismatur,
                              Current.AktifDoktor.TckNo.ToString(),
                              Current.AktifDoktor.TckNo.ToString(),
                              Current.AktifDoktor.WebServisSifre,
                              Current.AktifDoktor.Adi,
                              Current.AktifDoktor.Soyadi,
                              tckno.ToString(),
                              adi,
                              soyadi,
                              out mesaj,
                              out sonuc);

            Current.globalresmessage = mesaj;
            Current.globalressonuc = sonuc;

            return result;
        }

        public static HASTA_KARTI getBakanlikHastaSonDurum(string calismatur, long tckno, string adi, string soyadi)
        {
            int sonuc;
            string mesaj = "";
            CHastaBilgi chb = new CHastaBilgi();

            HASTA_KARTI hk = chb.fHastaKarti(
                                  calismatur,
                                  Current.AktifDoktor.TckNo.ToString(),
                                  Current.AktifDoktor.TckNo.ToString(),
                                  Current.AktifDoktor.WebServisSifre,
                                  Current.AktifDoktor.Adi,
                                  Current.AktifDoktor.Soyadi,
                                  tckno.ToString(),
                                  adi,
                                  soyadi,
                                  0,
                                  out mesaj,
                                  out sonuc);

            Current.globalresmessage = mesaj;
            Current.globalressonuc = sonuc;

            return hk;
        }

        public static Hasta setBakanlikHastaToLocalHasta(HASTAKAYITBILGISI bakanlikhasta, Hasta localhasta, bool gezicimi)
        {
            try
            {
                if (Current.AktifDoktorId == 0)
                {
                    MessageBox.Show("Aktif Doktor belirsiz devam edemezsiniz.", "Hata");
                    return null;
                }

                Cursor.Current = Cursors.WaitCursor;

                Hasta hasta = new Hasta();
                if (localhasta != null)
                    hasta = localhasta;

                hasta.Doktor.Id = Current.AktifDoktorId;
                hasta.KayitDurumu = myenum.KayitDurumu.Kayitli;
                hasta.KayitKimlikDurumu = myenum.KayitKimlikDurumu.TckNoVar;
                hasta.Adi = bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.HASTAKIMLIK.AD;
                hasta.Soyadi = bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.HASTAKIMLIK.SOYAD;
                hasta.TckNo = Convert.ToInt64(bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.HASTAKIMLIK.TCKIMLIK_NO);
                hasta.Id = hasta.TckNo;
                hasta.Aktif = true;
                hasta.AnneAdi = bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.ANNE_AD;
                hasta.BabaAdi = bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.BABA_AD;
                string dt = bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.BEYAN_DOGUM_TARIHI;
                hasta.BeyanDogumTarihi = Convert.ToDateTime(dt.Substring(0, 4) + "." + dt.Substring(4, 2) + "." + dt.Substring(6, 2));
                dt = bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.DOGUM_TARIHI;
                hasta.DogumTarihi = Convert.ToDateTime(dt.Substring(0, 4) + "." + dt.Substring(4, 2) + "." + dt.Substring(6, 2));
                hasta.PasaportNo = "";
                hasta.Resim = new byte[1];
                hasta.Resim[0] = 1;
                hasta.GeziciHizmetVerilenHasta = gezicimi;

                #region iletişim bilgileri
                if (bakanlikhasta.HASTA_ILETISIM_BILGI != null)
                {
                    if (bakanlikhasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_TIP!=null)
                        if (bakanlikhasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_TIP.Length>0)
                            if (bakanlikhasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_TIP[0].ILETISIM_TIP!=null)
                                hasta.IletisimTip = (myenum.IletisimTip)Convert.ToInt32(bakanlikhasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_TIP[0].ILETISIM_TIP.IletisimTipKod);
                    if (bakanlikhasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR!=null)
                        if (bakanlikhasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR.Length > 0)
                        {
                            foreach (var item in bakanlikhasta.HASTA_ILETISIM_BILGI.HASTA_ILETISIM_ADR)
                            {
                                if (item.ADRES_TIP.IletisimAdresKod==((int)myenum.AdresTip.EvAdresi).ToString())
                                {
                                    hasta.LokasyonAdresText = item.ADRES_ACIK;
                                    if (item.IL!=null)
                                    {
                                        Lokasyon lil=new Lokasyon();
                                        lil.Id=Convert.ToInt64(item.IL.IlKod);
                                        hasta.LokasyonSehir = lil;
                                    }
                                    if (item.ILCE != null)
                                    {
                                        Lokasyon lilce = new Lokasyon();
                                        lilce.Id = Convert.ToInt64(item.ILCE.IlceKod);
                                        hasta.Lokasyonilce = lilce;
                                    }
                                    if (item.MAHALLE != null)
                                    {
                                        Lokasyon lm = new Lokasyon();
                                        lm.Id = Convert.ToInt64(item.MAHALLE.MahalleKod);
                                        hasta.LokasyonMahalle = lm;
                                    }
                                    if (item.ULKE != null)
                                    {
                                        Ulke lu = new Ulke();
                                        lu.Id = Convert.ToInt64(item.ULKE.UlkeKod);
                                        hasta.Ulke = lu;
                                    }
                                }
                                else
                                if (item.ADRES_TIP.IletisimAdresKod == ((int)myenum.AdresTip.IsAdresi).ToString())
                                {
                                    hasta.LokasyonAdresText1 = item.ADRES_ACIK;
                                    if (item.IL != null)
                                    {
                                        Lokasyon lil = new Lokasyon();
                                        lil.Id = Convert.ToInt64(item.IL.IlKod);
                                        hasta.LokasyonSehir1 = lil;
                                    }
                                    if (item.ILCE != null)
                                    {
                                        Lokasyon lilce = new Lokasyon();
                                        lilce.Id = Convert.ToInt64(item.ILCE.IlceKod);
                                        hasta.Lokasyonilce1 = lilce;
                                    }
                                    if (item.MAHALLE != null)
                                    {
                                        Lokasyon lm = new Lokasyon();
                                        lm.Id = Convert.ToInt64(item.MAHALLE.MahalleKod);
                                        hasta.LokasyonMahalle1 = lm;
                                    }
                                }
                            }
                        }
                }
                #endregion iletişim bilgileri


                if (bakanlikhasta.HASTA_SOSYAL_EGITIM_BILGI.KAN_GRUB != null)
                    hasta.KanGrubu = (myenum.KanGrubu)Convert.ToInt32(bakanlikhasta.HASTA_SOSYAL_EGITIM_BILGI.KAN_GRUB.KanGrubuKod);

                if (bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.BEYAN_CINSIYET.CinsiyetAd == "Erkek")
                    hasta.BeyanCinsiyeti = myenum.Cinsiyet.Erkek;
                else
                    if (bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.BEYAN_CINSIYET.CinsiyetAd == "Kadın")
                        hasta.BeyanCinsiyeti = myenum.Cinsiyet.Kadın;

                if (bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.CINSIYET.CinsiyetAd == "Erkek")
                    hasta.Cinsiyeti = myenum.Cinsiyet.Erkek;
                else
                    if (bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.CINSIYET.CinsiyetAd == "Kadın")
                        hasta.Cinsiyeti = myenum.Cinsiyet.Kadın;
                    else
                        if (bakanlikhasta.HASTA_KAYIT_KIMLIK_BILGI.CINSIYET.CinsiyetAd == "Belirsiz")
                            hasta.Cinsiyeti = myenum.Cinsiyet.Belirsiz;

                if (null != bakanlikhasta.HASTA_SOSYAL_EGITIM_BILGI.SOSYAL_GUVENLIK_KURUM)
                    hasta.KurumTipi = (myenum.SosyalGuvenlikKurumTipi)Convert.ToInt32(bakanlikhasta.HASTA_SOSYAL_EGITIM_BILGI.SOSYAL_GUVENLIK_KURUM.SosyalGuvenlikKurumKod);

                if (null != bakanlikhasta.HASTA_SOSYAL_EGITIM_BILGI.MEDENI_HAL)
                    hasta.MedeniHali = (myenum.MedeniHali)Convert.ToInt32(bakanlikhasta.HASTA_SOSYAL_EGITIM_BILGI.MEDENI_HAL.MedeniHalKod);


                hasta.TransferDurumu = myenum.TransferDurumu.Gonderildi;
                hasta.TransferSonuc = "Bakanlıktan Bilgiler Başarıyla güncellendi.";
                hasta.TransferTarihi = DateTime.Now;

                return hasta;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public static TOPLUMUAYENELISTE getBakanlikHastaMuayeneIzlem(string calismatur, long tckno, string adi, string soyadi)
        {
            int sonuc;
            string mesaj = "";
            CHastaBilgi chb = new CHastaBilgi();

            TOPLUMUAYENELISTE tm = chb.fSonNMuayeneListe(
                                  calismatur,
                                  Current.AktifDoktor.TckNo.ToString(),
                                  Current.AktifDoktor.TckNo.ToString(),
                                  Current.AktifDoktor.WebServisSifre,
                                  Current.AktifDoktor.Adi,
                                  Current.AktifDoktor.Soyadi,
                                  tckno.ToString(),
                                  adi,
                                  soyadi,
                                  out mesaj,
                                  out sonuc);


            Current.globalresmessage = mesaj;
            Current.globalressonuc = sonuc;

            return tm;
        }
       
        public static void setBakanlikMuayeneIzlemToLocalMuayeneIzlem(TOPLUMUAYENELISTE tm, Hasta localhasta)
        {
            try
            {
                if (Current.AktifDoktorId == 0)
                {
                    MessageBox.Show("Aktif Doktor belirsiz devam edemezsiniz.", "Hata");
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                try
                {
                    #region kadın izlem
                    if (tm.KADIN_IZLEM_LISTE != null)
                        foreach (var item in tm.KADIN_IZLEM_LISTE)
                        {
                            if (item.BILESEN != null)
                                if (item.BILESEN.DOKUMAN_ICERIK_TIPI != null)
                                    if (item.BILESEN.DOKUMAN_ICERIK_TIPI.Length > 0)
                                        if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM != null)
                                            if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM.Length > 0)
                                                if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN != null)
                                                    if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN.Length > 0)
                                                        if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN != null)
                                                            if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN.Length > 0)
                                                                if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].KADIN_IZLEM_BILGISI != null)
                                                                {
                                                                    KadinIzleme ki = new KadinIzleme();
                                                                    var bki = item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].KADIN_IZLEM_BILGISI;
                                                                    ki.DogumKontrolDanismanligiAldi = bki.AP_DANISMANLIGI == "1";
                                                                    ki.CanliDogumAdedi = (byte)bki.CANLI_DOGUM_SAYISI;
                                                                    if (bki.DOGUM_KONTROL_YONTEMI != null)
                                                                        ki.KadinKorunmaYontemi = (myenum.KadinKorunmaYontemi)Convert.ToInt32(bki.DOGUM_KONTROL_YONTEMI.LoincKod);
                                                                    ki.DusukDogumAdedi = (byte)bki.DUSUK_DOGUM_SAYISI;
                                                                    ki.EvlilikYasi = bki.EVLENME_YASI;
                                                                    ki.IlkGebelikYasi = bki.ILK_GEBELIK_YASI;
                                                                    ki.KonjAnomali = bki.KONJ_ANOMALI == "1";
                                                                    ki.OluDogumAdedi = (byte)bki.OLU_DOGUM_SAYISI;
                                                                    ki.ServikalSmear = bki.SERVIKAL_SMEAR == "1";

                                                                    ki.Doktor = localhasta.Doktor;
                                                                    ki.Hasta = localhasta;
                                                                    ki.Id = Convert.ToInt64(item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].ID.PROTOKOL_NO);
                                                                    ki.Aktif = true;
                                                                    ki.TransferDurumu = (int)myenum.TransferDurumu.Gonderildi;
                                                                    ki.TransferSonuc = item.SONUC.ToString();
                                                                    string mystr = item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].ID.PROTOKOL_NO;
                                                                    if (mystr.Length == 13)
                                                                        mystr = "2" + mystr;
                                                                    if (mystr.Length == 12)
                                                                        mystr = "20" + mystr;
                                                                    ki.IzlemTarihi = DateTime.ParseExact(mystr, "yyyyMMddHHmmss", new System.Globalization.DateTimeFormatInfo());
                                                                    ki.TransferTarihi = ki.IzlemTarihi;
                                                                    ki.IsAutoImport = true;

                                                                    ki.Delete();
                                                                    ki.Insert();
                                                                }
                        }
                    #endregion kadın izlem

                    #region gebe baslangic
                    if (tm.GEBE_BILDIRIM_LISTE != null)
                        foreach (var item in tm.GEBE_BILDIRIM_LISTE)
                        {
                            if (item.BILESEN != null)
                                if (item.BILESEN.DOKUMAN_ICERIK_TIPI != null)
                                    if (item.BILESEN.DOKUMAN_ICERIK_TIPI.Length > 0)
                                        if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM != null)
                                            if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM.Length > 0)
                                                if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN != null)
                                                    if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN.Length > 0)
                                                        if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN != null)
                                                            if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN.Length > 0)
                                                                if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].GEBELIK_BILDIRIMI != null)
                                                                {
                                                                    var gbb = item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].GEBELIK_BILDIRIMI;
                                                                    GebeBaslangic gb = new GebeBaslangic();
                                                                    gb.BeslenmeDanismanligiAldimi = gbb.GEBELIK_EK_BILGI.BESLENME_DANISMANLIGI_ALDI == "1";
                                                                    gb.DemirDestegiAldimi = gbb.GEBELIK_EK_BILGI.DEMIR_DESTEGI_ALDI == "1";
                                                                    gb.TetanozBagisikligiVarmi = gbb.GEBELIK_EK_BILGI.TETANOS_BAGISIKLIGI == "1";
                                                                    gb.GebelikOncesiSistemikHastalik = gbb.GEBELIK_EK_BILGI.GEBELIK_ONCESI_SISTEMIK_HASTALIK == "1";
                                                                    if (gbb.GEBELIK_EK_BILGI.DOGUM_KONTROL_YONTEMI != null)
                                                                        gb.KadinKorunmaYontemi = (myenum.KadinKorunmaYontemi)Convert.ToInt32(gbb.GEBELIK_EK_BILGI.DOGUM_KONTROL_YONTEMI.LoincKod);
                                                                    gb.AkrabaEvliligiVarmi = gbb.GEBELIK_BILGISI.AKRABA_EVLILIGI_VAR == "1";
                                                                    gb.SonAdetTarihi = DateTime.ParseExact(gbb.GEBELIK_BILGISI.SON_ADET_TARIHI, "yyyyMMdd", new System.Globalization.DateTimeFormatInfo());
                                                                    gb.GebelikNo = (byte)gbb.GEBELIK_BILGISI.KACINCI_GEBELIGI;
                                                                    if (gbb.GEBELIK_BILGISI.ESININ_KAN_GRUBU != null)
                                                                        if (gbb.GEBELIK_BILGISI.ESININ_KAN_GRUBU.DEGER != null)
                                                                        {
                                                                            if (gbb.GEBELIK_BILGISI.ESININ_KAN_GRUBU.DEGER.Contains("0") && gbb.GEBELIK_BILGISI.ESININ_KAN_GRUBU.DEGER.Contains("+"))
                                                                                gb.EsininKanGrubu = myenum.KanGrubu.O_RH_Pozitif;
                                                                            else
                                                                                if (gbb.GEBELIK_BILGISI.ESININ_KAN_GRUBU.DEGER.Contains("0") && gbb.GEBELIK_BILGISI.ESININ_KAN_GRUBU.DEGER.Contains("-"))
                                                                                    gb.EsininKanGrubu = myenum.KanGrubu.O_RH_Negatif;
                                                                                else
                                                                                    if (gbb.GEBELIK_BILGISI.ESININ_KAN_GRUBU.DEGER.Contains("AB") && gbb.GEBELIK_BILGISI.ESININ_KAN_GRUBU.DEGER.Contains("+"))
                                                                                        gb.EsininKanGrubu = myenum.KanGrubu.AB_RH_Pozitif;
                                                                                    else
                                                                                        if (gbb.GEBELIK_BILGISI.ESININ_KAN_GRUBU.DEGER.Contains("AB") && gbb.GEBELIK_BILGISI.ESININ_KAN_GRUBU.DEGER.Contains("-"))
                                                                                            gb.EsininKanGrubu = myenum.KanGrubu.AB_RH_Negatif;
                                                                                        else
                                                                                            if (gbb.GEBELIK_BILGISI.ESININ_KAN_GRUBU.DEGER.Contains("A ") && gbb.GEBELIK_BILGISI.ESININ_KAN_GRUBU.DEGER.Contains("+"))
                                                                                                gb.EsininKanGrubu = myenum.KanGrubu.A_RH_Pozitif;
                                                                                            else
                                                                                                if (gbb.GEBELIK_BILGISI.ESININ_KAN_GRUBU.DEGER.Contains("A ") && gbb.GEBELIK_BILGISI.ESININ_KAN_GRUBU.DEGER.Contains("-"))
                                                                                                    gb.EsininKanGrubu = myenum.KanGrubu.A_RH_Negatif;
                                                                                                else
                                                                                                    if (gbb.GEBELIK_BILGISI.ESININ_KAN_GRUBU.DEGER.Contains("B ") && gbb.GEBELIK_BILGISI.ESININ_KAN_GRUBU.DEGER.Contains("+"))
                                                                                                        gb.EsininKanGrubu = myenum.KanGrubu.B_RH_Pozitif;
                                                                                                    else
                                                                                                        if (gbb.GEBELIK_BILGISI.ESININ_KAN_GRUBU.DEGER.Contains("B ") && gbb.GEBELIK_BILGISI.ESININ_KAN_GRUBU.DEGER.Contains("-"))
                                                                                                            gb.EsininKanGrubu = myenum.KanGrubu.B_RH_Negatif;
                                                                        }
                                                                    if (gbb.GEBELIK_BILGISI.AKRABALIK_DERECESI != null && gbb.GEBELIK_BILGISI.AKRABALIK_DERECESI != "Belirsiz")
                                                                        gb.EsininAkrabalikDerecesi = (myenum.AkrabalikDerece)Convert.ToInt32(gbb.GEBELIK_BILGISI.AKRABALIK_DERECESI);

                                                                    gb.Doktor = localhasta.Doktor;
                                                                    gb.Hasta = localhasta;
                                                                    gb.Id = Convert.ToInt64(item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].ID.PROTOKOL_NO);
                                                                    gb.Aktif = true;
                                                                    gb.TransferDurumu = (int)myenum.TransferDurumu.Gonderildi;
                                                                    gb.TransferSonuc = item.SONUC.ToString();
                                                                    string mystr = item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].ID.PROTOKOL_NO;
                                                                    if (mystr.Length == 13)
                                                                        mystr = "2" + mystr;
                                                                    if (mystr.Length == 12)
                                                                        mystr = "20" + mystr;
                                                                    gb.IzlemTarihi = DateTime.ParseExact(mystr, "yyyyMMddHHmmss", new System.Globalization.DateTimeFormatInfo());
                                                                    gb.TransferTarihi = gb.IzlemTarihi;
                                                                    gb.IsAutoImport = true;

                                                                    gb.Delete();
                                                                    gb.Insert();
                                                                }
                        }
                    #endregion gebe baslangic

                    #region gebe izlem
                    if (tm.GEBE_IZLEM_LISTE != null)
                        foreach (var item in tm.GEBE_IZLEM_LISTE)
                        {
                            if (item.BILESEN != null)
                                if (item.BILESEN.DOKUMAN_ICERIK_TIPI != null)
                                    if (item.BILESEN.DOKUMAN_ICERIK_TIPI.Length > 0)
                                        if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM != null)
                                            if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM.Length > 0)
                                                if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN != null)
                                                    if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN.Length > 0)
                                                    {
                                                        int gsay = 0;
                                                        GebeIzleme gb = new GebeIzleme();
                                                        gb.Doktor = localhasta.Doktor;
                                                        gb.Hasta = localhasta;
                                                        gb.Id = Convert.ToInt64(item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].ID.PROTOKOL_NO);
                                                        gb.Aktif = true;
                                                        gb.TransferDurumu = (int)myenum.TransferDurumu.Gonderildi;
                                                        gb.TransferSonuc = item.SONUC.ToString();
                                                        string mystr = item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].ID.PROTOKOL_NO;
                                                        if (mystr.Length == 13)
                                                            mystr = "2" + mystr;
                                                        if (mystr.Length == 12)
                                                            mystr = "20" + mystr;
                                                        gb.IzlemTarihi = DateTime.ParseExact(mystr, "yyyyMMddHHmmss", new System.Globalization.DateTimeFormatInfo());
                                                        gb.TransferTarihi = gb.IzlemTarihi;
                                                        gb.IsAutoImport = true;
                                                        foreach (var itemm in item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN)
                                                        {
                                                            gsay++;
                                                            if (itemm.MYN != null)
                                                                if (itemm.MYN.Length > 0)
                                                                    if (itemm.MYN[0] != null)
                                                                    {
                                                                        if (gsay == 1 && itemm.MYN[0].DEGER.DEGER != "")
                                                                            gb.Agirligi = Convert.ToInt16(itemm.MYN[0].DEGER.DEGER);
                                                                        else
                                                                            if (gsay == 2 && itemm.MYN[0].DEGER.DEGER != "" && itemm.MYN[0].DEGER.DEGER != "0,0")
                                                                                gb.CocukKalpSesiAdedi = Convert.ToByte(itemm.MYN[0].DEGER.DEGER);
                                                                            else
                                                                                if (gsay == 3 && itemm.MYN[0].DEGER.DEGER != "")
                                                                                    gb.Hemoglobin = Convert.ToDecimal(itemm.MYN[0].DEGER.DEGER);
                                                                                else
                                                                                    if (gsay == 4 && itemm.MYN[0].DEGER.DEGER != "")
                                                                                    {
                                                                                        gb.idrardaProteinVarmi = 0 < Convert.ToDecimal(itemm.MYN[0].DEGER.DEGER);
                                                                                        //gb.idrardaProtein = (myenum.IdrardaProteinDurumu)Convert.ToInt32(itemm.MYN[0].DEGER.DEGER);
                                                                                    }
                                                                                    else
                                                                                        if (gsay == 5)
                                                                                            gb.KanBasinci = itemm.MYN[0].DEGER.DEGER.ToString();
                                                                                        else
                                                                                            if (gsay == 6 && itemm.MYN[0].DEGER.DEGER != "")
                                                                                                gb.Nabiz = Convert.ToByte(itemm.MYN[0].DEGER.DEGER);
                                                                                            else
                                                                                                if (gsay == 7)
                                                                                                    gb.TetanozAsisiYapildi = itemm.MYN[0].DEGER.DEGER == "1";
                                                                    }
                                                        }
                                                        gb.Delete();
                                                        gb.Insert();
                                                    }

                        }
                    #endregion gebe izlem

                    #region gebe sonlandırma
                    if (tm.GEBE_SONLANDIRMA_LISTE != null)
                        foreach (var item in tm.GEBE_SONLANDIRMA_LISTE)
                        {
                            if (item.BILESEN != null)
                                if (item.BILESEN.DOKUMAN_ICERIK_TIPI != null)
                                    if (item.BILESEN.DOKUMAN_ICERIK_TIPI.Length > 0)
                                        if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM != null)
                                            if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM.Length > 0)
                                                if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN != null)
                                                    if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN.Length > 0)
                                                        if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN != null)
                                                            if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN.Length > 0)
                                                            {
                                                                var gbb = item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].GEBELIK_SON_BILGISI;
                                                                GebeSonuc gb = new GebeSonuc();
                                                                gb.CanliDogumAdedi = (byte)gbb.CANLI_BEBEK_SAYISI;
                                                                gb.DogumunYapildigiYer = (myenum.DogumunYapildigiYer)Convert.ToInt32(gbb.DOGUM_YERI.DOGUMYERITIPKOD);
                                                                gb.DogumYontemi = (myenum.DogumYontemi)Convert.ToInt32(gbb.DOGUM_YONTEMI.DOGUMYONTEMIKOD);
                                                                gb.DogumaYardimEden = (myenum.DogumaYardimEden)Convert.ToInt32(gbb.DOGUMA_YARDIMCI.DOGUMAYARDIMCIKOD);
                                                                gb.GebelikSonucu = (myenum.GebelikSonucu)Convert.ToInt32(gbb.GEBELIK_SONUCU.GEBELIKSONLANMAKOD);

                                                                gb.Doktor = localhasta.Doktor;
                                                                gb.Hasta = localhasta;
                                                                gb.Id = Convert.ToInt64(item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].ID.PROTOKOL_NO);
                                                                gb.Aktif = true;
                                                                gb.TransferDurumu = (int)myenum.TransferDurumu.Gonderildi;
                                                                gb.TransferSonuc = item.SONUC.ToString();
                                                                string mystr = item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].ID.PROTOKOL_NO;
                                                                if (mystr.Length == 13)
                                                                    mystr = "2" + mystr;
                                                                if (mystr.Length == 12)
                                                                    mystr = "20" + mystr;
                                                                gb.IzlemTarihi = DateTime.ParseExact(mystr, "yyyyMMddHHmmss", new System.Globalization.DateTimeFormatInfo());
                                                                gb.TransferTarihi = gb.IzlemTarihi;
                                                                gb.IsAutoImport = true;

                                                                gb.Delete();
                                                                gb.Insert();
                                                            }
                        }
                    #endregion gebe sonlandırma

                    #region lohusa izlem
                    if (tm.LOHUSA_IZLEM_LISTE != null)
                        foreach (var item in tm.LOHUSA_IZLEM_LISTE)
                        {
                            if (item.BILESEN != null)
                                if (item.BILESEN.DOKUMAN_ICERIK_TIPI != null)
                                    if (item.BILESEN.DOKUMAN_ICERIK_TIPI.Length > 0)
                                        if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM != null)
                                            if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM.Length > 0)
                                                if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN != null)
                                                    if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN.Length > 0)
                                                        if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN != null)
                                                            if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN.Length > 0)
                                                                if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].LOHUSA_IZLEM_BILGISI != null)
                                                                    if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].LOHUSA_IZLEM_BILGISI.LOHUSA_EK_BILGI != null)
                                                                    {
                                                                        var bli = item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].LOHUSA_IZLEM_BILGISI.LOHUSA_EK_BILGI;
                                                                        LohusaIzleme li = new LohusaIzleme();
                                                                        li.BeslenmeDanismanligiAldimi = bli.BESLENME_DANISMANLIGI_ALDI == "1";
                                                                        li.DemirDestegiAldimi = bli.DEMIR_DESTEGI_ALDI == "1";
                                                                        li.EmzirmeDanismanligiAldimi = bli.EMZIRME_DANISMANLIGI_ALDI == "1";
                                                                        li.BebekDogumKomplikasyonVarmi = bli.KOMPLIKASYON_VARMI == "1";

                                                                        li.Doktor = localhasta.Doktor;
                                                                        li.Hasta = localhasta;
                                                                        li.Id = Convert.ToInt64(item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].ID.PROTOKOL_NO);
                                                                        li.Aktif = true;
                                                                        li.TransferDurumu = (int)myenum.TransferDurumu.Gonderildi;
                                                                        li.TransferSonuc = item.SONUC.ToString();
                                                                        string mystr = item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].ID.PROTOKOL_NO;
                                                                        if (mystr.Length == 13)
                                                                            mystr = "2" + mystr;
                                                                        if (mystr.Length == 12)
                                                                            mystr = "20" + mystr;
                                                                        li.IzlemTarihi = DateTime.ParseExact(mystr, "yyyyMMddHHmmss", new System.Globalization.DateTimeFormatInfo());
                                                                        li.TransferTarihi = li.IzlemTarihi;
                                                                        li.IsAutoImport = true;

                                                                        li.Delete();
                                                                        li.Insert();
                                                                    }
                        }
                    #endregion lohusa izlem

                    #region bebek izlem
                    if (tm.BEBEK_IZLEM_LISTE != null)
                        foreach (var item in tm.BEBEK_IZLEM_LISTE)
                        {
                            if (item.BILESEN != null)
                                if (item.BILESEN.DOKUMAN_ICERIK_TIPI != null)
                                    if (item.BILESEN.DOKUMAN_ICERIK_TIPI.Length > 0)
                                        if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM != null)
                                            if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM.Length > 0)
                                                if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN != null)
                                                    if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN.Length > 0)
                                                        if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN != null)
                                                        {
                                                            if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN.Length > 0)
                                                                if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].BEBEK_COCUK_IZLEM_BILGISI != null)
                                                                    if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].BEBEK_COCUK_IZLEM_BILGISI.BEBEK_COCUK_IZLEM_KAYDI != null)
                                                                    {
                                                                        var bli = item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].BEBEK_COCUK_IZLEM_BILGISI.BEBEK_COCUK_IZLEM_KAYDI;
                                                                        BebekIzleme li = new BebekIzleme();
                                                                        li.FenilKetonuriIcinKanAlindimi = bli.BEBEK_DOGUM_FENIL_KAN_ALINDI == "1";
                                                                        li.BebekDogumKomplikasyonVarmi = bli.BEBEK_DOGUM_KOMPLIKASYON == "1";
                                                                        li.Agirligi = (int)bli.BEBEK_AGIRLIK.AGIRLIK_DEGER;
                                                                        li.Boyu = (byte)bli.BEBEK_BASCEVRE_BOY_UZUNLUK.BOY_UZUNLUK_DEGER;
                                                                        li.BasCevresi = (byte)bli.BEBEK_BASCEVRE_BOY_UZUNLUK.BASCEVRE_UZUNLUK_DEGER;
                                                                        li.DogumAgirligi = (int)bli.BEBEK_DOGUM_AGIRLIK.AGIRLIK_DEGER;
                                                                        li.DogumBoyu = (byte)bli.BEBEK_DOGUM_BASCEVRE_BOY_UZUNLUK.BOY_UZUNLUK_DEGER;
                                                                        li.DogumBasCevresi = (byte)bli.BEBEK_DOGUM_BASCEVRE_BOY_UZUNLUK.BASCEVRE_UZUNLUK_DEGER;
                                                                        li.EkGidaBaslamaAy = (byte)bli.BEBEK_DOGUM_EKGIDA_BASLAMA_AY;


                                                                        li.Doktor = localhasta.Doktor;
                                                                        li.Hasta = localhasta;
                                                                        li.Id = Convert.ToInt64(item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].ID.PROTOKOL_NO);
                                                                        li.Aktif = true;
                                                                        li.TransferDurumu = (int)myenum.TransferDurumu.Gonderildi;
                                                                        li.TransferSonuc = item.SONUC.ToString();
                                                                        string mystr = item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[0].BILESEN[0].MYN[0].ID.PROTOKOL_NO;
                                                                        if (mystr.Length == 13)
                                                                            mystr = "2" + mystr;
                                                                        if (mystr.Length == 12)
                                                                            mystr = "20" + mystr;
                                                                        li.IzlemTarihi = DateTime.ParseExact(mystr, "yyyyMMddHHmmss", new System.Globalization.DateTimeFormatInfo());
                                                                        li.TransferTarihi = li.IzlemTarihi;
                                                                        li.IsAutoImport = true;

                                                                        li.Delete();
                                                                        li.Insert();
                                                                    }
                                                        }
                            if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM.Length > 1)
                                if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[1].BILESEN != null)
                                {
                                    #region muayene
                                    Muayene my = new Muayene();
                                    my.ProtokolNo = item.ID.PROTOKOL_NO;
                                    my.MuayeneKapalimi = true;
                                    my.MuayeneDurumu = myenum.MuayeneDurumu.MuayeneEdildi;
                                    my.TransferDurumu = (int)myenum.TransferDurumu.Gonderildi;
                                    my.TransferSonuc = item.SONUC.ToString();
                                    string mystr = item.ID.PROTOKOL_NO;
                                    if (mystr.Length == 13)
                                        mystr = "2" + mystr;
                                    if (mystr.Length == 12)
                                        mystr = "20" + mystr;
                                    my.TransferTarihi = DateTime.ParseExact(mystr, "yyyyMMddHHmmss", new System.Globalization.DateTimeFormatInfo());
                                    my.MuayeneKapamaTarihi = my.TransferTarihi;
                                    my.MuayeneTarihi = my.TransferTarihi;
                                    my.Aktif = true;
                                    my.Doktor = Current.AktifDoktor;
                                    my.Hasta = localhasta;
                                    my.Id = Convert.ToInt64(item.ID.PROTOKOL_NO);
                                    my.ProtokolNo = item.ID.PROTOKOL_NO;
                                    my.IsAutoImport = true;

                                    my.Delete();
                                    my.Insert();
                                    #endregion muayene
                                    Transaction.Instance.ExecuteNonQuery("Delete from MuayeneHizmet where Hasta_Id=" + localhasta.Id + " and Muayene_Id=" + my.Id);
                                    Transaction.Instance.ExecuteNonQuery("Delete from Recete where Hasta_Id=" + localhasta.Id + " and Muayene_Id=" + my.Id);
                                    Transaction.Instance.ExecuteNonQuery("Delete from Receteilac where Hasta_Id=" + localhasta.Id + " and MuayeneId=" + my.Id);
                                    Transaction.Instance.ExecuteNonQuery("Delete from MuayeneAsi where Hasta_Id=" + localhasta.Id + " and Muayene_Id=" + my.Id);
                                    int sayyb = 0;
                                    foreach (var itemm in item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM[1].BILESEN)
                                    {
                                        sayyb++;
                                        #region aşı
                                        if (itemm.MUDAHALE[0].MADDE_KOD != null)
                                        {
                                            AsiTanim asi = new AsiTanim();
                                            asi.Id = Transaction.Instance.ExecuteScalarL("Select top 1 Id from AsiTanim where kodu =@prm0", itemm.MUDAHALE[0].MADDE_KOD.LoincKod);
                                            if (asi.Id == 0)
                                                continue;
                                            mymodel.MuayeneAsi mha = new mymodel.MuayeneAsi();
                                            mha.Muayene = my;
                                            mha.Hasta = localhasta;
                                            mha.Doktor = localhasta.Doktor;
                                            mha.Id = Convert.ToInt64(my.ProtokolNo + sayyb.ToString());
                                            mha.TransferDurumu = my.TransferDurumu;
                                            mha.TransferSonuc = my.TransferSonuc;
                                            mha.TransferTarihi = my.TransferTarihi;
                                            mha.AsiTanim = asi;
                                            mha.Aktif = true;
                                            mha.IzlemTarihi = my.TransferTarihi;
                                            mha.IsAutoImport = true;
                                            mha.Insert();
                                        }
                                        #endregion aşı

                                        #region hizmet
                                        if (itemm.MUDAHALE[0].ISLEM_KOD != null)
                                        {
                                            Hizmet hz = new Hizmet();
                                            hz.Id = Transaction.Instance.ExecuteScalarL("Select top 1 Id from Hizmet where kodu =@prm0", itemm.MUDAHALE[0].ISLEM_KOD.LoincKod);
                                            if (hz.Id == 0)
                                                continue;
                                            mymodel.MuayeneHizmet mhz = new mymodel.MuayeneHizmet();
                                            mhz.Muayene = my;
                                            mhz.Hasta = localhasta;
                                            mhz.Doktor = localhasta.Doktor;
                                            mhz.Id = Convert.ToInt64(my.ProtokolNo + sayyb.ToString());
                                            mhz.TransferDurumu = my.TransferDurumu;
                                            mhz.TransferSonuc = my.TransferSonuc;
                                            mhz.TransferTarihi = my.TransferTarihi;
                                            mhz.Hizmet = hz;
                                            mhz.Aktif = true;
                                            mhz.IzlemTarihi = my.TransferTarihi;
                                            mhz.IsAutoImport = true;
                                            mhz.Insert();
                                        }
                                        #endregion hizmet
                                    }
                                }
                        }
                    #endregion bebek izlem

                    #region muayene,recete,ilac,hizmet
                    if (tm.MUAYENE_LISTE != null)
                        foreach (var item in tm.MUAYENE_LISTE)
                        {
                            #region muayene
                            Muayene my = new Muayene();
                            my.ProtokolNo = item.ID.PROTOKOL_NO;
                            my.MuayeneKapalimi = true;
                            my.MuayeneDurumu = myenum.MuayeneDurumu.MuayeneEdildi;
                            my.TransferDurumu = (int)myenum.TransferDurumu.Gonderildi;
                            my.TransferSonuc = item.SONUC.ToString();
                            string mystr = item.ID.PROTOKOL_NO;
                            if (mystr.Length == 13)
                                mystr = "2" + mystr;
                            if (mystr.Length == 12)
                                mystr = "20" + mystr;
                            my.TransferTarihi = DateTime.ParseExact(mystr, "yyyyMMddHHmmss", new System.Globalization.DateTimeFormatInfo());
                            my.MuayeneKapamaTarihi = my.TransferTarihi;
                            my.MuayeneTarihi = my.TransferTarihi;
                            my.Aktif = true;
                            my.Doktor = Current.AktifDoktor;
                            my.Hasta = localhasta;
                            my.Id = Convert.ToInt64(item.ID.PROTOKOL_NO);
                            my.ProtokolNo = item.ID.PROTOKOL_NO;
                            my.IsAutoImport = true;

                            my.Delete();
                            my.Insert();
                            #endregion muayene

                            Transaction.Instance.ExecuteNonQuery("Delete from MuayeneHizmet where Hasta_Id=" + localhasta.Id + " and Muayene_Id=" + my.Id);
                            Transaction.Instance.ExecuteNonQuery("Delete from Recete where Hasta_Id=" + localhasta.Id + " and Muayene_Id=" + my.Id);
                            Transaction.Instance.ExecuteNonQuery("Delete from Receteilac where Hasta_Id=" + localhasta.Id + " and MuayeneId=" + my.Id);
                            Transaction.Instance.ExecuteNonQuery("Delete from MuayeneAsi where Hasta_Id=" + localhasta.Id + " and Muayene_Id=" + my.Id);
                            Transaction.Instance.ExecuteNonQuery("Delete from MuayeneTeshis where Hasta_Id=" + localhasta.Id + " and Muayene_Id=" + my.Id);

                            if (item.BILESEN != null)
                            {
                                if (item.BILESEN.DOKUMAN_ICERIK_TIPI != null)
                                    if (item.BILESEN.DOKUMAN_ICERIK_TIPI.Length > 0)
                                        if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM != null)
                                            if (item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM.Length > 0)
                                            {
                                                int say = 0;
                                                foreach (var itemm in item.BILESEN.DOKUMAN_ICERIK_TIPI[0].DOKUMAN_BOLUM)
                                                {
                                                    say++;
                                                    if (say == 2)
                                                    {
                                                        int tanisay = 0;
                                                        if (itemm.BILESEN != null)
                                                            foreach (var tanii in itemm.BILESEN)
                                                            {
                                                                tanisay++;

                                                                #region tanı teşhis
                                                                if (tanii.MYN != null)
                                                                {
                                                                    Teshis ts = new Teshis();
                                                                    ts.Id = Transaction.Instance.ExecuteScalarL("Select top 1 Id from Teshis where kodu =@prm0", tanii.MYN[0].LoincKod);
                                                                    if (ts.Id == 0)
                                                                        continue;
                                                                    mymodel.MuayeneTeshis mts = new mymodel.MuayeneTeshis();
                                                                    mts.Muayene = my;
                                                                    mts.Hasta = localhasta;
                                                                    mts.Doktor = localhasta.Doktor;
                                                                    mts.Id = Convert.ToInt64(my.ProtokolNo + tanisay.ToString());
                                                                    mts.TransferDurumu = my.TransferDurumu;
                                                                    mts.TransferSonuc = my.TransferSonuc;
                                                                    mts.TransferTarihi = my.TransferTarihi;
                                                                    mts.Teshis = ts;
                                                                    mts.Aktif = true;
                                                                    mts.IzlemTarihi = my.TransferTarihi;
                                                                    mts.IsAutoImport = true;
                                                                    mts.Insert();
                                                                }
                                                                #endregion tanı teşhis

                                                            }
                                                    }
                                                    #region hizmet ve aşı
                                                    if (say == 3) //hizmet
                                                    {
                                                        if (itemm.BILESEN != null)
                                                            if (itemm.BILESEN.Length > 0)
                                                            {
                                                                int sayy = 0;
                                                                if (itemm.BILESEN[0].MUDAHALE != null)
                                                                    foreach (var mh in itemm.BILESEN[0].MUDAHALE)
                                                                    {
                                                                        sayy++;
                                                                        #region hizmet
                                                                        if (mh.ISLEM_KOD != null)
                                                                        {
                                                                            Hizmet hz = new Hizmet();
                                                                            hz.Id = Transaction.Instance.ExecuteScalarL("Select top 1 Id from Hizmet where kodu =@prm0", mh.ISLEM_KOD.LoincKod);
                                                                            if (hz.Id == 0)
                                                                                continue;
                                                                            mymodel.MuayeneHizmet mhz = new mymodel.MuayeneHizmet();
                                                                            mhz.Muayene = my;
                                                                            mhz.Hasta = localhasta;
                                                                            mhz.Doktor = localhasta.Doktor;
                                                                            mhz.Id = Convert.ToInt64(my.ProtokolNo + sayy.ToString());
                                                                            mhz.TransferDurumu = my.TransferDurumu;
                                                                            mhz.TransferSonuc = my.TransferSonuc;
                                                                            mhz.TransferTarihi = my.TransferTarihi;
                                                                            mhz.Hizmet = hz;
                                                                            mhz.Aktif = true;
                                                                            mhz.IzlemTarihi = my.TransferTarihi;
                                                                            mhz.IsAutoImport = true;
                                                                            mhz.Insert();
                                                                        }
                                                                        #endregion hizmet

                                                                        #region aşı
                                                                        if (mh.MADDE_KOD != null)
                                                                        {
                                                                            AsiTanim asi = new AsiTanim();
                                                                            asi.Id = Transaction.Instance.ExecuteScalarL("Select top 1 Id from AsiTanim where kodu =@prm0", mh.MADDE_KOD.LoincKod);
                                                                            if (asi.Id == 0)
                                                                                continue;
                                                                            mymodel.MuayeneAsi mha = new mymodel.MuayeneAsi();
                                                                            mha.Muayene = my;
                                                                            mha.Hasta = localhasta;
                                                                            mha.Doktor = localhasta.Doktor;
                                                                            mha.Id = Convert.ToInt64(my.ProtokolNo + sayy.ToString());
                                                                            mha.TransferDurumu = my.TransferDurumu;
                                                                            mha.TransferSonuc = my.TransferSonuc;
                                                                            mha.TransferTarihi = my.TransferTarihi;
                                                                            mha.AsiTanim = asi;
                                                                            mha.Aktif = true;
                                                                            mha.IzlemTarihi = my.TransferTarihi;
                                                                            mha.IsAutoImport = true;
                                                                            mha.Insert();
                                                                        }
                                                                        #endregion aşı
                                                                    }
                                                            }

                                                    }
                                                    #endregion hizmet ve aşı

                                                    if (say == 4) //reçete ilaç
                                                    {
                                                        int sayy = 0;
                                                        if (itemm.BILESEN != null)
                                                            if (itemm.BILESEN.Length > 0)
                                                            {
                                                                sayy++;
                                                                #region recete
                                                                mymodel.Recete rc = new mymodel.Recete();
                                                                rc.Muayene = my;
                                                                rc.Hasta = localhasta;
                                                                rc.Doktor = localhasta.Doktor;
                                                                rc.Id = Convert.ToInt64(my.ProtokolNo + sayy.ToString());
                                                                rc.Aktif = true;
                                                                rc.Aciklama = "Bakanlıktan Çekilen Bilgi";
                                                                rc.IzlemTarihi = my.TransferTarihi;
                                                                rc.IsAutoImport = true;
                                                                rc.Insert();

                                                                #endregion recete

                                                                foreach (var ilacc in itemm.BILESEN)
                                                                {
                                                                    if (ilacc.RECETE != null)
                                                                    {
                                                                        sayy++;
                                                                        #region ilac
                                                                        mymodel.Receteilac rilac = new mymodel.Receteilac();
                                                                        mymodel.ilac ilac = new mymodel.ilac();
                                                                        ilac.Id = Convert.ToInt64(ilacc.RECETE[0].LoincKod);
                                                                        rilac.Ilac = ilac;
                                                                        rilac.Recete = rc;
                                                                        rilac.Id = Convert.ToInt64(my.ProtokolNo + sayy.ToString());
                                                                        rilac.Hasta = localhasta;
                                                                        rilac.ilacAciklama = ilacc.RECETE[0].LoincAd;
                                                                        if (ilacc.RECETE[0].KULLANIM_DOZU != null)
                                                                        {
                                                                            rilac.ilacDozAciklama = ilacc.RECETE[0].KULLANIM_DOZU.MedikalDozBirimAd;
                                                                            rilac.Adet = (Int16)ilacc.RECETE[0].KULLANIM_DOZU.DozDeger;
                                                                        }
                                                                        if (ilacc.RECETE[0].KULLANIM_SEKLI != null)
                                                                        {
                                                                            rilac.KullanimSekli = (myenum.ilacKullanimSekli)Convert.ToInt32(ilacc.RECETE[0].KULLANIM_SEKLI.MedikalKullanimSekliKod);
                                                                            rilac.KullanimSekliAciklama = ilacc.RECETE[0].KULLANIM_SEKLI.MedikalKullanimSekliAd;
                                                                        }
                                                                        rilac.MuayeneId = my.Id;
                                                                        rilac.KullanimPeriyot = myenum.ilacKullanimPeriyot._1X1;
                                                                        rilac.TransferDurumu = my.TransferDurumu;
                                                                        rilac.TransferSonuc = my.TransferSonuc;
                                                                        rilac.TransferTarihi = my.TransferTarihi;
                                                                        rilac.Aktif = true;
                                                                        rilac.MuayeneId = my.Id;
                                                                        rilac.IsAutoImport = true;
                                                                        rilac.Insert();
                                                                        #endregion ilac
                                                                    }
                                                                }
                                                            }
                                                    }

                                                }
                                            }
                            }
                        }
                    #endregion muayene,recete,ilac,hizmet
                }
                catch
                { 
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public static void setLocalMuayeneIzlemToBakanlikMuayeneIzlem(Hasta localhasta)
        { 
            

        }
        

        public static Hasta setHastaTuikBilgi(Hasta hasta)
        {
                if (Current.AktifDoktor.WebTUIKServisKullaniciNo == 0 ||
                    Current.AktifDoktor.WebTUIKServisSifre.Length == 0 ||
                    Current.AktifDoktor.TckNo == 0
                    )
                    return hasta;
                TCKimlikNoKisiBilgi[] NufusBilgi;
                string snc = "";
                ServiceSoapClient serviceSoapClient = new ServiceSoapClient();
                  
                    NufusBilgi = serviceSoapClient.TCKimlikNoSorgulaArray(
                            Current.AktifDoktor.WebTUIKServisKullaniciNo,
                            Current.AktifDoktor.WebTUIKServisSifre,
                            hasta.TckNo
                            );
                    if (NufusBilgi != null)
                        if (NufusBilgi.Length>0)
                        if (NufusBilgi[0].TCKimlikNo != null)
                        {
                            try { hasta.Adi= NufusBilgi[0].Ad ?? ""; }
                            catch { }
                            try { hasta.Soyadi = NufusBilgi[0].Soyad ?? ""; }
                            catch { }
                            try { hasta.NfAileSiraNo = NufusBilgi[0].AileSiraNo ?? ""; }
                            catch { }
                            try { hasta.NfAileSiraNo = NufusBilgi[0].AileSiraNo ?? ""; }catch { }
                            try { hasta.NfAnaAd = NufusBilgi[0].AnaAd ?? "";}catch { }
                            try { hasta.NfAnaSoyad = NufusBilgi[0].AnaSoyad ?? "";}catch { }
                            try { hasta.NfBabaAd = NufusBilgi[0].BabaAd ?? "";}catch { }
                            try { hasta.NfBabaSoyad = NufusBilgi[0].BabaSoyad ?? "";}catch { }
                            try { hasta.NfBireySiraNo = NufusBilgi[0].BireySiraNo ?? "";}catch { }
                            try { hasta.NfCiltAd = NufusBilgi[0].CiltAd ?? "";}catch { }
                            try { hasta.NfCiltKod = NufusBilgi[0].CiltKod ?? "";}catch { }
                            try { hasta.NfCuzdanNo = NufusBilgi[0].CuzdanNo ?? "";}catch { }
                            try { hasta.NfCuzdanSeri = NufusBilgi[0].CuzdanSeri ?? "";}catch { }
                            try { hasta.NfDin = NufusBilgi[0].Din ?? "";}catch { }
                            try { hasta.NfDogumTarih = NufusBilgi[0].DogumTarih ?? "";}catch { }
                            try { hasta.NfDogumYer = NufusBilgi[0].DogumYer ?? "";}catch { }
                            try { hasta.NfKayIlAd = NufusBilgi[0].IlAd ?? "";}catch { }
                            try { hasta.NfKayIlceAd = NufusBilgi[0].IlceAd ?? "";}catch { }
                            try { hasta.NfKayIlceKod = NufusBilgi[0].IlceKod ?? "";}catch { }
                            try { hasta.NfKayIlKod = NufusBilgi[0].IlKod ?? "";}catch { }
                            try { hasta.NfMedeniHal = NufusBilgi[0].MedeniHal ?? "";}catch { }
                            try { hasta.NfOlumTarih = NufusBilgi[0].OlumTarih ?? "";}catch { }
                            try { hasta.NfOlumYer = NufusBilgi[0].OlumYer ?? "";}catch { }
                            try { hasta.NfVerildigiIlceAd = NufusBilgi[0].VerildigiIlceAd ?? "";}catch { }
                            try { hasta.NfVerildigiIlceKod = NufusBilgi[0].VerildigiIlceKod ?? "";}catch { }
                            try { hasta.NfverilmeNeden = NufusBilgi[0].verilmeNeden ?? "";}catch { }
                            try { hasta.NfVerilmeTarih = NufusBilgi[0].VerilmeTarih ?? "";}catch { }
                            try { hasta.NfYakinlik = NufusBilgi[0].Yakinlik ?? "";}catch { }
                        }
                        else
                            snc = "kimlik";
                
                GenelAdresKisiBilgi[] TUIKAdres;
                    TUIKAdres = serviceSoapClient.GenelAdresKisiBilgiSorgulaArray(
                            Current.AktifDoktor.WebTUIKServisKullaniciNo,
                            Current.AktifDoktor.WebTUIKServisSifre,
                            hasta.TckNo
                            );
                    if (TUIKAdres != null)
                        if (TUIKAdres.Length > 0)
                            if (TUIKAdres[0].AdresNo != null)
                    {
                        try { hasta.TUIKAdresNo = TUIKAdres[0].AdresNo ?? "";}catch { }
                        try { hasta.TUIKBucak = TUIKAdres[0].Bucak ?? "";}catch { }
                        try { hasta.TUIKBucakKodu = TUIKAdres[0].BucakKodu ?? "";}catch { }
                        try { hasta.TUIKCsbm = TUIKAdres[0].Csbm ?? "";}catch { }
                        try { hasta.TUIKCsbmKodu = TUIKAdres[0].CsbmKodu ?? "";}catch { }
                        try { hasta.TUIKDisKapiNo = TUIKAdres[0].DisKapiNo ?? "";}catch { }
                        try { hasta.TUIKIcKapiNo = TUIKAdres[0].IcKapiNo ?? "";}catch { }
                        try { hasta.TUIKIl = TUIKAdres[0].Il ?? "";}catch { }
                        try { hasta.TUIKIlKodu = TUIKAdres[0].IlKodu ?? "";}catch { }
                        try { hasta.TUIKIlce = TUIKAdres[0].Ilce ?? "";}catch { }
                        try { hasta.TUIKIlceKodu = TUIKAdres[0].IlceKodu ?? "";}catch { }
                        try { hasta.TUIKKoy = TUIKAdres[0].Koy ?? "";}catch { }
                       try {  hasta.TUIKKoyKodu = TUIKAdres[0].KoyKodu ?? "";}catch { }
                        try { hasta.TUIKKoyKayitNo = TUIKAdres[0].KoyKayitNo ?? "";}catch { }
                        try { hasta.TUIKMahalle = TUIKAdres[0].Mahalle ?? "";}catch { }
                        try { hasta.TUIKMahalleKodu = TUIKAdres[0].MahalleKodu ?? ""; }
                        catch { }
                    }
                    else
                        snc += " adres";

                if (snc.Length > 0)
                    snc = "Tuık " + snc + " bilgisi alınamadı";
                else
                {
                    snc = "Tuik kimlik adres bilgisi güncellendi";
                    hasta.TransferDurumu = myenum.TransferDurumu.Alindi;
                }
                hasta.TransferSonuc = snc;
                hasta.TransferTarihi = DateTime.Today;
           
            return hasta;
        }
    }
}
