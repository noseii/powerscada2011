using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class HizmetlerAlt :BaseEntity
    {
        public Hizmetler Hizmetler { get; set; }
        [FieldDefinition(Length =256)]
        public string Adi { get; set; }

        public override string ToString() { return this.Adi ?? ""; }
       
    }
}
