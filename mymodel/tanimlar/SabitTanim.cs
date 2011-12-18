using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class SabitTanim :BaseEntity
    {
        //TODO: NE İŞE YARAR
        public Int32 Kodu { get; set; }
        [FieldDefinition(Length =80)]
        public string Adi { get; set; }
        [FieldDefinition(Length =200)]
        public string Aciklama { get; set; }

        public override string ToString() { return this.Adi ?? ""; }
       
    }
}
