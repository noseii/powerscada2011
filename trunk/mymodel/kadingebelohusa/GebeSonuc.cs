using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL;

using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class GebeSonuc : BaseEntity
    {
        //private Muayene muayene;public Muayene Muayene { get { return muayene == null ? muayene = new Muayene() : muayene;} set { muayene = value;} }

        //private Hasta hasta;

        //public Hasta Hasta
        //{
        //    get
        //    {
        //        return hasta == null ? hasta = new Hasta() : hasta;
        //    }
        //    set
        //    {
        //        hasta = value;
        //    }
        //}

        //private Takvim randevu;

        //public Takvim Randevu
        //{
        //    get
        //    {
        //        return randevu == null ? randevu = new Takvim() : randevu;
        //    }
        //    set
        //    {
        //        randevu = value;
        //    }
        //}

        private GebeBaslangic gebebaslangic;
        public GebeBaslangic GebeBaslangic
        {
            get
            {
                return gebebaslangic == null ? gebebaslangic = new GebeBaslangic() : gebebaslangic;
            }
            set
            {
                gebebaslangic = value;
            }
        }

        public myenum.CocukGelisBicimi GelisBicimi { get; set; }

        public byte GebelikNo { get; set; }

        public byte GebelikHaftaNo { get; set; }

        public byte CanliDogumAdedi { get; set; }

        public byte OluDogumAdedi { get; set; }

        public DateTime Tarih { get; set; }

        public myenum.GebelikSonucu GebelikSonucu { get; set; }

        public myenum.DogumYontemi DogumYontemi { get; set; }

        public myenum.DogumaYardimEden DogumaYardimEden { get; set; }

        public myenum.DogumunYapildigiYer DogumunYapildigiYer { get; set; }

        public bool CogulDogummu { get; set; }

        [FieldDefinition(Length =1000)]
        public string SonucNotlar { get; set; }

        public decimal Agirligi { get; set; }

        public int TransferDurumu { get; set; }
        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }
        public DateTime TransferTarihi { get; set; }

       

        public override void Validate()
        {
            base.Validate();
            if (IsAutoImport)
                return;

            if (Hasta.Cinsiyeti != myenum.Cinsiyet.Kadın)
                throw new Exception("Gebelik başlangıç kaydı yanlızca bayanlar için kullanılabilir.");

            if (this.GebelikNo == 0)
                throw new Exception("Gebelik no alanı sıfır olamaz.\nBu hasataya ait gebelik başlangıç kaydı girmelisiniz.");

            if (this.Id == 0)
            {
                int gebebaslangicvarmi = 0;
                gebebaslangicvarmi = Transaction.Instance.ExecuteScalarI("Select count(Id) from GebeBaslangic Where Hasta_Id=@prm0  and GebelikDurumu='" + myenum.GebelikDurumu.Basladi.ToString() + "' and GebelikNo=@prm1 and  aktif=1", new object[] { this.Hasta.Id, this.GebelikNo });

                if (0 >= gebebaslangicvarmi)
                    throw new Exception("Gebelik başlangıcı olmayan hastaya gebe sonuç kaydı girilemez");
            }

            if (this.CanliDogumAdedi == 0 && this.OluDogumAdedi == 0)
            {
                throw new Exception("CanliDogumAdedi ve OluDogumAdedi Toplami o dan büyük olmalıdır.");
            }

            if (this.DogumaYardimEden == 0)
            {
                throw new Exception("Doğuma Yardım Eden alanı boş bırakılamaz.");
            }

            if (this.DogumunYapildigiYer == 0)
            {
                throw new Exception("Doğumun Yapıldıgı Yer alanı boş bırakılamaz.");
            }

            if (this.DogumYontemi == 0)
            {
                throw new Exception("Doğum Yöntemi alanı boş bırakılamaz.");
            }

            if (this.GebelikHaftaNo == 0)
            {
                throw new Exception("Gebelik Hafta No alanı boş bırakılamaz.");
            }

            if (this.GebelikNo == 0)
            {
                throw new Exception("Gebelik No alanı boş bırakılamaz.");
            }

            if (this.GebelikSonucu == 0)
            {
                throw new Exception("Gebelik Sonucu alanı boş bırakılamaz.");
            }

            if (this.GelisBicimi == 0)
            {
                throw new Exception("Geliş Biçimi alanı boş bırakılamaz.");
            }

            if (this.Tarih == System.DateTime.MinValue)
            {
                throw new Exception("Tarih alanı boş bırakılamaz.");
            }

           
        }
    }
}
