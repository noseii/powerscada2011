using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class Hizmetler :BaseEntity
    {
        [FieldDefinition(Length =100)]
        public string Adi { get; set; }
        public Int32 HizmetYeriKodu { get; set; }
        [FieldDefinition(Length =100)]
        public string MuhasebeGelirHesapKodu { get; set; }
        public Int32 Seviye { get; set; }

        public override string ToString() { return this.Adi ?? ""; }
       
    }
}
