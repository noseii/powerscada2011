using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
using SharpBullet.OAL;
namespace mymodel
{
    public class Receteilac : Entity
    {

        private Hasta hasta;

        public Hasta Hasta
        {
            get
            {
                return hasta == null ? hasta = new Hasta() : hasta;
            }
            set
            {
                hasta = value;
            }
        }

        private Takvim randevu;

        public Takvim Randevu
        {
            get
            {
                return randevu == null ? randevu = new Takvim() : randevu;
            }
            set
            {
                randevu = value;
            }
        }

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public string Adi
        {
            get
            {
                return ilac.Id > 0 ? ilac.Adi : "";
            }

        }
        
        [FieldDefinition(MappingType = FieldMappingType.No)]
        public string Barkod
        {
            get
            {
                return ilac.Id > 0 ? ilac.Barkod : "";
            }
        }

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public string AzamiDozaj
        {
            get
            {
                return ilac.Id > 0 ? ilac.AzamiDozaj : "";
            }

        }
        
        
        private Recete recete;
        public Recete Recete
        {
            get
            {
                return recete == null ? recete = new Recete() : recete;
            }
            set
            {
                recete = value;
            }
        }


        private ilac ilac;
        public ilac Ilac
        {
            get
            {
                return ilac == null ? ilac = new ilac() : ilac;
            }
            set
            {
                ilac = value;
            }
        }
        
        public Int16 Adet { get; set; }

        public string Dozaj { get; set; }

        public myenum.ilacKullanimSekli KullanimSekli { get; set; }

        public myenum.ilacKullanimPeriyot KullanimPeriyot { get; set; }

        public string ilacDozAciklama { get; set; }

        public string KullanimSekliAciklama { get; set; }

        public string ilacKutu { get; set; }

        public string ilacAciklama { get; set; }

        public int TransferDurumu { get; set; }
        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }
        public DateTime TransferTarihi { get; set; }


        //Denormalize alan Raporda kullanılmak üzere eklendi..
        //Başka bir amacı yok..
        [FieldDefinition(NonUniqueIndexGroup = "MuayeneIdIND")]
        public long MuayeneId { get; set; }

        public Receteilac()
        {
            Recete = new Recete();
            ilac = new ilac();
            Adet = 0;
            Dozaj = string.Empty;
            ilacDozAciklama = string.Empty;
            ilacKutu = string.Empty;
            ilacAciklama = string.Empty;
        }

        public override void Validate()
        {
            base.Validate();
            if (IsAutoImport)
                return;
            if (KullanimSekli == 0)
                throw new Exception("Kullanım şekli boş bırakılamaz");
            if (KullanimPeriyot == 0)
                throw new Exception("Kullanım periyodu boş bırakılamaz");
            if (Adet == 0)
                throw new Exception("Adet bilgisi 0 olamaz");
        }

        public override string ToString() { return this.ilac.ToString() ?? ""; }

      
    }
}
