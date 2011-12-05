using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class Defter :BaseEntity
    {
        //TODO:Defter ne işe yarar tekrar sorulacak
        [FieldDefinition(Length =80)]
        public string Adi { get; set; }
        public Hizmet HizmetlerDet { get; set; }
        public Int32 UzmanlikKodu { get; set; }
        public bool SabitFiyat { get; set; }

        public override string ToString() { return this.Adi ?? ""; }
       
    }
}
