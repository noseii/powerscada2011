using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL;

using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class GebeIzleme : BaseEntity
    {
       
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

        public byte GebelikNo { get; set; }

        public byte GebelikHaftaNo { get; set; }

        public bool VarisVarmi { get; set; }

        public byte Nabiz { get; set; }

        public byte KucukTansiyon { get; set; }

        public byte BuyukTansiyon { get; set; }

        public bool idrardaProteinVarmi { get; set; }

        public myenum.IdrardaProteinDurumu idrardaProtein { get; set; }

        

        public string KanBasinci { get; set; }

        public myenum.CocukGelisBicimi GelisBicimi { get; set; }

        public decimal Hemoglobin { get; set; }

        public byte CocukKalpSesiAdedi { get; set; }

        [FieldDefinition(Length =512)]
        public string Ogutler { get; set; }

        public DateTime SonrakiIzlemTarihi { get; set; }

        //public DateTime izlemTarihi { get; set; }

        public bool OdemVarmi { get; set; }

        public Int16 Agirligi { get; set; }

        public byte izlemHaftaNo { get; set; }

        public bool TetanozAsisiYapildi { get; set; }


        public int TransferDurumu { get; set; }

        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }

        public DateTime TransferTarihi { get; set; }

        private Doktor vekildoktor;
        public Doktor VekilDoktor
        {
            get
            {
                return vekildoktor == null ? vekildoktor = new Doktor() : vekildoktor;
            }
            set
            {
                vekildoktor = value;
            }
        }

        public override void Validate()
        {
            base.Validate();
            if (IsAutoImport)
                return;
            if (Hasta.Cinsiyeti != myenum.Cinsiyet.Kadın)
                throw new Exception("Gebelik başlangıç kaydı yanlızca bayanlar için kullanılabilir.");

            if (this.GebelikNo == 0)
                throw new Exception("Gebelik no alanı sıfır olamaz.\nBu hastaya ait gebelik başlangıç kaydı girmelisiniz.");

  

            int gebebaslangicvarmi = 0;
            gebebaslangicvarmi = Transaction.Instance.ExecuteScalarI("Select count(Id) from GebeBaslangic Where Hasta_Id=@prm0  and GebelikDurumu='" + myenum.GebelikDurumu.Basladi.ToString() + "' and GebelikNo=@prm1 and  aktif=1", new object[] { this.Hasta.Id,this.GebelikNo});

            if (0 >= gebebaslangicvarmi)
                throw new Exception("Gebelik başlangıcı olmayan hastaya gebe izleme kaydı girilemez");

            if (this.Agirligi == 0)
                throw new Exception("Ağırlığı alanı boş bırakılamaz.");

            if (this.BuyukTansiyon == 0)
                throw new Exception("Büyük Tansiyon alanı boş bırakılamaz.");

            if (this.CocukKalpSesiAdedi == 0)
                throw new Exception("Çocuk Kalp Sesi Adedi alanı boş bırakılamaz.");

            if (this.GebelikHaftaNo == 0)
                throw new Exception("Gebelik Hafta No alanı boş bırakılamaz.");

            if (this.GebelikNo == 0)
                throw new Exception("Gebelik No alanı boş bırakılamaz.");

            if (this.GelisBicimi == 0)
                throw new Exception("Gelis Biçimi alanı boş bırakılamaz.");

            if (this.Hemoglobin == 0)
                throw new Exception("Hemoglobin alanı boş bırakılamaz.");

            if (this.izlemHaftaNo == 0)
                throw new Exception("izlem Hafta No alanı boş bırakılamaz.");

            if (this.IzlemTarihi == System.DateTime.MinValue)
                throw new Exception("Izlem Tarihi  alanı boş bırakılamaz.");

            if (this.KucukTansiyon == 0)
                throw new Exception("Kücük Tansiyon  alanı boş bırakılamaz.");

            if (this.Nabiz==0)
                throw new Exception("Nabiz alanı boş bırakılamaz.");

            
        }
    }
}
