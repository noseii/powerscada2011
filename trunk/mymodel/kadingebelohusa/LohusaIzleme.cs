using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class LohusaIzleme : BaseEntity
    {
        public byte Nabiz { get; set; }

        public byte KucukTansiyon { get; set; }

        public byte BuyukTansiyon { get; set; }

        public byte Ates { get; set; }

        [FieldDefinition(Length =512)]
        public string AciklamaOgut { get; set; }
        
        public byte GebelikNo { get; set; }

        public DateTime GebelikSonucuTarihi { get; set; }

        public bool BeslenmeDanismanligiAldimi { get; set; }

        public bool DemirDestegiAldimi { get; set; }

        public bool EmzirmeDanismanligiAldimi { get; set; }

        public GebeSonuc GebeSonuc { get; set; }

        public bool BebekDogumKomplikasyonVarmi { get; set; }//TODO:kadınlohusa izlem BebekDogumKomplikasyonVarmi alanı yeni ekrrana eklenmeli


        private Teshis lohusaizlemtanisi;
        public Teshis  LohusaIzlemTanisi
        {
            get
            {
                return lohusaizlemtanisi == null ? lohusaizlemtanisi = new Teshis() : lohusaizlemtanisi;
            }
            set { lohusaizlemtanisi = value; }
        }

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

            int gebebaslangicvarmi = 0;
            gebebaslangicvarmi = Transaction.Instance.ExecuteScalarI("Select count(Id) from GebeBaslangic Where Hasta_Id=@prm0 and  GebelikDurumu='" + myenum.GebelikDurumu.Bitti.ToString() + "' and GebelikNo=@prm1 and  aktif=1", new object[] { this.Hasta.Id,this.GebelikNo });

            if (0 >= gebebaslangicvarmi)
                throw new Exception("Gebelik başlangıcı olmayan hastaya gebe sonuç kaydı girilemez");

            if (this.AciklamaOgut.Length==0)
                throw new Exception("Açıklama ögüt alanı boş bırakılamaz.");

            if (this.Ates==0)
                throw new Exception("Ates alanı boş bırakılamaz.");

            if (this.BuyukTansiyon == 0)
                throw new Exception("Büyük Tansiyon alanı boş bırakılamaz.");

            if (this.GebelikSonucuTarihi == System.DateTime.MinValue)
                throw new Exception("Gebelik Sonuç Tarihi alanı boş bırakılamaz.");
        }
    }

    //TODO:GEbeliği bitti diye sonuç girilen bir kadının birden fazla kaydı var ise bu kayıtlardan hangisine sonu
    //girilecek bir combo getirsek biten gebeliklerin listesini ordan mı seçtirmeliyiz ya da nasıl oluyor 
}
