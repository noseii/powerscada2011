using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
using SharpBullet.OAL;
namespace mymodel
{
    public class Doktor : Entity
    {
        [FieldDefinition(Length = 36)]
        public string Adi { get; set; }

        [FieldDefinition(Length = 36)]
        public string Soyadi { get; set; }

        public DateTime Dogumtar { get; set; }

        public mymodel.myenum.Cinsiyet Cinsiyeti { get; set; }

        public Int64 TckNo { get; set; }


        #region Iletisim Bilgileri

        [FieldDefinition(Length = 20)]
        public string Ceptel { get; set; }

        [FieldDefinition(Length = 20)]
        public string Evtel { get; set; }

        [FieldDefinition(Length = 510)]
        public string Evadresi { get; set; }


        #endregion


        [FieldDefinition(Length = 100)]
        public string Universite { get; set; }

        public DateTime MezuniyetTarihi { get; set; }

        [FieldDefinition(Length = 36)]
        public string Diplomano { get; set; }

        [FieldDefinition(Length = 30)]
        public string Unvan { get; set; }

        //TODO: brans kodları bakılacak:Tablodan Geliyor enum değilde Tablo olmalı SAdece Adı Kodu oaln bir tablo TanımTablosu
        public mymodel.myenum.Brans Brans { get; set; }

        //TODO: doktor tipi kodlarını bul 
        //Gereksiz Aile Hekimi mi yoksa sağlık Ocağımı diyor bizde zaten sadece aile hekimi olacak
        //public mymodel.myenum.DoktorTipi DoktorTipi  { get; set; }

        public DateTime GorevBasTar { get; set; }

        public DateTime GorevBitTar { get; set; }

        //TODO: görev durum kodları bakılacak Çalışıyor Ayrıldı SAğlık Ocağından mı Kalma
        /// <summary>
        /// Çalışıyor Ayrıldı. Ne işe yara
        /// </summary>
        public mymodel.myenum.GorevDurumu GorevDurumu { get; set; }


        [FieldDefinition(Length = 32)]
        public string TescilNo { get; set; }

        public decimal/*(12,2)*/ Kontur { get; set; }



        [FieldDefinition(Length = 16)]
        public string WebServisSifre { get; set; }
        public Int64 WebServisKullaniciNo { get; set; }

        [FieldDefinition(Length = 16)]
        public string WebTUIKServisSifre { get; set; }
        public Int32 WebTUIKServisKullaniciNo { get; set; }

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

        public override string ToString() { { return this.Adi + " " + this.Soyadi; } }


        [FieldDefinition(MappingType = FieldMappingType.No)]
        public static Doktor DoktorOku(long id)
        {
            Doktor doktor = SharpBullet.OAL.Persistence.Read<Doktor>(id);

            return doktor;
        }

        public override void Validate()
        {
            base.Validate();
            if (this.TckNo == 0)
                throw new Exception("Tc kimlik numarası boş bırakılamaz");
            if (this.TckNo.ToString().Length != 11)
                throw new Exception("Tc kimlik numarası 11 karakter olmalıdır. Tc kimlik numarasını kontrol ediniz");
            if (this.Adi.Length == 0)
                throw new Exception("Adı boş bırakılamaz");
            if (this.Soyadi.Length == 0)
                throw new Exception("Soyadı boş bırakılamaz");
            if (this.WebServisKullaniciNo== 0)
                throw new Exception("Web Servis Kullanıcı No boş bırakılamaz");
            if (this.WebTUIKServisKullaniciNo == 0)
                throw new Exception("Web TUIK Kullanıcı No boş bırakılamaz");
            if (this.WebServisSifre.Length == 0)
                throw new Exception("Web Servis şifre boş bırakılamaz");
            if (this.WebTUIKServisSifre.Length == 0)
                throw new Exception("Web TUIK şifre boş bırakılamaz");
            if (this.Diplomano.Length == 0)
                throw new Exception("Diploma no boş bırakılamaz");


        }
    }
}
