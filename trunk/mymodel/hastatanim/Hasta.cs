using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL;

using SharpBullet.OAL.Metadata;
namespace mymodel
{
    public class Hasta : Entity
    {
        public override string ToString() { return this.Adi + " " + this.Soyadi; }

        [FieldDefinition(IsRequired = true)]
        public myenum.KayitDurumu KayitDurumu { get; set; }

        [FieldDefinition(IsRequired = true)]
        public myenum.KayitKimlikDurumu KayitKimlikDurumu { get; set; }

        [FieldDefinition(Length = 40, IsRequired = true)]
        public string Adi { get; set; }

        [FieldDefinition(Length = 40, IsRequired = true)]
        public string Soyadi { get; set; }

        [FieldDefinition(IsRequired = true)]
        public myenum.Cinsiyet Cinsiyeti { get; set; }

        public long TckNo { get; set; }
        public string PasaportNo { get; set; }
        public myenum.SosyalGuvenlikKurumTipi KurumTipi { get; set; }
        public myenum.MedeniHali MedeniHali { get; set; }

        public myenum.KanGrubu KanGrubu { get; set; }
        [FieldDefinition(IsRequired = true)]
        public myenum.Cinsiyet BeyanCinsiyeti { get; set; }

        [FieldDefinition(IsRequired = true)]
        public DateTime DogumTarihi { get; set; }
        public DateTime OlumTarihi { get; set; }
        public DateTime BeyanDogumTarihi { get; set; }
        private Doktor doktor;
        [FieldDefinition(IsRequired = true)]
        public Doktor Doktor
        {
            get
            {
                return doktor == null ? doktor = new Doktor() : doktor;
            }
            set
            {
                doktor = value;
            }
        }

        /// <summary>
        /// İletişim Bilgileri
        /// </summary>
        public myenum.IletisimTip IletisimTip { get; set; }
        public myenum.AdresTip AdresTip { get; set; }
        [FieldDefinition(Length = 28)]
        public string EvTel { get; set; }
        [FieldDefinition(Length = 28)]
        public string IsTel { get; set; }
        [FieldDefinition(Length = 28)]
        public string CepTel { get; set; }
        public string EPostaAdresi { get; set; }
        public string WebAdresi { get; set; }
        /// <summary>
        /// Ev Adresi
        /// </summary>
        private Lokasyon lokasyonSehir;//seviye1
        public Lokasyon LokasyonSehir
        {
            get
            {
                return lokasyonSehir == null ? lokasyonSehir = new Lokasyon() : lokasyonSehir;
            }
            set
            {
                lokasyonSehir = value;
            }
        }
        private Lokasyon lokasyonilce;//seviye2
        public Lokasyon Lokasyonilce
        {
            get
            {
                return lokasyonilce == null ? lokasyonilce = new Lokasyon() : lokasyonilce;
            }
            set
            {
                lokasyonilce = value;
            }
        }
        private Lokasyon lokasyonSemtBelediye;//seviye3
        public Lokasyon LokasyonSemtBelediye
        {
            get
            {
                return lokasyonSemtBelediye == null ? lokasyonSemtBelediye = new Lokasyon() : lokasyonSemtBelediye;
            }
            set
            {
                lokasyonSemtBelediye = value;
            }
        }
        private Lokasyon lokasyonMahalleKoy;//seviye4
        public Lokasyon LokasyonMahalleKoy
        {
            get
            {
                return lokasyonMahalleKoy == null ? lokasyonMahalleKoy = new Lokasyon() : lokasyonMahalleKoy;
            }
            set
            {
                lokasyonMahalleKoy = value;
            }
        }
        private Lokasyon lokasyonMahalle;//seviye5
        public Lokasyon LokasyonMahalle
        {
            get
            {
                return lokasyonMahalle == null ? lokasyonMahalle = new Lokasyon() : lokasyonMahalle;
            }
            set
            {
                lokasyonMahalle = value;
            }
        }
        public string LokasyonIcKapiNo { get; set; }
        [FieldDefinition(Length = 20)]
        public string LokasyonDısKapiNo { get; set; }
        [FieldDefinition(Length = 500)]
        public string LokasyonAdresText { get; set; }
        /// <summary>
        /// İş Adresi
        /// </summary>
        [FieldDefinition(Length = 20)]
        private Lokasyon lokasyonSehir1;//seviye1
        public Lokasyon LokasyonSehir1
        {
            get
            {
                return lokasyonSehir1 == null ? lokasyonSehir1 = new Lokasyon() : lokasyonSehir1;
            }
            set
            {
                lokasyonSehir1 = value;
            }
        }
        private Lokasyon lokasyonilce1;//seviye2
        public Lokasyon Lokasyonilce1
        {
            get
            {
                return lokasyonilce1 == null ? lokasyonilce1 = new Lokasyon() : lokasyonilce1;
            }
            set
            {
                lokasyonilce1 = value;
            }
        }
        private Lokasyon lokasyonSemtBelediye1;//seviye3
        public Lokasyon LokasyonSemtBelediye1
        {
            get
            {
                return lokasyonSemtBelediye1 == null ? lokasyonSemtBelediye1 = new Lokasyon() : lokasyonSemtBelediye1;
            }
            set
            {
                lokasyonSemtBelediye1 = value;
            }
        }
        private Lokasyon lokasyonMahalleKoy1;//seviye4
        public Lokasyon LokasyonMahalleKoy1
        {
            get
            {
                return lokasyonMahalleKoy1 == null ? lokasyonMahalleKoy1 = new Lokasyon() : lokasyonMahalleKoy1;
            }
            set
            {
                lokasyonMahalleKoy1 = value;
            }
        }
        private Lokasyon lokasyonMahalle1;//seviye5
        public Lokasyon LokasyonMahalle1
        {
            get
            {
                return lokasyonMahalle1 == null ? lokasyonMahalle1 = new Lokasyon() : lokasyonMahalle1;
            }
            set
            {
                lokasyonMahalle1 = value;
            }
        }
        public string LokasyonIcKapiNo1 { get; set; }
        [FieldDefinition(Length = 20)]
        public string LokasyonDısKapiNo1 { get; set; }
        [FieldDefinition(Length = 500)]
        public string LokasyonAdresText1 { get; set; }

