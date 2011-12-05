using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
using SharpBullet.OAL;


namespace mymodel
{
    public class Lokasyon : Entity
    {
       

        private Lokasyon ustlokasyon;
        public Lokasyon UstLokasyon
        {
            get
            {
                return ustlokasyon == null ? ustlokasyon = new Lokasyon() : ustlokasyon;
            }
            set { ustlokasyon = value; }
        }

        [FieldDefinition(Length =100)]
        public string Adi { get; set; }

        public int Seviye { get; set; }

        public string Kodu { get; set; }

        public string Aciklama { get; set; }

        [FieldDefinition(Length = 100)]
        public string tagadresi;

        public string TagAdresi
        {
            get
            {
                return tagadresi;
            }
            set
            {
                tagadresi = value;
            }

        }

        private Adres adres;
        public Adres Adres
        {
            get
            {
                return adres == null ? adres = new Adres() : adres;
            }
            set { adres = value; }
        }

        public override string ToString() { return this.Adi ?? ""; }

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public long UstLokasyon_Id
        {
            get
            {
                return UstLokasyon == null ? 0 : UstLokasyon.Id;
            }

        }
       
    }
}
