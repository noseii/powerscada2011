using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class HizmetTur : Entity
    {
        [FieldDefinition(Length = 50)]
        public string Kodu { get; set; }

        [FieldDefinition(Length = 255)]
        public string Adi { get; set; }

        [FieldDefinition(Length =255)]
        public string Aciklama { get; set; }

        public override string ToString() { return this.Adi ?? ""; }
       
    }
}
