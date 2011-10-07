using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class Ulke : Entity
    {
        [FieldDefinition(Length =50)]
        public string Adi { get; set; }
        public string Kodu { get; set; }

        public override string ToString() { return this.Adi ?? ""; }
       
    }
}
