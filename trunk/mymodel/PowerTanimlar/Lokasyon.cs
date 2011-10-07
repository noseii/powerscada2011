using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;


namespace mymodel
{
    public class Lokasyon : Entity
    {
        private Lokasyon Ust_Lokasyon;

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

        public override string ToString() { return this.Adi ?? ""; }
       
    }
}
