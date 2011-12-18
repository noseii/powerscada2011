using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL.Metadata;

using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class Kullanici : Entity
    {
        [FieldDefinition(Length = 30, IsRequired = true)]
        public string Login { get; set; }

        [FieldDefinition(Length = 60, IsRequired = true)]
        public string Sifre { get; set; }

        [FieldDefinition(Length = 30, IsRequired = true)]
        public string Adi { get; set; }

        [FieldDefinition(Length = 30, IsRequired = true)]
        public string SoyAdi { get; set; }

        [FieldDefinition(IsRequired = true)]
        public DateTime DogumTarihi { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDoktorDegistirebilir { get; set; }

        //TODO:Şimdilik Gereksiz.
        //public KullaniciGrup kullanicigrup;

        //public KullaniciGrup KullaniciGrup
        //{
        //    get
        //    {
        //        return kullanicigrup == null ? kullanicigrup = new KullaniciGrup() : kullanicigrup;
        //    }
        //    set
        //    {
        //        kullanicigrup = value;
        //    }
        //}

        [FieldDefinition(Length = 20, IsRequired = true)]
        public string EvTel { get; set; }

        [FieldDefinition(Length = 30)]
        public string Gsm { get; set; }

        [FieldDefinition(Length = 400, IsRequired = true)]
        public string EvAdresi { get; set; }

        [FieldDefinition(Length = 100)]
        public string email { get; set; }

        [FieldDefinition(IsRequired = true)]
        public myenum.GorevTuru GorevTuru { get; set; }
        
        public Int32 Tipi { get; set; }

         [FieldDefinition(IsRequired = true)]
        public Int64 TckNo { get; set; }

        [FieldDefinition(Length = 30)]
        public string Bolumu { get; set; }

        public override string ToString() { return this.Adi +" "+ this.SoyAdi ?? ""; }

        public override void Validate()
        {
        //    if (this.Doktor.Id == 0)
        //        throw new ApplicationException("Her personel bir doktora bağlı olmak zorundadır.\nBu alan boş bırakılamaz.");
        //
        }
      

        //private Doktor doktor;

        //public Doktor Doktor
        //{
        //    get
        //    {
        //        return doktor == null ? doktor = new Doktor() : doktor;
        //    }
        //    set { doktor = value; }
        //}

        public  void ChangePassword(string newPassword)
        {
            string hash = SecurityHelper.GetMd5Hash(newPassword);
            this.Sifre = hash;
            Save();
        }
       
    }
}