        private Ulke ulke;
        public Ulke Ulke
        {
            get
            {
                return ulke == null ? ulke = new Ulke() : ulke;
            }
            set
            {
                ulke = value;
            }
        }
        private Uyruk uyruk;
        public Uyruk Uyruk
        {
            get
            {
                return uyruk == null ? uyruk = new Uyruk() : uyruk;
            }
            set
            {
                uyruk = value;
            }
        }

        [FieldDefinition(Length = 1000)]
        public string BeyanAdresi { get; set; }

        public myenum.EgitimDurumu EgitimDurumu { get; set; }
        [FieldDefinition(Length = 30)]
        public string KurumSicilNo { get; set; }
        [FieldDefinition(IsRequired = true)]

        public bool OzurluHasta { get; set; }
        public bool EskiHasta { get; set; }

        public bool GeziciHizmetVerilenHasta { get; set; }

        public bool YardimaMuhtacHasta { get; set; }

        [FieldDefinition(Length = 8000)]
        public byte[] Resim { get; set; }

        [FieldDefinition(Length = 40)]
        public string DogumYeri { get; set; }


        [FieldDefinition(Length = 30)]
        public byte EvlilikYasi { get; set; }

        public byte ilkGebelikYasi { get; set; }

        public string AnneAdi { get; set; }

        [FieldDefinition(Length = 30)]
        public string AnneSoyadi { get; set; }

        public Int64 TckNoAnne { get; set; }

        public myenum.KanGrubu EsininKanGrubu { get; set; }

        public bool Obezmi { get; set; }

        public myenum.TransferDurumu TransferDurumu { get; set; }

        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }

        public DateTime TransferTarihi { get; set; }

        [FieldDefinition(Length = 16)]
        public string KarneSeri { get; set; }

        public Int64 KarneSiraNo { get; set; }

        [FieldDefinition(Length = 30)]
        public string BabaAdi { get; set; }

        [FieldDefinition(Length = 16)]
        public string EsAdi { get; set; }

        public myenum.AkrabalikDerece EsininAkrabalikDerecesi { get; set; }
        public string NfAileSiraNo { get; set; }
        public string NfBireySiraNo { get; set; }
        public string NfCiltAd { get; set; }
        public string NfCiltKod { get; set; }
        public string NfKayIlAd { get; set; }
        public string NfKayIlKod { get; set; }
        public string NfKayIlceAd { get; set; }
        public string NfKayIlceKod { get; set; }
        public string NfCuzdanSeri { get; set; }
        public string NfCuzdanNo { get; set; }
        public string NfVerildigiIlceAd { get; set; }
        public string NfVerildigiIlceKod { get; set; }
        public string NfverilmeNeden { get; set; }
        public string NfVerilmeTarih { get; set; }
        public string NfYakinlik { get; set; }
        public string NfOlumYer { get; set; }
        public string NfOlumTarih { get; set; }
        public string NfDin { get; set; }
        public string NfBabaSoyad { get; set; }
        public string NfBabaAd { get; set; }
        public string NfAnaSoyad { get; set; }
        public string NfAnaAd { get; set; }
        public string NfDogumYer { get; set; }
        public string NfDogumTarih { get; set; }
        public string NfMedeniHal { get; set; }
        public string TUIKAdresNo { get; set; }
        public string TUIKIl { get; set; }
        public string TUIKIlKodu { get; set; }
        public string TUIKIlce { get; set; }
        public string TUIKIlceKodu { get; set; }
        public string TUIKIcKapiNo { get; set; }
        public string TUIKMahalle { get; set; }
        public string TUIKMahalleKodu { get; set; }
        public string TUIKCsbm { get; set; }
        public string TUIKCsbmKodu { get; set; }
        public string TUIKDisKapiNo { get; set; }
        public string TUIKKoy { get; set; }
        public string TUIKKoyKodu { get; set; }
        public string TUIKKoyKayitNo { get; set; }
        public string TUIKBucak { get; set; }
        public string TUIKBucakKodu { get; set; }

