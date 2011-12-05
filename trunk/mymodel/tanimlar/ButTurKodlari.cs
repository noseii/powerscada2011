using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class ButTurKodlari : BaseEntity
    {
        [FieldDefinition(Length = 40)]
        public string Kodu { get; set; }

        [FieldDefinition(Length = 120)]
        public string Adi { get; set; }

        [FieldDefinition(Length =120)]
        public string Aciklama { get; set; }

        public override string ToString() { return this.Adi ?? ""; }
       
    }
}
