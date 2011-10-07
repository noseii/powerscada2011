using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL;

using SharpBullet.OAL.Metadata; 
namespace mymodel
{
    public class GebeBaslangic : BaseEntity
    {
        public byte GebelikNo { get; set; }
        
        public byte Nabiz { get; set; }
        
        public byte KucukTansiyon { get; set; }
        
        public byte BuyukTansiyon { get; set; }
        
        public DateTime SonAdetTarihi { get; set; }
        
        public DateTime BeklenenDogumTarihi { get; set; }
        
        public myenum.PelvisDurumu PelvisDurumu { get; set; }
        
        public DateTime OncekiGebelikSonlanmaTarihi { get; set; }
        
        public DateTime GebelikBildirimTarihi { get; set; }
        
        public bool TetanozBagisikligiVarmi { get; set; }
        
        public bool BeslenmeDanismanligiAldimi { get; set; }
        
        public bool DemirDestegiAldimi { get; set; }
        
        public bool AkrabaEvliligiVarmi { get; set; }

        public bool GebelikOncesiSistemikHastalik { get; set; }

        public myenum.KadinKorunmaYontemi KadinKorunmaYontemi { get; set; }

        
        //public myenum.GebelikRiskFaktoru GebelikRiskFaktoru { get; set; }
        
        public myenum.DogumlaIlgiliKarar DogumlaIlgiliKarar { get; set; }
        
        public myenum.KanGrubu EsininKanGrubu { get; set; }
        
        public myenum.AkrabalikDerece EsininAkrabalikDerecesi { get; set; }
        
        [FieldDefinition(Length =1000)]
        public string GenelNot { get; set; }
        
        [FieldDefinition(Length =510)]
        public string GebelikBildirimAciklamasi { get; set; }

        public int TransferDurumu { get; set; }
        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }
        public DateTime TransferTarihi { get; set; }

        public myenum.GebelikDurumu GebelikDurumu { get; set; }

        public DateTime GebelikSonuclanmaTarihi {get;set; }

        public string  FizikselMuayeneNotlari  { get; set; }

        public override void Validate()
        {
            base.Validate();
            if (IsAutoImport)
                return;
           

            if(Hasta.Cinsiyeti!=myenum.Cinsiyet.Kadın)
                throw new Exception("Gebelik başlangıç kaydı yanlızca bayanlar için kullanılabilir.");

            if (this.GebelikNo == 0)
                throw new Exception("Gebelik No alanı 0 bırakılamaz.");
            else
            {
                if (this.Id == 0)
                {
                    int aynınolugebelik = Transaction.Instance.ExecuteScalarI("Select count(Id) from GebeBaslangic where Hasta_Id=" + Hasta.Id + " and GebelikNo=" + GebelikNo + " and Aktif=1");
                    if (aynınolugebelik > 0)
                        throw new Exception("Sistem bu hastaya ait bu gebelik numarası kullanılmış.\n Bu gebelik No ile yeni bir kayıt açılamaz.");
                }
            }

           

            int gebebaslangicvarmi = 0;
            gebebaslangicvarmi = Transaction.Instance.ExecuteScalarI("Select count(Id) from GebeBaslangic Where Hasta_Id=@prm0 and GebelikDurumu='" + myenum.GebelikDurumu.Basladi.ToString()+ "' and aktif=1", new object[] { this.Hasta.Id });

            if (this.Id==0 && gebebaslangicvarmi > 0)
                throw new Exception("Sonlandırılmamış gebelik varken yeni gebe kaydı yapılamaz");

            if (this.BeklenenDogumTarihi == System.DateTime.MinValue)
                throw new Exception("Beklenen doğum tarihi alanı boş bırakılamaz.");

            if (this.BuyukTansiyon == 0)
                throw new Exception("Büyük Tansiyon alanı boş bırakılamaz.");

            if (this.DogumlaIlgiliKarar == 0)
                throw new Exception("Doğumla ilgili Karar alanı boş bırakılamaz.");

            if (this.EsininAkrabalikDerecesi == 0)
                throw new Exception("Eşinin Akrabalık Derecesi alanı boş bırakılamaz.");

            if (this.EsininKanGrubu == 0)
                throw new Exception("Eşinin Kan Grubu alanı boş bırakılamaz.");

            if (this.GebelikBildirimTarihi == System.DateTime.MinValue)
                throw new Exception("Gebelik Bildirim Tarihi alanı boş bırakılamaz.");

           

            if (this.KucukTansiyon == 0)
                throw new Exception("Küçük Tansiyon alanı boş bırakılamaz.");

            if (this.Nabiz == 0)
                throw new Exception("Nabız alanı boş bırakılamaz.");

           

            if (this.PelvisDurumu == 0)
                throw new Exception("Pelvis Durumu alanı boş bırakılamaz.");

            if (this.SonAdetTarihi ==System.DateTime.MinValue)
                throw new Exception("Son Adet Tarihi alanı boş bırakılamaz.");


            if (this.FizikselMuayeneNotlari.Length == 0)
                throw new Exception("Fiziksel Muayene Notları alanı boş bırakılamaz.");
          
        }

        public GebeBaslangic()
        {
            this.AkrabaEvliligiVarmi = false;
            this.Aktif = true;
            this.BeklenenDogumTarihi = System.DateTime.Now;
            this.BeslenmeDanismanligiAldimi = false;
            this.BuyukTansiyon = 0;
            this.DemirDestegiAldimi = false;
            this.GebelikBildirimAciklamasi = string.Empty;
            this.GebelikBildirimTarihi = System.DateTime.Now;
            this.GebelikDurumu = myenum.GebelikDurumu.Basladi;
            this.GebelikNo = 0;
            this.GenelNot = string.Empty;
            this.Hasta = new Hasta();
            this.KucukTansiyon = 0;
            this.Muayene = new Muayene();
            this.Nabiz = 0;
            this.Randevu = new Takvim();
            this.SonAdetTarihi = System.DateTime.Now;
            this.TetanozBagisikligiVarmi = false;
            

        }
    }
}