        public string TUIKAcikAdres()
        {
            return TUIKCsbm ?? "" + " " + TUIKBucak ?? "" + " " + TUIKKoy ?? "" + " " + TUIKMahalle ?? "" + " No:" + TUIKDisKapiNo ?? "" + "/" + TUIKIcKapiNo ?? "" + " " +
                TUIKIlce ?? "" + " - " + TUIKIl ?? "";
        }

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public static Hasta HastaOku(long id)
        {
            Hasta hasta = SharpBullet.OAL.Persistence.Read<Hasta>(id);

            return hasta;
        }

        public byte Yasi()
        {
            byte result = 0;
            if (this.DogumTarihi != null)
                result = (byte)(DateTime.Now.Subtract(this.DogumTarihi.Date).Days / 365);
            return result;
        }

        public string YasAy()
        {
            string result = "";

            if (this.BeyanDogumTarihi != null && this.BeyanDogumTarihi != System.DateTime.MinValue)
            {
                TimeSpan tmspn = (DateTime.Now.Subtract(this.BeyanDogumTarihi.Date));
                result = tmspn.Days / 365 + " Yil " + (tmspn.Days % 365) / 30 + " Ay " + ((tmspn.Days % 365) % 30) + " Gün ";
            }
            else
                if (this.DogumTarihi != null && this.DogumTarihi != System.DateTime.MinValue)
                {
                    TimeSpan tmspn = (DateTime.Now.Subtract(this.DogumTarihi.Date));
                    result = tmspn.Days / 365 + " Yil " + (tmspn.Days % 365) / 30 + " Ay " + ((tmspn.Days % 365) % 30) + " Gün ";
                }
           
            return result;
        }

        public override void Validate()
        {
            base.Validate();
            if (this.KayitKimlikDurumu == 0)
            {
                throw new Exception("Kayıt Kimlik Durumu boş bırakılamaz");
            }
            if (this.KayitDurumu == 0)
            {
                throw new Exception("Kayıt Durumu boş bırakılamaz");
            }

            if (this.KayitKimlikDurumu == 0)
            {
                throw new Exception("Kayıt Kimlik Durumu boş bırakılamaz");
            }

            if (this.KayitKimlikDurumu == myenum.KayitKimlikDurumu.TckNoVar)
            {
                if (this.TckNo == 0)
                    throw new Exception("Tc kimlik numarası boş bırakılamaz");
                if (this.TckNo.ToString().Length != 11)
                    throw new Exception("Tc kimlik numarası 11 karakter olmalıdır. Tc kimlik numarasını kontrol ediniz");
                if (Transaction.Instance.ExecuteScalarI("Select Count(Id) from Hasta where KayitKimlikDurumu='TckNoVar' and TckNo=" + TckNo) > 1)
                    throw new Exception("Bu Kimlik numarasına ait hasta kaydı mevcut.\nAynı kimlik numarasına ait hasta açılamaz");
                if (PasaportNo.Trim().Length != 0)
                    throw new Exception("Kimlik numarası girilen hastalarda Pasaport No alanı dolu olamaz.");
            }
            else
                if (this.KayitKimlikDurumu == myenum.KayitKimlikDurumu.YabanciUyruk)
                {
                    if (PasaportNo.Trim().Length == 0)
                        throw new Exception("Yabancı uyruklu hastalarda Pasaport No alanı boş bırakılamaz");
                    else
                        if (PasaportNo.Trim().Length < 7)
                            throw new Exception("Pasaport No alanı 7 karakterden küçük olamaz");
                    if (this.Ulke.Id == 0)
                        throw new Exception("Yabancı uyruklu hastalarda ülke seçimi zorunludur.");
                    if (Transaction.Instance.ExecuteScalarI("Select Count(Id) from Hasta where PasaportNo='" + PasaportNo + "'") > 1)
                        throw new Exception("Bu pasaport numarası ile daha önce bir hasta kaydı yapılmış.\nAynı pasaport numarasına ait hasta açılmaz");
                }

            if (this.DogumTarihi > DateTime.Today)
                throw new Exception("Doğum tarihi bugünün tarihinden büyük olamaz");
            if (this.DogumTarihi == System.DateTime.MinValue)
                throw new Exception("Doğum tarihi boş bırakılamaz");
            if (this.Cinsiyeti == 0)
            {
                throw new Exception("Cinsiyeti boş bırakılamaz");
            }          
        }
    }



}
